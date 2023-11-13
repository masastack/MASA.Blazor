namespace Masa.Blazor;

public partial class MDigitalClock<TValue> : BDomComponentBase, IAsyncDisposable
{
    [Inject] protected IntersectJSModule IntersectJSModule { get; set; } = null!;

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    /// <summary>
    /// Allowed hours, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

    /// <summary>
    /// Allowed Minutes, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

    /// <summary>
    /// Allowed Seconds, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

    /// <summary>
    /// Allowed times, only work when <see cref="MultiSection"/> is false. 
    /// </summary>
    [Parameter] public OneOf<Func<TimeOnly, bool>, List<TimeOnly>> AllowedTimes { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter]
    [MassApiParameter(TimeFormat.AmPm)]
    public TimeFormat Format { get; set; } = TimeFormat.AmPm;

    [Parameter] public StringNumber? Height { get; set; }

    /// <summary>
    /// The hour step (interval) of the time picker, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter]
    [MassApiParameter(1)]
    public int HourStep { get; set; } = 1;

    /// <summary>
    /// The minute step (interval) of the time picker, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter]
    [MassApiParameter(1)]
    public int MinuteStep { get; set; } = 1;

    [Parameter] public TimeOnly? Max { get; set; }

    [Parameter] public TimeOnly? Min { get; set; }

    /// <summary>
    /// Split the time picker into at least 2 sections:
    /// hour and minute, or hour, minute and second.
    /// </summary>
    [Parameter] public bool MultiSection { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter]
    [MassApiParameter(true)]
    public bool Ripple { get; set; } = true;

    /// <summary>
    /// The second step (interval) of the time picker, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter]
    [MassApiParameter(1)]
    public int SecondStep { get; set; } = 1;

    /// <summary>
    /// The time step (interval) of the time picker, only works when <see cref="MultiSection"/> is false.
    /// </summary>
    [Parameter]
    [MassApiParameter("TimeSpan.FromMinutes(30)")]
    public TimeSpan Step { get; set; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Determines whether to show the seconds section, only works when <see cref="MultiSection"/> is true.
    /// </summary>
    [Parameter] public bool UseSeconds { get; set; }

    [Parameter]
    public TValue? Value
    {
        get => GetValue<TValue?>();
        set => SetValue(value);
    }

    [Parameter] public bool Light { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }

    private Block Block => new("m-digital-clock");

    private ElementReference _hoursRef;
    private ElementReference _minutesRef;
    private ElementReference _secondsRef;
    private ElementReference _timesRef;

    private List<TimeOnly> _singleSectionItems = new();
    private TimeSpan? _previousStep;

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

    private TimeOnly? InternalTime { get; set; }

    /// <summary>
    /// Hour in 12 or 24 hour format, it depends on <see cref="Format"/> property.
    /// </summary>
    private int? ComputedHour => Format == TimeFormat.AmPm ? TimeHelper.Convert24To12(Hour ?? 0) : Hour;

    private bool ComputedUseSeconds => MultiSection && UseSeconds;

    private string ActiveItemClass => _activeItemClass ??= Block.Element("item").Modifier("active").Build().Split(" ").Last();

    private bool IsRipple => !Disabled && !Readonly && Ripple;

    private Func<int, bool>? IsAllowedHour24Callback
        => TimeHelper.IsAllowedHour24(AllowedHours, Max, Min);

    private Func<int, bool>? IsAllowedMinuteCallback
        => TimeHelper.IsAllowedMinute(IsAllowedHour24Callback, AllowedMinutes, Max, Min, Hour);

    private Func<int, bool>? IsAllowedSecondCallback
        => TimeHelper.IsAllowedSecond(IsAllowedHour24Callback, IsAllowedMinuteCallback, AllowedSeconds, Max, Min, Hour, Minute);

    private Func<int, bool>? IsAllowedHourCallback
        => TimeHelper.IsAllowedHourAmPm(IsAllowedHour24Callback, Format, _timePeriod);

    private Func<TimeOnly, bool>? IsAllowedTimeCallback
        => TimeHelper.IsAllowedTime(AllowedTimes, Max, Min);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!MultiSection && _previousStep != Step)
        {
            _previousStep = Step;
            _singleSectionItems.Clear();

            if (Step == default)
            {
                _singleSectionItems = Enumerable.Range(0, 24 * 60).Select(m => TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(m))).ToList();
            }
            else
            {
                var count = (int)Math.Ceiling(TimeOnly.MaxValue.ToTimeSpan().TotalMinutes / Step.TotalMinutes);
                for (var i = 0; i < count; i++)
                {
                    _singleSectionItems.Add(TimeOnly.FromTimeSpan(Step * i));
                }
            }
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _ = IntersectJSModule.ObserverAsync(Ref, _ =>
            {
                ScrollToActive("h");
                ScrollToActive("m");
                ScrollToActive("s");
                ScrollToActive();
                return Task.CompletedTask;
            });

