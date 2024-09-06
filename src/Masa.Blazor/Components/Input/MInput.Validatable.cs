using System.Collections;
using System.Linq.Expressions;
using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public partial class MInput<TValue> : IInputJsCallbacks, IValidatable
{
    [Inject] private InputJSModule InputJSModule { get; set; } = null!;

    [CascadingParameter] public MForm? Form { get; set; }

    [CascadingParameter] public EditContext? EditContext { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool ValidateOnBlur { get; set; }

    [Parameter]
    public virtual TValue? Value
    {
        get => GetValue(DefaultValue);
        set => SetValue(value);
    }

    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter] public Expression<Func<TValue>>? ValueExpression { get; set; }

    [Parameter] public bool Error { get; set; }

    [Parameter] public int ErrorCount { get; set; } = 1;

    [Parameter] public List<string>? ErrorMessages { get; set; }

    [Parameter] public List<string>? Messages { get; set; } = new();

    [Parameter] public EventCallback<TValue> OnInput { get; set; }

    [Parameter] public bool Success { get; set; }

    [Parameter] public List<string>? SuccessMessages { get; set; }

    [Parameter]
    public IEnumerable<Func<TValue?, StringBoolean>>? Rules
    {
        get => GetValue<IEnumerable<Func<TValue?, StringBoolean>>>();
        set => SetValue(value);
    }

    private bool _hasInput;
    private bool _hasFocused;
    private bool _isResetting;
    private bool _forceStatus;
    private bool _internalValueChangingFromOnValueChanged;
    private CancellationTokenSource? _cancellationTokenSource;
    private EditContext? _prevEditContext;
    private InternalValueChangeType _changeType;

    protected void UpdateInternalValue(TValue? value, InternalValueChangeType changeType)
    {
        _changeType = changeType;
        InternalValue = value;
    }

    protected virtual TValue? DefaultValue => default;

    public FieldIdentifier? ValueIdentifier => ValueExpression is null ? null : FieldIdentifier.Create(ValueExpression);

    public virtual ElementReference InputElement { get; set; }

    protected virtual TValue? InternalValue
    {
        get => GetValue<TValue>();
        set
        {
            var clonedLazyValue = value.TryDeepClone();
            SetValue(clonedLazyValue);
        }
    }

    public bool IsFocused
    {
        get => GetValue<bool>();
        protected set => SetValue(value);
    }

    public List<string> ErrorBucket { get; protected set; } = new();

    public virtual bool HasError => ErrorMessages is { Count: > 0 } || ErrorBucket.Count > 0 || Error;

    public virtual bool HasSuccess => SuccessMessages is { Count: > 0 } || Success;

    public virtual bool HasState
    {
        get
        {
            if (IsDisabled)
            {
                return false;
            }

            return HasSuccess || (ShouldValidate && HasError);
        }
    }

    public virtual bool HasMessages => ValidationTarget.Count > 0;

    public List<string> ValidationTarget
    {
        get
        {
            if (ErrorMessages is { Count: > 0 })
            {
                return ErrorMessages;
            }

            if (SuccessMessages?.Count > 0)
            {
                return SuccessMessages;
            }

            if (Messages?.Count > 0)
            {
                return Messages;
            }

            if (ShouldValidate)
            {
                return ErrorBucket;
            }

            return new List<string>();
        }
    }

    public virtual bool IsDisabled => Disabled || Form is { Disabled: true };

    public bool IsInteractive => !IsDisabled && !IsReadonly;

    public virtual bool IsReadonly => Readonly || Form is { Readonly: true };

    public ValidateOn ValidateOn
    {
        get
        {
            if (IsDirtyParameter(nameof(ValidateOnBlur)))
            {
                return ValidateOnBlur ? ValidateOn.Blur : ValidateOn.Input;
            }
            
            return Form?.ValidateOn ?? ValidateOn.Input;
        }
    }

    public virtual bool ShouldValidate
    {
        get
        {
            if (ExternalError)
            {
                return true;
            }

            if (ValidateOn == ValidateOn.Blur)
            {
                return _hasFocused;
            }

            return _hasInput || _hasFocused;
        }
    }

    public virtual bool ExternalError => ErrorMessages is { Count: > 0 } || Error;

    public virtual int InternalDebounceInterval => 0;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Form?.Register(this);

        Id ??= "input-" + Guid.NewGuid().ToString("N");

        InternalValue = Value;

        if (InputsFilter != null && ValueChanged.HasDelegate)
        {
            InputsFilter.RegisterInput(this);
            Dense = InputsFilter.Dense;
            HideDetails = InputsFilter.HideDetails;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InputJSModule.InitializeAsync(this);

            if (!DisableSetValueByJsInterop)
            {
                _ = SetValueByJsInterop(Formatter(Value));
            }
        }
    }

    public virtual async Task HandleOnInputAsync(ChangeEventArgs args)
    {
        if (BindConverter.TryConvertTo<TValue>(args.Value?.ToString(), CultureInfo.InvariantCulture, out var val))
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(val);
            }
        }

        if (ValidateOn == ValidateOn.Input)
        {
            //We removed NextTick since it doesn't trigger render
            //and validate may not be called
            InternalValidate();
        }
    }

    public virtual Task HandleOnChangeAsync(ChangeEventArgs args)
    {
        return Task.CompletedTask;
    }

    protected virtual bool DisableSetValueByJsInterop => false;

    protected virtual bool WatchValueChangeImmediately => true;

    protected virtual async Task SetValueByJsInterop(string? val)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        await Retry(() => InputJSModule.SetValue(val),
            () => InputJSModule is not { Initialized: true },
            cancellationToken: _cancellationTokenSource.Token);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<TValue>(nameof(Value), OnValueChanged, immediate: WatchValueChangeImmediately)
            .Watch<TValue>(nameof(InternalValue), OnInternalValueChange)
            .Watch<bool>(nameof(IsFocused), IsFocusedChangeCallback)
            .Watch<bool>(nameof(Required), () =>
            {
                // waiting for InternalValue to be assigned
                NextTick(InternalValidate);
            }).Watch<IEnumerable<Func<TValue, StringBoolean>>>(nameof(Rules), () =>
            {
                Console.Out.WriteLine($"Rules HashCode: {Rules?.GetHashCode()}");
                // waiting for InternalValue to be assigned
                // NextTick(InternalValidate);
            });
    }

    private async void IsFocusedChangeCallback(bool val)
    {
        if (!val && !IsDisabled)
        {
            _hasFocused = true;

            if (ValidateOn == ValidateOn.Blur)
            {
                InternalValidate();
            }
        }

        await OnIsFocusedChange(val);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif

        SubscribeValidationStateChanged();

        if (ValueChanged.HasDelegate && !EqualityComparer<TValue>.Default.Equals(Value, InternalValue))
        {
            OnValueChanged(Value);
        }
    }

    protected virtual void OnValueChanged(TValue? val)
    {
        var isEqual = true;
        if (val is IList valList && InternalValue is IList internalValueList)
        {
            if (valList.Count != internalValueList.Count ||
                valList.Cast<object>().Any(valItem => !internalValueList.Contains(valItem)))
            {
                isEqual = false;
            }
        }
        else
        {
            isEqual = EqualityComparer<TValue>.Default.Equals(val, InternalValue);
        }

        if (!isEqual)
        {
            _internalValueChangingFromOnValueChanged = true;

            if (!DisableSetValueByJsInterop)
            {
                _ = SetValueByJsInterop(Formatter(val));
            }

            InternalValue = val;
        }
        else
        {
            _internalValueChangingFromOnValueChanged = false;
        }
    }

    protected virtual void OnInternalValueChange(TValue? val)
    {
        // If it's the first time we're setting input,
        // mark it with hasInput
        _hasInput = true;

        if (ValidateOn == ValidateOn.Input)
        {
            NextTick(InternalValidate);
        }

        if (_internalValueChangingFromOnValueChanged)
        {
            _internalValueChangingFromOnValueChanged = false;
        }
        else
        {
            if (_changeType != InternalValueChangeType.Input && !DisableSetValueByJsInterop)
            {
                _ = SetValueByJsInterop(Formatter(val));
            }

            _ = ValueChanged.InvokeAsync(val.TryDeepClone());
        }
    }
    
    protected void SubscribeValidationStateChanged()
    {
        //When EditContext update,we should re-subscribe OnValidationStateChanged
        if (_prevEditContext == EditContext) return;

        if (_prevEditContext != null)
        {
            _prevEditContext.OnValidationStateChanged -= HandleOnValidationStateChanged;
        }

        if (EditContext != null)
        {
            // assume the ValueExpression would not change after the component is initialized
            if (ValueIdentifier.HasValue)
            {
                EditContext.OnValidationStateChanged += HandleOnValidationStateChanged;
            }
            else
            {
                Logger.LogWarning(
                    $"{(string.IsNullOrWhiteSpace(Label) ? "" : $"[{Label}] ")}ValueExpression was missing, the validation is not working. Ignore this warning if validation is not needed.");
            }
        }

        _prevEditContext = EditContext;
    }

    protected virtual Task OnIsFocusedChange(bool val)
    {
        return Task.CompletedTask;
    }

    protected void InternalValidate()
    {
        if (_isResetting)
        {
            _isResetting = false;
            _hasInput = false;
            _hasFocused = false;
        }

        if (EditContext == null)
        {
            var previousErrorBucket = ErrorBucket;
            ErrorBucket.Clear();
            ErrorBucket.AddRange(ValidateRules(InternalValue));
            if (!previousErrorBucket.OrderBy(e => e).SequenceEqual(ErrorBucket.OrderBy(e => e)))
            {
                Form?.UpdateValidValue();
                StateHasChanged();
            }

            return;
        }

        if (ValueIdentifier.HasValue && !EqualityComparer<FieldIdentifier>.Default.Equals(ValueIdentifier.Value, default))
        {
            EditContext.NotifyFieldChanged(ValueIdentifier.Value);
        }
    }

    private IEnumerable<string> ValidateRules(TValue? value)
    {
        foreach (var rule in InternalRules)
        {
            var result = rule(value);
            if (result.IsT0)
            {
                yield return result.AsT0;
            }
        }
    }

    protected virtual string? Formatter(object? val)
    {
        return BindConverter.FormatValue(val, CultureInfo.CurrentUICulture)?.ToString();
    }

    /// <summary>
    /// Gives focus.
    /// </summary>
    [MasaApiPublicMethod]
    public async Task FocusAsync()
    {
        await InputElement.FocusAsync().ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task BlurAsync()
    {
        await Js.InvokeVoidAsync(JsInteropConstants.Blur, InputElement).ConfigureAwait(false);
    }

    public bool Validate()
    {
        return Validate(default);
    }

    protected bool Validate(TValue? val)
    {
        var force = true;

        _forceStatus = force;

        //No rules should be valid. 
        var valid = true;

        if (force)
        {
            _hasInput = true;
            _hasFocused = true;
        }

        if (InternalRules.Any())
        {
            var value = EqualityComparer<TValue>.Default.Equals(val, default) ? InternalValue : val;

            ErrorBucket.Clear();
            ErrorBucket.AddRange(ValidateRules(value));

            valid = ErrorBucket.Count == 0;
        }

        return valid;
    }

    public void Reset()
    {
        _hasInput = false;
        _hasFocused = false;
        _isResetting = true;

        if (ValueIdentifier.HasValue)
        {
            EditContext?.MarkAsUnmodified(ValueIdentifier.Value);
        }

        // If the input is not bound to a ValueChanged event,
        // We assume it's a scenario like "read-only" or "value set automatically by other property",
        // So we would not reset the value.
        if (ValueChanged.HasDelegate)
        {
            UpdateInternalValue(default, InternalValueChangeType.InternalOperation);
        }
    }

    public void ResetValidation()
    {
        _hasInput = false;
        _hasFocused = false;
        _isResetting = true;
    }

    protected void HandleOnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        // The following conditions require an error message to be displayed:
        // 1. Force validation, because it validates all input elements
        // 2. The input pointed to by ValueIdentifier has been modified
        if (!_forceStatus && EditContext?.IsModified() is true
                          && !EditContext.IsModified(ValueIdentifier!.Value)
                          && InternalRules.Any() is false)
        {
            return;
        }

        _forceStatus = false;

        ErrorBucket.Clear();

        var editContextErrors = EditContext!.GetValidationMessages(ValueIdentifier!.Value).ToList();
        ErrorBucket.AddRange(editContextErrors);

        var ruleErrors = ValidateRules(InternalValue);
        ErrorBucket.AddRange(ruleErrors);

        Form?.UpdateValidValue();

        InvokeStateHasChanged();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        if (EditContext != null)
        {
            EditContext.OnValidationStateChanged -= HandleOnValidationStateChanged;
        }

        Form?.Remove(this);

        return base.DisposeAsyncCore();
    }
}