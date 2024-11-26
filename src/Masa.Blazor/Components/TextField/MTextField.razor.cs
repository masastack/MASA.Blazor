using System.Runtime.CompilerServices;
using Masa.Blazor.Components.Input;
using Masa.Blazor.Extensions;

namespace Masa.Blazor;

public partial class MTextField<TValue> : MInput<TValue>
{
    [Inject]
    public MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject]
    private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter]
    public virtual bool Clearable { get; set; }

    [Parameter]
    public string? Format { get; set; }

    [Parameter]
    public string? Locale { get; set; }

    [Parameter]
    public bool PersistentPlaceholder { get; set; }

    [MasaApiParameter("$clear")]
    [Parameter]
    public string ClearIcon { get; set; } = "$clear";

    [Parameter]
    public bool FullWidth { get; set; }

    [Parameter]
    public string? Prefix
    {
        get => GetValue<string?>();
        set => SetValue(value);
    }

    [Parameter]
    public bool SingleLine { get; set; }

    [Parameter]
    public bool Solo { get; set; }

    [Parameter]
    public bool SoloInverted { get; set; }

    [Parameter]
    public bool Flat { get; set; }

    [Parameter]
    public bool Filled { get; set; }

    [Parameter]
    public virtual bool Outlined
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    [Parameter]
    public bool Reverse { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public bool Rounded { get; set; }

    [Parameter]
    public bool Shaped { get; set; }

    [Parameter]
    public string Type { get; set; } = "text";

    [Parameter]
    public RenderFragment? PrependInnerContent { get; set; }

    [Parameter]
    public string? AppendOuterIcon { get; set; }

    [Parameter]
    public RenderFragment? AppendOuterContent { get; set; }

    [Parameter]
    public string? PrependInnerIcon { get; set; }

    [Parameter]
    public string? Suffix { get; set; }

    [Parameter]
    public bool Autofocus { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnBlur { get; set; }

    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

    [Parameter]
    public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

    [Parameter]
    public EventCallback OnEnter { get; set; }

    [Parameter]
    public RenderFragment? ProgressContent { get; set; }

    [Parameter]
    public StringNumber LoaderHeight { get; set; } = 2;

    [Parameter]
    public StringNumberBoolean? Counter { get; set; }

    [Parameter]
    public Func<TValue, int>? CounterValue { get; set; }

    [Parameter]
    public RenderFragment? CounterContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnAppendOuterClick { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnPrependInnerClick { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClearClick { get; set; }

    [Parameter]
    public virtual Action<TextFieldNumberProperty>? NumberProps { get; set; }

    [Parameter]
    public int DebounceInterval { get; set; }

    /// <summary>
    /// Update the bound value on change event instead of on input.
    /// </summary>
    [Parameter]
    [Obsolete("Use UpdateOnChange instead.")]
    public virtual bool UpdateOnBlur { get; set; }

    /// <summary>
    /// Update the bound value on change event instead of on input.
    /// </summary>
    [Parameter]
    public virtual bool UpdateOnChange { get; set; }

    private static readonly string[] s_dirtyTypes = { "color", "file", "time", "date", "datetime-local", "week", "month" };

    private bool _badInput;

    public TextFieldNumberProperty Props { get; set; } = new();

    public bool IsSolo => Solo || SoloInverted;

    public bool IsBooted { get; set; } = true;

    public bool IsEnclosed => Filled || IsSolo || Outlined;

    public bool ShowLabel => HasLabel && !(IsSingle && LabelValue);

    public bool IsSingle => IsSolo || SingleLine || FullWidth || (Filled && !HasLabel);

    protected override bool IsDirty => base.IsDirty || _badInput;

    public override int InternalDebounceInterval => DebounceInterval;

    public override bool IsLabelActive => IsDirty || s_dirtyTypes.Contains(Type);

    public virtual bool LabelValue => IsFocused || IsLabelActive || PersistentPlaceholder;

    protected double LabelWidth { get; set; }

    public string LegendInnerHTML => "&#8203;";

    public bool HasCounter => Counter != false && Counter != null;

    public override string? ComputedColor => !SoloInverted || !IsFocused ? base.ComputedColor : Color;

    public override bool HasColor => IsFocused;

    public virtual string Tag => "input";

    protected virtual Dictionary<string, object> InputAttrs
    {
        get
        {
            Dictionary<string, object> attributes = new(Attributes) { { "type", Type } };

            if (Type == "number")
            {
                if (Props.Min.HasValue)
                    attributes.Add("min", Props.Min);
                if (Props.Max.HasValue)
                    attributes.Add("max", Props.Max);
                attributes.Add("step", Props.Step);
            }

            return attributes;
        }
    }

    public virtual StringNumber ComputedCounterValue
    {
        get
        {
            if (InternalValue is null)
            {
                return 0;
            }

            return CounterValue?.Invoke(InternalValue) ?? InternalValue.ToString()!.Length;
        }
    }

    public override bool HasDetails => base.HasDetails || HasCounter;

    public StringNumber? Max
    {
        get
        {
            int? max = null;

            if (Counter == true)
            {
                if (Attributes.TryGetValue("maxlength", out var maxValue))
                {
                    max = Convert.ToInt32(maxValue);
                }
            }
            else
            {
                max = Counter?.ToInt32();
            }

            return max == null ? null : (StringNumber)max;
        }
    }

    protected double PrefixWidth { get; set; }

    protected double PrependWidth { get; set; }

    protected(StringNumber left, StringNumber right) LabelPosition
    {
        get
        {
            var offset = (Prefix != null && !LabelValue) ? PrefixWidth : 0;

            if (LabelValue && PrependWidth > 0)
            {
                offset -= PrependWidth;
            }

            return MasaBlazor?.RTL == Reverse ? (offset, "auto") : ("auto", offset);
        }
    }

    public bool UpButtonEnabled { get; set; } =  true;
    public bool DownButtonEnabled { get; set; } = true;
    public MLabel? LabelReference { get; set; }

    public ElementReference PrefixElement { get; set; }

    private ElementReferenceWrapper _prependInnerElementReferenceWrapper = new();
    private ElementReferenceWrapper _appendInnerElementReferenceWrapper = new();

    public ElementReference PrependInnerElement => _prependInnerElementReferenceWrapper.Value;

    public ElementReference AppendInnerElement => _appendInnerElementReferenceWrapper.Value;

    private static Block _block = new("m-text-field");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _numberModifierBuilder = _block.Element("number").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(new[]{
            _modifierBuilder
                .Add(FullWidth)
                .Add("prefix", Prefix != null)
                .Add("single-line", IsSingle)
                .Add("solo", IsSolo)
                .Add(SoloInverted)
                .Add("solo-flat", Flat)
                .Add(Filled)
                .Add(IsBooted)
                .Add("enclosed", IsEnclosed)
                .Add(Reverse)
                .Add(Outlined)
                .Add("placeholder", Placeholder != null)
                .Add(Rounded)
                .Add(Shaped)
                .Build()
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        NumberProps?.Invoke(Props);

        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await Js.InvokeVoidAsync(JsInteropConstants.RegisterTextFieldOnMouseDown, InputSlotElement, InputElement,
                DotNetObjectReference.Create(new Invoker<MouseEventArgs>(HandleOnMouseDownAsync)));

            await IntersectJSModule.ObserverAsync(InputElement, async _ =>
            {
                await TryAutoFocus();
                await OnResize();
            });

            var tasks = new Task[3];

            tasks[0] = SetLabelWidthAsync();
            tasks[1] = SetPrefixWidthAsync();
            tasks[2] = SetPrependWidthAsync();

            if (tasks.All(task => task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Canceled))
            {
                return;
            }

            await Task.WhenAll(tasks);
            StateHasChanged();
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Outlined), SetLabelWidthAsync)
            .Watch<string>(nameof(Label), () => NextTick(SetLabelWidthAsync)) //TODO:form auto label
            .Watch<string>(nameof(Prefix), SetPrefixWidthAsync);
    }

    protected override string? Formatter(object? val)
    {
        var localeExists = !string.IsNullOrWhiteSpace(Locale);
        var formatExists = !string.IsNullOrWhiteSpace(Format);
        
        if (localeExists || formatExists)
        {
            if (val is DateTime dt)
            {
                return dt.ToString(
                    format: formatExists ? Format : null,
                    provider: localeExists ? CurrentLocale : null);
            }
        }

        if (IsNumberType && Props.Precision.HasValue && BindConverter.TryConvertToDecimal(val?.ToString(), CultureInfo.InvariantCulture, out var value))
        {
            return value.ToString(Props.PrecisionFormat);
        }

        return base.Formatter(val);
    }

    public CultureInfo CurrentLocale
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture;

            if (Locale is not null)
            {
                try
                {
                    culture = CultureInfo.CreateSpecificCulture(Locale);
                }
                catch (CultureNotFoundException e)
                {
                    Logger.LogWarning(e, "Locale {Locale} is not found", Locale);
                }
            }

            return culture;
        }
    }

    private async Task SetLabelWidthAsync()
    {
        if (!Outlined)
        {
            return;
        }

        //No label
        if (LabelReference is not { Ref.Context: { } })
        {
            return;
        }

        var scrollWidth = await Js.GetScrollWidthAsync(LabelReference.Ref);
        if (scrollWidth == null) return;

        var offsetWidth = await Js.GetOffsetWidthAsync(Ref);
        if (offsetWidth == null) return;

        LabelWidth = Math.Min(scrollWidth.Value * 0.75 + 6, offsetWidth.Value - 24);

        StateHasChanged();
    }

    private async Task SetPrefixWidthAsync()
    {
        if (PrefixElement.Context == null)
        {
            return;
        }

        var offsetWidth = await Js.GetOffsetWidthAsync(PrefixElement);
        if (offsetWidth == null) return;

        PrefixWidth = offsetWidth.Value;

        StateHasChanged();
    }

    private async Task SetPrependWidthAsync()
    {
        if (!Outlined)
        {
            return;
        }

        if (PrependInnerElement.Context == null)
        {
            return;
        }

        var offsetWidth = await Js.GetOffsetWidthAsync(PrependInnerElement);
        if (offsetWidth == null) return;

        PrependWidth = offsetWidth.Value;

        StateHasChanged();
    }

    private async Task<bool> TryAutoFocus()
    {
        if (!Autofocus || InputElement.Context is null)
        {
            return false;
        }

        try
        {
            await InputElement.FocusAsync();
            return true;
        }
        catch (JSException)
        {
            return false;
        }
    }

    private async Task OnResize()
    {
        await PreventRenderingUtil(SetLabelWidthAsync, SetPrefixWidthAsync, SetPrependWidthAsync);
        StateHasChanged();
    }

    public virtual async Task HandleOnAppendOuterClickAsync(MouseEventArgs args)
    {
        if (OnAppendOuterClick.HasDelegate)
        {
            await OnAppendOuterClick.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnPrependInnerClickAsync(MouseEventArgs args)
    {
        if (OnPrependInnerClick.HasDelegate)
        {
            await OnPrependInnerClick.InvokeAsync(args);
        }
    }

    public override async Task HandleOnClickAsync(ExMouseEventArgs args)
    {
        if (IsFocused || IsDisabled)
        {
            return;
        }

        await InputElement.FocusAsync();
    }

    public virtual async Task HandleOnBlurAsync(FocusEventArgs args)
    {
        _ = Js.InvokeVoidAsync(JsInteropConstants.RemoveStopPropagationEvent,  InputElement, "wheel");
            
        IsFocused = false;
        if (OnBlur.HasDelegate)
        {
            await OnBlur.InvokeAsync(args);
        }
    }

    public override Task HandleOnInputAsync(ChangeEventArgs args)
    {
        return HandleOnInputOrChangeEvent(args, OnInput);
    }

    public override Task HandleOnChangeAsync(ChangeEventArgs args)
    {
        return HandleOnInputOrChangeEvent(args, OnChange);
    }

    private bool IsNumberType => Type.Equals("number", StringComparison.OrdinalIgnoreCase);

    private async Task HandleOnInputOrChangeEvent(ChangeEventArgs args, EventCallback<TValue> cb,
        [CallerArgumentExpression("cb")] string cbName = "")
    {
        var originValue = args.Value?.ToString();

        var succeed = TryConvertTo<TValue>(originValue, out var result);

        var updateOnChange = UpdateOnBlur || UpdateOnChange || IsNumberType;

        if ((cbName == nameof(OnInput) && !updateOnChange) || (cbName == nameof(OnChange) && updateOnChange))
        {
            UpdateValue(originValue, succeed, result);
            StateHasChanged();
        }

        if (succeed && cb.HasDelegate)
        {
            await cb.InvokeAsync(result);
        }
    }

    protected override void UpdateInternalValue(TValue? value, InternalValueChangeType changeType)
    {
        base.UpdateInternalValue(value, changeType);

        if (IsNumeric(value, out var numeric))
        {
            var decimalValue = Convert.ToDecimal(numeric, CultureInfo.InvariantCulture);

            UpButtonEnabled = !Props.Max.HasValue || decimalValue < Props.Max;
            DownButtonEnabled = !Props.Min.HasValue || decimalValue > Props.Min;
        }
    }

    private bool IsNumeric(TValue? value, out IConvertible numeric)
    {
        numeric = default;
        if (IsNumberType && value is not string && value is IConvertible convertible)
        {
            numeric = convertible;
            return true;
        }

        return false;
    }

    protected override TValue? ConvertAndSetValueByJSInterop(TValue? val)
    {
        if (Props.Precision.HasValue && IsNumeric(val, out var numeric))
        {
            var decimalValue = Convert.ToDecimal(numeric, CultureInfo.InvariantCulture);
            var newValue = Math.Round(decimalValue, Props.Precision.Value);
            _ = SetValueByJsInterop(newValue.ToString(Props.PrecisionFormat, CultureInfo.InvariantCulture));
            return (TValue)Convert.ChangeType(newValue, typeof(TValue), CultureInfo.InvariantCulture);
        }

        return base.ConvertAndSetValueByJSInterop(val);
    }

    private static bool TryConvertTo<T>(string? value, out T? result)
    {
        var succeeded = BindConverter.TryConvertTo<T>(value, CultureInfo.InvariantCulture, out var val);

        if (succeeded)
        {
            result = val;
            return true;
        }

        result = default;
        return false;
    }

    private void UpdateValue(string? originValue, bool succeeded, TValue? convertedValue)
    {
        if (succeeded)
        {
            _badInput = false;
            var validValue = GetValidValue(convertedValue);
            UpdateInternalValue(validValue, InternalValueChangeType.Input);
        }
        else
        {
            _badInput = true;
            UpdateInternalValue(default, InternalValueChangeType.Input);

            if (IsNumberType)
            {
                // reset the value of input element if failed to convert
                _ = SetValueByJsInterop(0.ToString(Props.PrecisionFormat));
            }
        }

        if (ValidateOn == ValidateOn.Input)
        {
            //We removed NextTick since it doesn't trigger render
            //and validate may not be called
            InternalValidate();
        }
    }

    private TValue GetValidValue(TValue val)
    {
        if (IsNumeric(val, out var numeric))
        {
            var decimalValue = Convert.ToDecimal(numeric, CultureInfo.InvariantCulture);

            if (Props.Min.HasValue && decimalValue < Props.Min)
            {
                return (TValue)Convert.ChangeType(Props.Min, typeof(TValue), CultureInfo.InvariantCulture);
            }

            if (Props.Max.HasValue && decimalValue > Props.Max)
            {
                return (TValue)Convert.ChangeType(Props.Max, typeof(TValue), CultureInfo.InvariantCulture);
            }
        }

        return val;
    }

    public async Task HandleOnKeyUpAsync(KeyboardEventArgs args)
    {
        if (OnKeyUp.HasDelegate)
            await OnKeyUp.InvokeAsync(args);
    }

    public async Task HandleOnNumberUpClickAsync(MouseEventArgs args)
    {
        if (UpButtonEnabled)
        {
            if (InternalValue is IConvertible convertible)
            {
                var decimalValue = Convert.ToDecimal(convertible, CultureInfo.InvariantCulture);

                if (Props.Min != null && decimalValue < Props.Min)
                {
                    decimalValue = Props.Min.Value;
                }

                decimalValue += Props.Step;

                if (Props.Max != null && decimalValue > Props.Max)
                {
                    decimalValue = Props.Max.Value;
                }

                var newValue = (TValue)Convert.ChangeType(decimalValue, typeof(TValue), CultureInfo.InvariantCulture);
                UpdateInternalValue(newValue, InternalValueChangeType.InternalOperation);
                await OnChange.InvokeAsync(newValue);
            }
        }

        await InputElement.FocusAsync();
    }

    public async Task HandleOnNumberDownClickAsync(MouseEventArgs args)
    {
        if (DownButtonEnabled)
        {
            if (InternalValue is IConvertible convertible)
            {
                var decimalValue = Convert.ToDecimal(convertible, CultureInfo.InvariantCulture);

                if (Props.Max != null && decimalValue > Props.Max)
                {
                    decimalValue = Props.Max.Value;
                }

                decimalValue -= Props.Step;

                if (Props.Min != null && decimalValue < Props.Min)
                {
                    decimalValue = Props.Min.Value;
                }

                var newValue = (TValue)Convert.ChangeType(decimalValue, typeof(TValue), CultureInfo.InvariantCulture);
                UpdateInternalValue(newValue, InternalValueChangeType.InternalOperation);
                await OnChange.InvokeAsync(newValue);
            }
        }

        await InputElement.FocusAsync();
    }

    public virtual async Task HandleOnFocusAsync(FocusEventArgs args)
    {
        _ = Js.InvokeVoidAsync(JsInteropConstants.AddStopPropagationEvent, InputElement, "wheel");
            
        if (!IsFocused)
        {
            IsFocused = true;

            if (OnFocus.HasDelegate)
            {
                await OnFocus.InvokeAsync(args);
            }
        }
    }

    public virtual async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key is KeyCodes.Enter or KeyCodes.NumpadEnter)
        {
            if (OnEnter.HasDelegate)
            {
                await OnEnter.InvokeAsync();
            }

            await TryInvokeFieldChangeOfInputsFilter();
        }

        if (OnKeyDown.HasDelegate)
        {
            await OnKeyDown.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnClearClickAsync(MouseEventArgs args)
    {
        UpdateInternalValue(default, InternalValueChangeType.InternalOperation);

        if (OnClearClick.HasDelegate)
        {
            await OnClearClick.InvokeAsync(args);
        }

        await OnChange.InvokeAsync(default);

        await TryInvokeFieldChangeOfInputsFilter(isClear: true);

        await InputElement.FocusAsync();
    }

    public override async Task HandleOnMouseUpAsync(ExMouseEventArgs args)
    {
        if (HasMouseDown)
        {
            await InputElement.FocusAsync();
        }

        await base.HandleOnMouseUpAsync(args);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await Js.InvokeVoidAsync(JsInteropConstants.UnregisterTextFieldOnMouseDown, InputSlotElement);
        await IntersectJSModule.UnobserveAsync(InputElement);
            
        await base.DisposeAsyncCore();
    }
}