            OnValueChanged(Value);
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<TValue?>(nameof(Value), OnValueChanged);
        watcher.Watch<int?>(nameof(Hour), () => ScrollToActive("h"));
        watcher.Watch<int?>(nameof(Minute), () => ScrollToActive("m"));
        watcher.Watch<int?>(nameof(Second), () => ScrollToActive("s"));
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

            Second = ComputedUseSeconds ? time.Second : 0;
        }
        else
        {
            Hour = null;
            Minute = null;
            Second = null;
        }

        if (!MultiSection)
        {
            NextTick(ScrollToActive);
        }

        InternalTime = new TimeOnly(Hour ?? 0, Minute ?? 0, Second ?? 0);

        StateHasChanged();
    }

    /// <summary>
    /// Scroll to active element for multi section
    /// </summary>
    /// <param name="type"></param>
    /// <exception cref="ArgumentException"></exception>
    private void ScrollToActive(string type)
    {
        if (!MultiSection)
        {
            return;
        }

        var @ref = type switch
        {
            "h" => _hoursRef,
            "m" => _minutesRef,
            "s" => _secondsRef,
            _   => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };

        NextTick(() => ScrollToActive(@ref));
    }

    /// <summary>
    /// Scroll to active element for single section
    /// </summary>
    private void ScrollToActive()
    {
        if (MultiSection)
        {
            return;
        }

        ScrollToActive(_timesRef);
    }

    private void ScrollToActive(ElementReference @ref)
    {
        _ = Js.InvokeVoidAsync(JsInteropConstants.ScrollToActiveElement, @ref, $".{ActiveItemClass}", 4);
    }

    /// <summary>
    /// Handle hour click
    /// </summary>
    /// <param name="hour">value in 12 or 24 hour format</param>
    private async Task HandleOnHourClick(int hour)
    {
        var hour24 = Format == TimeFormat.AmPm
            ? TimeHelper.Convert12To24(hour, _timePeriod)
            : hour;

        await OnInternalTimeClick(hour24, Minute ?? 0, Second ?? 0);
    }

    private async Task HandleOnMinuteClick(int minute)
    {
        await OnInternalTimeClick(Hour ?? 0, minute, Second ?? 0);
    }

    private async Task HandleOnSecondClick(int second)
    {
        await OnInternalTimeClick(Hour ?? 0, Minute ?? 0, second);
    }

    private async Task HandleOnTimeClick(TimeOnly selected)
    {
        await OnInternalTimeClick(selected.Hour, selected.Minute, selected.Second);

        NextTick(ScrollToActive);
    }

    private async Task OnInternalTimeClick(int hour, int minute, int second)
    {
        var time = new TimeOnly(hour, minute, second);

        if (Max.HasValue && time > Max.Value)
        {
            time = Max.Value;
        }
        else if (Min.HasValue && time < Min.Value)
        {
            time = Min.Value;
        }

        if (MultiSection)
        {
            if (IsAllowedHour24Callback?.Invoke(time.Hour) is false || IsAllowedMinuteCallback?.Invoke(time.Minute) is false ||
                IsAllowedSecondCallback?.Invoke(time.Second) is false)
            {
                return;
            }
        }
        else if (IsAllowedTimeCallback?.Invoke(time) is false)
        {
            return;
        }

        if (BindConverter.TryConvertTo<TValue>(time.ToString("O"), CultureInfo.InvariantCulture, out var value))
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

    private bool IsValidHour(int hour)
    {
        return IsAllowedHour24Callback?.Invoke(hour) ?? true;
    }

    private async Task OnPeriodClick(TimePeriod period)
    {
        _timePeriod = period;

        await HandleOnHourClick(ComputedHour ?? 0);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await IntersectJSModule.UnobserveAsync(Ref);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
