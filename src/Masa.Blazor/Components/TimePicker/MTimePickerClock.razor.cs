namespace Masa.Blazor.Components.TimePicker;

public partial class MTimePickerClock : MasaComponentBase
{
    [Parameter]
    public int? Value
    {
        get => GetValue<int?>();
        set => SetValue(value);
    }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public int Rotate { get; set; }

    [Parameter] public bool Double { get; set; }

    [Parameter] public int Max { get; set; }

    [Parameter] public int Min { get; set; }

    [Parameter] public Func<int, bool>? AllowedValues { get; set; }

    [Parameter] public EventCallback<(int, SelectingTime)> OnInput { get; set; }

    [Parameter] public EventCallback<int> OnChange { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public int Step { get; set; } = 1;

    [Parameter] public Func<int, string>? Format { get; set; }

    [Parameter] public SelectingTime Selecting { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }


    protected int DisplayedValue => Value ?? Min;

    protected async Task HandleOnWheelAsync(WheelEventArgs args)
    {
        if (Readonly || Disabled)
        {
            return;
        }

        if (!Scrollable)
        {
            return;
        }

        var delta = Math.Sign(-args.DeltaY == 0 ? 1 : -args.DeltaY);
        var value = DisplayedValue;
        do
        {
            value = value + delta;
            value = (value - Min + Count) % Count + Min;
        } while (!IsAllowed(value) && value != DisplayedValue);

        if (value != DisplayedValue)
        {
            await UpdateAsync(value, Selecting);
        }
    }

    protected int? ValueOnMouseDown { get; set; }

    protected int? ValueOnMouseUp { get; set; }

    protected bool IsDragging { get; set; }

    public ElementReference InnerClockElement { get; set; }

    protected double InnerRadiusScale => 0.62;

    protected int Count => Max - Min + 1;

    protected double DegreesPerUnit => 360d / RoundCount;

    protected int RoundCount => Double ? (Count / 2) : Count;

    protected int? InputValue { get; set; }

    protected double Degrees => DegreesPerUnit * Math.PI / 180;

    private bool NonInteractive => Readonly || Disabled;

    private async Task OnMouseDown(object args)
    {
        if (NonInteractive) return;

        ValueOnMouseDown = null;
        ValueOnMouseUp = null;
        IsDragging = true;

        await OnDragMove(args);
    }

    private async Task OnDragMove(object args)
    {
        if (NonInteractive) return;

        if (Ref.Context is null || InnerClockElement.Context is null) return;

        string type;
        double clientX, clientY;
        switch (args)
        {
            case MouseEventArgs mouseEventArgs:
                type = mouseEventArgs.Type;
                clientX = mouseEventArgs.ClientX;
                clientY = mouseEventArgs.ClientY;
                break;
            case TouchEventArgs touchEventArgs:
                type = touchEventArgs.Type;
                clientX = touchEventArgs.Touches[0].ClientX;
                clientY = touchEventArgs.Touches[0].ClientY;
                break;
            default:
                return;
        }

        if (!IsDragging && type != "click") return;

        var selecting = Selecting;

        var clockRect = await GetBoundingClientRectAsync(Ref);
        var width = clockRect.Width;
        var top = clockRect.Top;
        var left = clockRect.Left;

        var innerClockRect = await GetBoundingClientRectAsync(InnerClockElement);
        var innerWidth = innerClockRect.Width;

        var center = (X: width / 2, Y: -width / 2);
        var coords = (X: clientX - left, Y: top - clientY);
        var handAngle = Math.Round(Angle(center, coords) - Rotate + 360) % 360;
        var insideClick = Double && Euclidean(center, coords) < (innerWidth + innerWidth * InnerRadiusScale) / 4;
        var checksCount = Math.Ceiling(15 / DegreesPerUnit);

        for (int i = 0; i < checksCount; i++)
        {
            var value = AngleToValue(handAngle + i * DegreesPerUnit, insideClick);
            if (IsAllowed(value))
            {
                await SetMouseDownValueAsync(value, selecting);
                return;
            }

            value = AngleToValue(handAngle - i * DegreesPerUnit, insideClick);
            if (IsAllowed(value))
            {
                await SetMouseDownValueAsync(value, selecting);
                return;
            }
        }
    }

    protected async Task OnMouseUp()
    {
        if (NonInteractive) return;

        IsDragging = false;

        if (ValueOnMouseUp == null || !IsAllowed(ValueOnMouseUp.Value)) return;

        await OnChange.InvokeAsync(ValueOnMouseUp.Value);
    }

    protected async Task OnMouseLeave()
    {
        if (NonInteractive) return;

        if (IsDragging)
        {
            await OnMouseUp();
        }
    }

    private ValueTask<BoundingClientRect> GetBoundingClientRectAsync(ElementReference element)
    {
        return Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, element, ".m-application");
    }

