using System.Collections;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;

namespace Masa.Blazor;

public partial class MInput<TValue> : IInputJsCallbacks, IValidatable
{
    [Inject] private InputJSModule InputJSModule { get; set; } = null!;

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

    [CascadingParameter] public MForm? Form { get; set; }

    [CascadingParameter] public EditContext? EditContext { get; set; }

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

    private bool _forceStatus;
    private bool _internalValueChangingFromOnValueChanged;
    private CancellationTokenSource? _cancellationTokenSource;

    private InternalValueChangeType _changeType;

    protected void UpdateInternalValue(TValue? value, InternalValueChangeType changeType)
    {
        _changeType = changeType;
        InternalValue = value;
    }

    protected virtual TValue? DefaultValue => default;

    protected EditContext? OldEditContext { get; set; }

    public FieldIdentifier ValueIdentifier { get; set; }

    protected bool HasInput { get; set; }

    protected bool HasFocused { get; set; }

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

    public virtual bool ShouldValidate
    {
        get
        {
            if (ExternalError)
            {
                return true;
            }

            if (ValidateOnBlur)
            {
                return HasFocused && !IsFocused;
            }

            return HasInput || HasFocused;
        }
    }

    public virtual bool ExternalError => ErrorMessages is { Count: > 0 } || Error;

    public virtual int InternalDebounceInterval => 0;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Form?.Register(this);

        InternalValue = Value;

        if (InputsFilter != null)
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

        if (!ValidateOnBlur)
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

    protected virtual bool ValidateOnlyInFocusedState => true;

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
            HasFocused = true;
            if (ValidateOnBlur)
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
        HasInput = true;

        if (ValidateOnlyInFocusedState)
        {
            if (HasFocused)
            {
                NextTickIf(InternalValidate, () => !ValidateOnBlur);
            }
        }
        else
        {
            NextTickIf(InternalValidate, () => !ValidateOnBlur);
        }

        if (_internalValueChangingFromOnValueChanged)
        {
            _internalValueChangingFromOnValueChanged = false;
        }
        else if (ValueChanged.HasDelegate)
        {
            if (_changeType != InternalValueChangeType.Input && !DisableSetValueByJsInterop)
            {
                _ = SetValueByJsInterop(Formatter(val));
            }

            _ = ValueChanged.InvokeAsync(val.TryDeepClone());
        }
    }

    protected virtual void SubscribeValidationStateChanged()
    {
        if (ValueExpression != null)
        {
            ValueIdentifier = FieldIdentifier.Create(ValueExpression);
        }
        else
        {
            //No ValueExpression,subscribe is unnecessary
            return;
        }

        //When EditContext update,we should re-subscribe OnValidationStateChanged
        if (OldEditContext != EditContext)
        {
            if (OldEditContext != null)
            {
                OldEditContext.OnValidationStateChanged -= HandleOnValidationStateChanged;
            }

            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged += HandleOnValidationStateChanged;
            }

            OldEditContext = EditContext;
        }
    }

    protected virtual Task OnIsFocusedChange(bool val)
    {
        return Task.CompletedTask;
    }

    protected virtual void InternalValidate()
    {
        var previousErrorBucket = ErrorBucket;
        ErrorBucket.Clear();

        if (EditContext != null)
        {
            if (!EqualityComparer<FieldIdentifier>.Default.Equals(ValueIdentifier, default))
            {
                EditContext.NotifyFieldChanged(ValueIdentifier);
            }
        }
        else
        {
            ErrorBucket.AddRange(ValidateRules(InternalValue));
            if (!previousErrorBucket.OrderBy(e => e).SequenceEqual(ErrorBucket.OrderBy(e => e)))
            {
                StateHasChanged();
            }
        }

        Form?.UpdateValidValue();
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
            HasInput = true;
            HasFocused = true;
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
        ErrorBucket.Clear();

        HasInput = false;
        HasFocused = false;

        EditContext?.MarkAsUnmodified(ValueIdentifier);

        UpdateInternalValue(default, InternalValueChangeType.InternalOperation);
    }

    public void ResetValidation()
    {
        ErrorBucket.Clear();
    }

    protected virtual void HandleOnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        // The following conditions require an error message to be displayed:
        // 1. Force validation, because it validates all input elements
        // 2. The input pointed to by ValueIdentifier has been modified
        if (!_forceStatus && EditContext?.IsModified() is true
                          && !EditContext.IsModified(ValueIdentifier)
                          && InternalRules.Any() is false)
        {
            return;
        }

        _forceStatus = false;

        var editContextErrors = EditContext!.GetValidationMessages(ValueIdentifier).ToList();
        ErrorBucket.AddRange(editContextErrors);

        var ruleErrors = ValidateRules(InternalValue);
        ErrorBucket.AddRange(ruleErrors);

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