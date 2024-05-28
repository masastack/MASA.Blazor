﻿using System.Runtime.CompilerServices;
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

    public override string ComputedColor
    {
        get
        {
            if (!SoloInverted || !IsFocused)
            {
                return base.ComputedColor;
            }

            return Color ?? "primary";
        }
    }

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

    private string? NumberValue => InternalValue == null || string.IsNullOrWhiteSpace(InternalValue.ToString()) ? "0" : InternalValue.ToString();

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

    public bool UpButtonEnabled
    {
        get
        {
            if (Props.Max == null)
            {
                return true;
            }

            if (BindConverter.TryConvertToDecimal(NumberValue, CultureInfo.InvariantCulture, out var value))
            {
                return value < Props.Max;
            }

            return false;
        }
    }

    public bool DownButtonEnabled
    {
        get
        {
            if (Props.Min == null)
            {
                return true;
            }

            if (BindConverter.TryConvertToDecimal(NumberValue, CultureInfo.InvariantCulture, out var value))
            {
                return value > Props.Min;
            }

            return false;
        }
    }

    public MLabel? LabelReference { get; set; }

    public ElementReference PrefixElement { get; set; }

    private ElementReferenceWrapper _prependInnerElementReferenceWrapper = new();
    private ElementReferenceWrapper _appendInnerElementReferenceWrapper = new();

    public ElementReference PrependInnerElement => _prependInnerElementReferenceWrapper.Value;

    public ElementReference AppendInnerElement => _appendInnerElementReferenceWrapper.Value;

    private static Block _block = new("m-text-field");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private static ModifierBuilder _numberModifierBuilder = _block.Element("number").CreateModifierBuilder();

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
            .Watch<string>(nameof(Label), () => NextTick(SetLabelWidthAsync))
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

        await InputElement.FocusAsync();

        return true;
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

        var checkValue = CheckNumberValidate();

        if (!EqualityComparer<TValue>.Default.Equals(checkValue, InternalValue))
        {
            UpdateInternalValue(checkValue, InternalValueChangeType.InternalOperation);
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

    private async Task HandleOnInputOrChangeEvent(ChangeEventArgs args, EventCallback<TValue> cb,
        [CallerArgumentExpression("cb")] string cbName = "")
    {
        var originValue = args.Value?.ToString();

        var succeed = TryConvertTo<TValue>(originValue, out var result);

        var updateOnChange = UpdateOnBlur || UpdateOnChange;

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
            UpdateInternalValue(convertedValue, InternalValueChangeType.Input);
        }
        else
        {
            _badInput = true;
            UpdateInternalValue(default, InternalValueChangeType.Input);

            if (Type.ToLower() == "number")
            {
                // reset the value of input element if failed to convert
                if (!string.IsNullOrEmpty(originValue))
                {
                    _ = SetValueByJsInterop("");
                }
            }
        }

        if (!ValidateOnBlur)
        {
            //We removed NextTick since it doesn't trigger render
            //and validate may not be called
            InternalValidate();
        }
    }

    private TValue CheckNumberValidate()
    {
        if (Type != "number" || !BindConverter.TryConvertToDecimal(NumberValue, CultureInfo.InvariantCulture, out var value))
            return InternalValue;

        if (Props.Min != null && value < Props.Min &&
            BindConverter.TryConvertTo<TValue>(Props.Min.ToString(), CultureInfo.InvariantCulture, out var returnValue))
            return returnValue;

        if (Props.Max != null && value > Props.Max &&
            BindConverter.TryConvertTo<TValue>(Props.Max.ToString(), CultureInfo.InvariantCulture, out returnValue))
            return returnValue;

        return InternalValue;
    }

    public async Task HandleOnKeyUpAsync(KeyboardEventArgs args)
    {
        if (OnKeyUp.HasDelegate)
            await OnKeyUp.InvokeAsync(args);
    }

    public async Task HandleOnNumberUpClickAsync(MouseEventArgs args)
    {
        if (UpButtonEnabled && BindConverter.TryConvertToDecimal(NumberValue, CultureInfo.InvariantCulture, out decimal value))
        {
            if (Props.Min != null && value < Props.Min)
            {
                value = Props.Min.Value;
            }

            value += Props.Step;

            if (Props.Max != null && value > Props.Max)
            {
                value = Props.Max.Value;
            }

            if (BindConverter.TryConvertTo<TValue>(value.ToString(), CultureInfo.InvariantCulture, out var internalValue))
            {
                UpdateInternalValue(internalValue, InternalValueChangeType.InternalOperation);

                await OnChange.InvokeAsync(internalValue);
            }
        }

        await InputElement.FocusAsync();
    }

    public async Task HandleOnNumberDownClickAsync(MouseEventArgs args)
    {
        if (DownButtonEnabled && BindConverter.TryConvertToDecimal(NumberValue, CultureInfo.InvariantCulture, out var value))
        {
            if (Props.Max != null && value > Props.Max)
            {
                value = Props.Max.Value;
            }

            value -= Props.Step;

            if (Props.Min != null && value < Props.Min)
            {
                value = Props.Min.Value;
            }

            if (BindConverter.TryConvertTo<TValue>(value.ToString(), CultureInfo.InvariantCulture, out var internalValue))
            {
                UpdateInternalValue(internalValue, InternalValueChangeType.InternalOperation);

                await OnChange.InvokeAsync(internalValue);
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