    private async Task SetMouseDownValueAsync(int value, SelectingTime selecting)
    {
        ValueOnMouseDown ??= value;
        ValueOnMouseUp = value;

        await UpdateAsync(value, selecting);
    }

    private async Task UpdateAsync(int value, SelectingTime selecting)
    {
        if (InputValue != value)
        {
            InputValue = value;
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync((value, selecting));
            }
        }
    }

    private bool IsAllowed(int value)
    {
        return AllowedValues == null || AllowedValues(value);
    }

    private int AngleToValue(double angel, bool insideClick)
    {
        var value = (Math.Round(angel / DegreesPerUnit) + (insideClick ? RoundCount : 0)) % Count + Min;

        if (angel < (360 - DegreesPerUnit / 2))
        {
            return Convert.ToInt32(value);
        }

        return Convert.ToInt32(insideClick ? Max - RoundCount + 1 : Min);
    }

    private double Angle((double X, double Y) center, (double X, double Y) p1)
    {
        var value = 2 * Math.Atan2(p1.Y - center.Y - Euclidean(center, p1), p1.X - center.X);
        return Math.Abs(value * 180 / Math.PI);
    }

    private double Euclidean((double X, double Y) p0, (double X, double Y) p1)
    {
        var dx = p1.X - p0.X;
        var dy = p1.Y - p0.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    private bool IsInner(int? value)
    {
        return Double && (value - Min >= RoundCount);
    }

    private double HandScale(int value)
    {
        return IsInner(value) ? InnerRadiusScale : 1;
    }

    private (double, double) GetPosition(int value)
    {
        var rotateRadians = Rotate * Math.PI / 180;
        var x = Math.Sin((value - Min) * Degrees + rotateRadians) * HandScale(value);
        var y = -Math.Cos((value - Min) * Degrees + rotateRadians) * HandScale(value);
        return (x, y);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<int?>(nameof(Value), val => { InputValue = val; });
    }

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

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

    private static Block _block = new("m-time-picker-clock");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _handModifierBuilder = _block.Element("hand").CreateModifierBuilder();
    private ModifierBuilder _itemModifierBuilder = _block.Element("item").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("indeterminate", Value == null)
            .AddTheme(IsDark, IndependentTheme)
            .Build();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = Js.AddHtmlElementEventListener<TouchEventArgs>(Ref, "touchstart", OnMouseDown,
                new EventListenerOptions { Passive = false }, new EventListenerExtras { PreventDefault = true });

            _ = Js.AddHtmlElementEventListener<TouchEventArgs>(Ref, "touchmove", OnDragMove,
                new EventListenerOptions { Passive = false }, new EventListenerExtras { PreventDefault = true });

            if (Scrollable)
            {
                _ = Js.AddHtmlElementEventListener<WheelEventArgs>(
                    Ref,
                    "wheel",
                    HandleOnWheelAsync,
                    false,
                    new EventListenerExtras { PreventDefault = true });
            }
        }
    }
}