namespace Masa.Blazor.Presets;

public partial class PDateTimePickerView<TValue>
{
    [Parameter] public DatePickerType? ActivePicker { get; set; }

    [Parameter] public Func<DateOnly, bool>? AllowedDates { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

    [Parameter] public bool AmPmInTitle { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public Func<DateOnly, bool>? DayFormat { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public int FirstDayOfWeek { get; set; }

    [Parameter]
    [ApiDefaultValue(TimeFormat.AmPm)]
    public TimeFormat TimeFormat { get; set; } = TimeFormat.AmPm;

    [Parameter] public Func<DateOnly, string>? HeaderDateFormat { get; set; }

    [Parameter] public string? HeaderColor { get; set; }

    [Parameter] public string? Locale { get; set; }

    [Parameter]
    public DateTime? Min
    {
        get => _min;
        set
        {
            _min = value;

            if (value.HasValue)
            {
                _minDate = DateOnly.FromDateTime(value.Value);
                _minTime = TimeOnly.FromDateTime(value.Value);
            }
            else
            {
                _minDate = null;
                _minTime = null;
            }
        }
    }

    [Parameter]
    public DateTime? Max
    {
        get => _max;
        set
        {
            _max = value;

            if (value.HasValue)
            {
                _maxDate = DateOnly.FromDateTime(value.Value);
                _maxTime = TimeOnly.FromDateTime(value.Value);
            }
            else
            {
                _maxDate = null;
                _maxTime = null;
            }
        }
    }

    [Parameter] public Func<DateOnly, string>? MonthFormat { get; set; }

    [Parameter] public bool NoTitle { get; set; }

    [Parameter]
    [ApiDefaultValue("$next")]
    public string NextIcon { get; set; } = "$next";

    [Parameter]
    [ApiDefaultValue("$prev")]
    public string PrevIcon { get; set; } = "$prev";

    [Parameter] public bool Reactive { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter] public bool ShowAdjacentMonths { get; set; }

    [Parameter]
    [ApiDefaultValue(true)]
    public OneOf<DateOnly, bool> ShowCurrent { get; set; } = true;

    [Parameter] public bool ShowWeek { get; set; }

    [Parameter] public Func<IList<DateOnly>, string>? TitleDateFormat { get; set; }

    [Parameter]
    [ApiDefaultValue(true)]
    public bool UseSeconds { get; set; } = true;

    [Parameter] public TValue? Value { get; set; }

    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }

    [Parameter] public Func<DateOnly, string>? WeekdayFormat { get; set; }

    [Parameter] public Func<DateOnly, string>? YearFormat { get; set; }

    [Parameter] public string? YearIcon { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    private DateTime? _prevValue;
    private DateOnly? _dateOnly;
    private TimeOnly? _timeOnly;

    private DateTime? _min;
    private DateTime? _max;
    private DateOnly? _maxDate;
    private DateOnly? _minDate;
    private TimeOnly? _maxTime;
    private TimeOnly? _minTime;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Value is null)
        {
            if (_prevValue != null)
            {
                UpdateInternalDateTime(null);
            }
        }
        else if (Value is DateTime value)
        {
            if (_prevValue != value)
            {
                UpdateInternalDateTime(value);
            }
        }
    }

    private void UpdateInternalDateTime(DateTime? value)
    {
        _prevValue = value;

        if (value is null || value.Value == default)
        {
            _dateOnly = null;
            _timeOnly = null;
        }
        else
        {
            _dateOnly = DateOnly.FromDateTime(value.Value);
            _timeOnly = TimeOnly.FromDateTime(value.Value);
        }
    }

    private async Task DateChanged(DateOnly? date)
    {
        if (!date.HasValue || date.Value == default)
        {
            return;
        }

        _dateOnly = date;

        await UpdateValue();
    }

    private async Task TimeChanged(TimeOnly? time)
    {
        if (!time.HasValue || time.Value == default)
        {
            return;
        }

        _timeOnly = time;

        await UpdateValue();
    }

    private async Task OnHourClick(int hour)
    {
        _timeOnly = _timeOnly.HasValue
            ? new TimeOnly(hour, _timeOnly.Value.Minute, _timeOnly.Value.Second)
            : new TimeOnly(hour, 0, 0);

        await UpdateValue();
    }

    private async Task UpdateValue()
    {
        var newValue = MergeDateAndTime().ToString(CultureInfo.InvariantCulture);

        if (BindConverter.TryConvertTo<TValue>(newValue, CultureInfo.InvariantCulture, out var value))
        {
            await ValueChanged.InvokeAsync(value);
        }
    }

    private DateTime MergeDateAndTime()
    {
        int year, month, day, hour, minute, second;

        var now = DateTime.Now;

        if (_dateOnly.HasValue && _dateOnly.Value != default)
        {
            year = _dateOnly.Value.Year;
            month = _dateOnly.Value.Month;
            day = _dateOnly.Value.Day;
        }
        else
        {
            year = now.Year;
            month = now.Month;
            day = now.Day;
        }

        if (_timeOnly.HasValue)
        {
            hour = _timeOnly.Value.Hour;
            minute = _timeOnly.Value.Minute;
            second = _timeOnly.Value.Second;
        }
        else
        {
            hour = now.Hour;
            minute = now.Minute;
            second = now.Second;
        }

        return new DateTime(year, month, day, hour, minute, second);
    }
}
