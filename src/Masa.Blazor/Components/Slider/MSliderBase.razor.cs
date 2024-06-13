﻿namespace Masa.Blazor.Components.Slider;

/// <summary>
/// The base component for Slider and RangeSlider components, do not use.
/// </summary>
/// <typeparam name="TValue">Numeric type or a list of numeric type</typeparam>
/// <typeparam name="TNumeric">Numeric type</typeparam>
#if NET6_0
public partial class MSliderBase<TValue, TNumeric> : MInput<TValue>, IOutsideClickJsCallback
#else
public partial class MSliderBase<TValue, TNumeric> : MInput<TValue>, IOutsideClickJsCallback, IAsyncDisposable
    where TNumeric : struct, IComparable<TNumeric>
#endif
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private OutsideClickJSModule OutsideClickJSModule { get; set; } = null!;

    [Parameter] public bool Vertical { get; set; }

    [Parameter] [MasaApiParameter(100)] public double Max { get; set; } = 100;

    [Parameter] public double Min { get; set; }

    [Parameter] [MasaApiParameter(1)] public TNumeric Step { get; set; } = (TNumeric)(dynamic)1;

    [Parameter] public List<string> TickLabels { get; set; } = new();

    [Parameter] [MasaApiParameter(false)] public StringBoolean Ticks { get; set; } = false;

    [Parameter] public string? TrackColor { get; set; }

    [Parameter] public string? TrackFillColor { get; set; }

    [Parameter] [MasaApiParameter(2)] public double TickSize { get; set; } = 2;

    [Parameter] public StringBoolean? ThumbLabel { get; set; }

    [Parameter] public RenderFragment<TNumeric>? ThumbLabelContent { get; set; }

    [MasaApiParameter(Ignored = true)] public override EventCallback<MouseEventArgs> OnMouseDown { get; set; }

    [MasaApiParameter(Ignored = true)] public override EventCallback<MouseEventArgs> OnMouseUp { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.1.1")]
    public EventCallback<TValue> OnStart { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.1.1")]
    public EventCallback<TValue> OnEnd { get; set; }

    protected virtual double GetRoundedValue(int index)
    {
        return RoundValue(DoubleInternalValue);
    }

    public RenderFragment<int> ComputedThumbLabelContent
    {
        get
        {
            if (ThumbLabelContent != null)
            {
                return context => builder =>
                {
                    var value = (TNumeric)(dynamic)GetRoundedValue(context);
                    builder.AddContent(0, ThumbLabelContent(value));
                };
            }

            return context => builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, GetRoundedValue(context));
                builder.CloseElement();
            };
        }
    }

    [Parameter] public string? ThumbColor { get; set; }

    [Parameter] [MasaApiParameter(32)] public StringNumber ThumbSize { get; set; } = 32;

    [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }

    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

    [Parameter] public bool InverseLabel { get; set; }

    [Parameter] [MasaApiParameter(2)] public StringNumber LoaderHeight { get; set; } = 2;

    [Parameter] public RenderFragment? ProgressContent { get; set; }

    private CancellationTokenSource? _mouseCancellationTokenSource;

    protected virtual double DoubleInternalValue
    {
        get => (double)(dynamic)InternalValue!;
        set
        {
            var val = RoundValue(Math.Min(Math.Max(value, Min), Max));
            InternalValue = (TValue)Convert.ChangeType(val, typeof(TValue));
        }
    }

    public bool IsActive { get; set; }

    public bool NoClick { get; set; }

    public ElementReference SliderElement { get; set; }

    protected ElementReferenceWrapper ThumbElementWrapper = new();

    public ElementReference ThumbElement => ThumbElementWrapper.Value;

    public ElementReference TrackElement { get; set; }

    public bool ThumbPressed { get; set; }

    public double StartOffset { get; set; }

    public Dictionary<string, object> InputAttrs { get; } = new();

    public string? TrackTransition
    {
        get
        {
            if (ThumbPressed)
            {
                if (ShowTicks || (double)(dynamic)Step > 0)
                {
                    return "0.1s cubic-bezier(0.25, 0.8, 0.5, 1)";
                }

                return "none";
            }

            return null;
        }
    }

    public bool ShowTicks => TickLabels.Count > 0 || (!IsDisabled && (double)(dynamic)Step > 0 && Ticks != false);

    public double InputWidth => (RoundValue(DoubleInternalValue) - Min) / (Max - Min) * 100;

    public double StepNumeric => (double)(dynamic)Step > 0 ? (double)(dynamic)Step : 0;

    public string? ComputedTrackColor
    {
        get
        {
            if (IsDisabled)
            {
                return null;
            }

            if (TrackColor != null)
            {
                return TrackColor;
            }

            if (IsDark)
            {
                return ValidationState;
            }

            return string.IsNullOrEmpty(ValidationState) ? "primary lighten-3" : ValidationState;
        }
    }

    public string? ComputedTrackFillColor
    {
        get
        {
            if (IsDisabled)
            {
                return null;
            }

            if (TrackFillColor != null)
            {
                return TrackFillColor;
            }

            return string.IsNullOrEmpty(ValidationState) ? ComputedColor : ValidationState;
        }
    }

    public string ComputedThumbColor
    {
        get
        {
            if (ThumbColor != null)
            {
                return ThumbColor;
            }

            return string.IsNullOrEmpty(ValidationState) ? ComputedColor : ValidationState;
        }
    }

    public double NumTicks => Math.Ceiling((Max - Min) / StepNumeric);

    public bool ShowThumbLabel =>
        !IsDisabled && ((ThumbLabel != null && ThumbLabel != false) || ThumbLabelContent != null);

    public Dictionary<string, object> ThumbAttrs => new()
    {
        { "role", "slider" },
        { "tabindex", IsDisabled ? -1 : 0 }
    };

    public bool ShowThumbLabelContainer => IsFocused || IsActive || ThumbLabel == "always";

    protected override bool ValidateOnlyInFocusedState => false;

    protected virtual Task SetInternalValueAsync(double internalValue)
    {
        var val = RoundValue(Math.Min(Math.Max(internalValue, Min), Max));
        var value = Convert.ChangeType(val, typeof(TValue));
        InternalValue = (TValue)value;
        return Task.CompletedTask;
    }

    protected override void OnValueChanged(TValue val)
    {
        //Value may not between min and max
        //If that so,we should invoke ValueChanged 
        var roundedVal = ConvertDoubleToTValue<TValue>(RoundValue(Math.Min(Math.Max(Convert.ToDouble(val), Min), Max)));
        if (!EqualityComparer<TValue>.Default.Equals(roundedVal, InternalValue))
        {
            InternalValue = roundedVal;
        }
    }

    protected static T ConvertDoubleToTValue<T>(double val)
    {
        return (T)Convert.ChangeType(val, typeof(T));
    }

    private DotNetObjectReference<object>? _interopHandleReference;
    private TValue? _oldValue;
    private long _id;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OutsideClickJSModule.InitializeAsync(this, SliderElement.GetSelector());

            _interopHandleReference =
                DotNetObjectReference.Create<object>(new SliderInteropHandle<TValue, TNumeric>(this));
            _id = await Js.InvokeAsync<long>(JsInteropConstants.RegisterSliderEvents, SliderElement,
                _interopHandleReference);
        }
    }

    public virtual async Task HandleOnSliderClickAsync(MouseEventArgs args)
    {
        if (NoClick)
        {
            NoClick = false;
            return;
        }

        await ThumbElement.FocusAsync();
        await HandleOnMouseMoveAsync(args);
    }

    public virtual async Task HandleOnTouchStartAsync(ExTouchEventArgs args)
        => await HandleOnSliderStartSwiping(args.Target!, args.Touches[0].ClientX, args.Touches[0].ClientY);

    public virtual async Task HandleOnSliderMouseDownAsync(ExMouseEventArgs args)
        => await HandleOnSliderStartSwiping(args.Target!, args.ClientX, args.ClientY);

    protected async Task HandleOnSliderStartSwiping(EventTarget target, double clientX, double clientY)
    {
        _oldValue = InternalValue;
        IsActive = true;

        if (target.Class?.Contains("m-slider__thumb-container") ?? false)
        {
            ThumbPressed = true;
            var domRect = await Js.GetBoundingClientRectAsync($"{SliderElement.GetSelector()} .m-slider__thumb-container");
            StartOffset = Vertical
                ? (clientX - (domRect.Top + domRect.Height / 2))
                : (clientY - (domRect.Left + domRect.Width / 2));
        }
        else
        {
            StartOffset = 0;

            _mouseCancellationTokenSource?.Cancel();
            _mouseCancellationTokenSource = new CancellationTokenSource();
            await RunTaskInMicrosecondsAsync(() =>
            {
                ThumbPressed = true;
                return InvokeStateHasChangedAsync();
            }, 300, _mouseCancellationTokenSource.Token);
        }

        await OnStart.InvokeAsync(InternalValue);
    }

    public async Task HandleOnSliderEndSwiping()
    {
        _mouseCancellationTokenSource?.Cancel();
        ThumbPressed = false;

        await OnEnd.InvokeAsync(InternalValue);

        if (!EqualityComparer<TValue>.Default.Equals(_oldValue, InternalValue))
        {
            await OnChange.InvokeAsync(InternalValue);
            NoClick = true;
        }

        IsActive = false;

        StateHasChanged();
    }

    public virtual async Task HandleOnMouseMoveAsync(MouseEventArgs args)
    {
        if (args.Type == "mousemove")
        {
            ThumbPressed = true;
        }

        var val = await ParseMouseMoveAsync(args);

        await SetInternalValueAsync(val);

        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    protected async Task<double> ParseMouseMoveAsync(MouseEventArgs args)
    {
        var rect = await Js.GetBoundingClientRectAsync(TrackElement);

        var tractStart = Vertical ? rect.Top : rect.Left;
        var trackLength = Vertical ? rect.Height : rect.Width;
        var clickOffset = Vertical ? args.ClientY : args.ClientX;

        var clickPos = Math.Min(Math.Max((clickOffset - tractStart - StartOffset) / trackLength, 0), 1);

        if (Vertical)
        {
            clickPos = 1 - clickPos;
        }

        if (MasaBlazor.RTL)
        {
            clickPos = 1 - clickPos;
        }

        return Min + clickPos * (Max - Min);
    }

    protected double RoundValue(double value)
    {
        if (StepNumeric == 0)
        {
            return value;
        }

        var trimmedStep = Step.ToString()?.Trim();
        var decimals = trimmedStep != null && trimmedStep.IndexOf('.') > -1
            ? (trimmedStep.Length - trimmedStep.IndexOf('.') - 1)
            : 0;
        var offset = Min % StepNumeric;

        var newValue = Math.Round((value - offset) / StepNumeric) * StepNumeric + offset;
        var rounded = Math.Round(Math.Min(newValue, Max), decimals);
        if (rounded == 0)
        {
            //Avoid -0
            rounded = Math.Abs(rounded);
        }

        return rounded;
    }

    protected virtual bool IsThumbActive(int index)
    {
        return IsActive;
    }

    protected virtual bool IsThumbFocus(int index)
    {
        return IsFocused;
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    protected static readonly Block Block = new("m-slider");
    private ModifierBuilder _modifierBuilder = Block.CreateModifierBuilder();
    private static readonly Block _inputBlock = new("m-input__slider");
    private readonly ModifierBuilder _inputModifierBuilder = _inputBlock.CreateModifierBuilder();
    private readonly ModifierBuilder _thumbContainerModifierBuilder = Block.Element("thumb-container").CreateModifierBuilder();
    private readonly ModifierBuilder _tickModifierBuilder = Block.Element("tick").CreateModifierBuilder();
    private readonly ModifierBuilder _ticksContainerModifierBuilder = Block.Element("ticks-container").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                _inputModifierBuilder.Add(Vertical, InverseLabel).Build()
            }
        );
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            MasaBlazor.OnRTLChange += OnRTLChange;
        }
    }

    private void OnRTLChange(bool rtl)
    {
        InvokeStateHasChanged();
    }

    public virtual async Task HandleOnFocusAsync(int index, FocusEventArgs args)
    {
        IsFocused = true;

        if (OnFocus.HasDelegate)
        {
            await OnFocus.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnBlurAsync(int index, FocusEventArgs args)
    {
        IsFocused = false;

        if (OnBlur.HasDelegate)
        {
            await OnBlur.InvokeAsync(args);
        }
    }

    public virtual async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
    {
        if (!IsInteractive)
        {
            return;
        }

        var value = ParseKeyDown(args, DoubleInternalValue);

        if (value == null || value.AsT2 < Min || value.AsT2 > Max)
        {
            return;
        }

        await SetInternalValueAsync(value.AsT2);
        if (OnChange.HasDelegate)
        {
            await OnChange.InvokeAsync(InternalValue);
        }
    }

    protected StringNumber? ParseKeyDown(KeyboardEventArgs args, double value)
    {
        if (!IsInteractive) return null;

        var keyCodes = new[]
        {
            KeyCodes.PageUp,
            KeyCodes.PageDown,
            KeyCodes.End,
            KeyCodes.Home,
            KeyCodes.ArrowLeft,
            KeyCodes.ArrowRight,
            KeyCodes.ArrowDown,
            KeyCodes.ArrowUp
        };

        var directionCodes = new[]
        {
            KeyCodes.ArrowLeft,
            KeyCodes.ArrowRight,
            KeyCodes.ArrowDown,
            KeyCodes.ArrowUp,
        };

        if (!keyCodes.Contains(args.Key)) return null;

        var step = StepNumeric == 0 ? 1 : StepNumeric;
        var steps = Max - Min / step;
        if (directionCodes.Contains(args.Key))
        {
            var increase = MasaBlazor.RTL
                ? new[] { KeyCodes.ArrowLeft, KeyCodes.ArrowUp }
                : new[] { KeyCodes.ArrowRight, KeyCodes.ArrowUp };
            var direction = increase.Contains(args.Key) ? 1 : -1;
            var multiplier = args.ShiftKey ? 3 : (args.CtrlKey ? 2 : 1);

            value += (direction * step * multiplier);
        }
        else if (args.Key == KeyCodes.Home)
        {
            value = Min;
        }
        else if (args.Key == KeyCodes.End)
        {
            value = Max;
        }
        else
        {
            var direction = args.Key == KeyCodes.PageDown ? 1 : -1;
            value = value - (direction * step * (steps > 100 ? steps / 10 : 10));
        }

        return value;
    }

    public Task HandleOnOutsideClickAsync()
    {
        NextTick(async () =>
        {
            await HandleOnBlurAsync(0, new FocusEventArgs());
            StateHasChanged();
        });

        return Task.CompletedTask;
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await Js.InvokeVoidAsync(JsInteropConstants.UnregisterSliderEvents, SliderElement, _id);

        _interopHandleReference?.Dispose();

        await base.DisposeAsyncCore();
    }
}