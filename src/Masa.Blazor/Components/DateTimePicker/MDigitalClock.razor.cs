using BemIt;

namespace Masa.Blazor;

public partial class MDigitalClock<TValue> : BDomComponentBase
{
    [Parameter]
    public string? Color { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    [ApiDefaultValue(TimeFormat.AmPm)]
    public TimeFormat Format { get; set; } = TimeFormat.AmPm;

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    [ApiDefaultValue(1)]
    public int HourStep { get; set; } = 1;

    [Parameter]
    [ApiDefaultValue(1)]
    public int MinuteStep { get; set; } = 1;

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    [ApiDefaultValue(true)]
    public bool Ripple { get; set; } = true;

    [Parameter]
    [ApiDefaultValue(1)]
    public int SecondStep { get; set; } = 1;

    [Parameter]
    public bool UseSeconds { get; set; }

    [Parameter]
    public TValue? Value
    {
        get => GetValue<TValue?>();
        set => SetValue(value);
    }

    [Parameter]
    public bool Light { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    private Block Block => new("m-digital-clock");

    private ElementReference _hoursRef;
    private ElementReference _minutesRef;
    private ElementReference _secondsRef;

    private TimePeriod _timePeriod;

    private string? _activeItemClass;

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

    /// <summary>
    /// 24 hour format
    /// </summary>
    private int? Hour
    {
        get => GetValue<int?>();
        set => SetValue(value);
    }

    private int? Minute
    {
        get => GetValue<int?>();
        set => SetValue(value);
    }

    private int? Second
    {
        get => GetValue<int?>();
        set => SetValue(value);
    }

    /// <summary>
    /// Hour in 12 or 24 hour format, it depends on <see cref="Format"/> property.
    /// </summary>
    private int? ComputedHour => Format == TimeFormat.AmPm ? TimeHelper.Convert24To12(Hour ?? 0) : Hour;

    private string ActiveItemClass => _activeItemClass ??= Block.Element("item").Modifier("active").Build().Split(" ").Last();

    private bool IsRipple => !Disabled && !Readonly && Ripple;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            OnValueChanged(Value);
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<TValue?>(nameof(Value), OnValueChanged);
        watcher.Watch<int?>(nameof(Hour), () => OnInternalTimeChanged("h"));
        watcher.Watch<int?>(nameof(Minute), () => OnInternalTimeChanged("m"));
        watcher.Watch<int?>(nameof(Second), () => OnInternalTimeChanged("s"));
    }

    private void OnValueChanged(TValue? value)
    {
        if (value is TimeOnly time)
        {
            if (Format == TimeFormat.AmPm)
            {
                _timePeriod = time.Hour is 0 or < 12
                    ? TimePeriod.Am
                    : TimePeriod.Pm;
            }

            Hour = time.Hour;
            Minute = time.Minute;

            if (UseSeconds)
            {
                Second = time.Second;
            }
        }
        else
        {
            Hour = null;
            Minute = null;
            Second = null;
        }

        StateHasChanged();
    }

    private void OnInternalTimeChanged(string type)
    {
        var @ref = type switch
        {
            "h" => _hoursRef,
            "m" => _minutesRef,
            "s" => _secondsRef,
            _   => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };

        NextTick(() => { _ = Js.InvokeVoidAsync(JsInteropConstants.ScrollToActiveElement, @ref, $".{ActiveItemClass}", 4); });
    }

    /// <summary>
    /// Handle hour click
    /// </summary>
    /// <param name="hour">value in 12 or 24 hour format</param>
    private async Task HandleOnHourClick(int hour)
    {
        var formatHour = Format == TimeFormat.AmPm
            ? TimeHelper.Convert12To24(hour, _timePeriod)
            : hour;

        await OnInternalTimeClick(formatHour, Minute ?? 0, Second ?? 0);
    }

    private async Task HandleOnMinuteClick(int minute)
    {
        await OnInternalTimeClick(Hour ?? 0, minute, Second ?? 0);
    }

    private async Task HandleOnSecondClick(int second)
    {
        await OnInternalTimeClick(Hour ?? 0, Minute ?? 0, second);
    }

    private async Task OnInternalTimeClick(int hour, int minute, int second)
    {
        var time = new TimeOnly(hour, minute, second).ToString("O");

        if (BindConverter.TryConvertTo<TValue>(time, CultureInfo.InvariantCulture, out var value))
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }
    }

    private async Task OnPeriodClick(TimePeriod period)
    {
        _timePeriod = period;

        await HandleOnHourClick(ComputedHour ?? 0);
    }
}
