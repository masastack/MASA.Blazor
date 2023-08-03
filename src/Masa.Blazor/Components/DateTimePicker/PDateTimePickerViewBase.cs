namespace Masa.Blazor.Presets;

public partial class PDateTimePickerViewBase<TValue>: ComponentBase
{
    [Parameter] public DatePickerType? ActiveDatePicker { get; set; }

    [Parameter] public Func<DateOnly, bool>? AllowedDates { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public Func<DateOnly, bool>? DayFormat { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public int FirstDayOfWeek { get; set; }

    [Parameter]
    [ApiDefaultValue(TimeFormat.AmPm)]
    public TimeFormat TimeFormat { get; set; } = TimeFormat.AmPm;

    [Parameter] public Func<DateOnly, string>? HeaderDateFormat { get; set; }

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
                MinDate = System.DateOnly.FromDateTime(value.Value);
                MinTime = System.TimeOnly.FromDateTime(value.Value);
            }
            else
            {
                MinDate = null;
                MinTime = null;
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
                MaxDate = System.DateOnly.FromDateTime(value.Value);
                MaxTime = System.TimeOnly.FromDateTime(value.Value);
            }
            else
            {
                MaxDate = null;
                MaxTime = null;
            }
        }
    }

    [Parameter] public Func<DateOnly, string>? MonthFormat { get; set; }

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
    protected DateOnly? DateOnly;
    protected TimeOnly? TimeOnly;

    private DateTime? _min;
    private DateTime? _max;
    protected DateOnly? MaxDate;
    protected DateOnly? MinDate;
    protected TimeOnly? MaxTime;
    protected TimeOnly? MinTime;

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
            DateOnly = null;
            TimeOnly = null;
        }
        else
        {
            DateOnly = System.DateOnly.FromDateTime(value.Value);
            TimeOnly = System.TimeOnly.FromDateTime(value.Value);
        }
    }

    protected async Task DateChanged(DateOnly? date)
    {
        if (!date.HasValue || date.Value == default)
        {
            return;
        }

        DateOnly = date;

        await UpdateValue();
    }

    protected async Task TimeChanged(TimeOnly? time)
    {
        if (!time.HasValue || time.Value == default)
        {
            return;
        }

        TimeOnly = time;

        await UpdateValue();
    }

    protected async Task OnHourClick(int hour)
    {
        TimeOnly = TimeOnly.HasValue
            ? new TimeOnly(hour, TimeOnly.Value.Minute, TimeOnly.Value.Second)
            : new TimeOnly(hour, 0, 0);

        await UpdateValue();
    }

    private async Task UpdateValue()
    {
        var newValue = MergeDateAndTime().ToString("O");

        if (BindConverter.TryConvertTo<TValue>(newValue, CultureInfo.InvariantCulture, out var value))
        {
            await ValueChanged.InvokeAsync(value);
        }
    }

    private DateTime MergeDateAndTime()
    {
        int year, month, day, hour, minute, second;

        var now = DateTime.Now;

        if (DateOnly.HasValue && DateOnly.Value != default)
        {
            year = DateOnly.Value.Year;
            month = DateOnly.Value.Month;
            day = DateOnly.Value.Day;
        }
        else
        {
            year = now.Year;
            month = now.Month;
            day = now.Day;
        }

        if (TimeOnly.HasValue)
        {
            hour = TimeOnly.Value.Hour;
            minute = TimeOnly.Value.Minute;
            second = TimeOnly.Value.Second;
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
