namespace Masa.Blazor.Presets;

public partial class PDateTimeRangePickerView
{
    [Parameter] public Func<DateOnly, bool>? AllowedDates { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public Func<DateOnly, string>? DayFormat { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public int FirstDayOfWeek { get; set; }

    [Parameter]
    [MasaApiParameter(TimeFormat.AmPm)]
    public TimeFormat TimeFormat { get; set; } = TimeFormat.AmPm;

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
    [MasaApiParameter("$next")]
    public string NextIcon { get; set; } = "$next";

    [Parameter]
    [MasaApiParameter("$prev")]
    public string PrevIcon { get; set; } = "$prev";

    [Parameter] public bool Reactive { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter] public bool ShowAdjacentMonths { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public OneOf<DateOnly, bool> ShowCurrent { get; set; } = true;

    [Parameter] public bool ShowWeek { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool UseSeconds { get; set; } = true;

    [Parameter] public DateTime? Start { get; set; }

    [Parameter] public EventCallback<DateTime?> StartChanged { get; set; }

    [Parameter] public DateTime? End { get; set; }

    [Parameter] public EventCallback<DateTime?> EndChanged { get; set; }

    [Parameter] public Func<DateOnly, string>? WeekdayFormat { get; set; }

    [Parameter] public Func<DateOnly, string>? YearFormat { get; set; }

    [Parameter] public string? YearIcon { get; set; }

    [Parameter] public string? Theme { get; set; }

    [Parameter] public OneOf<Func<TimeOnly, bool>, List<TimeOnly>> AllowedTimes { get; set; }

    [Parameter] public bool MultiSection { get; set; }

    [Parameter] [MasaApiParameter("TimeSpan.FromMinutes(30)")]
    public TimeSpan Step { get; set; } = TimeSpan.FromMinutes(30);

    [Parameter] [MasaApiParameter(1)]
    public int HourStep { get; set; } = 1;

    [Parameter] [MasaApiParameter(1)]
    public int MinuteStep { get; set; } = 1;

    [Parameter] [MasaApiParameter(1)]
    public int SecondStep { get; set; } = 1;

    private static Block _block = new("m-date-time-picker__view");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    private DateOnly _tableDate = DateOnly.FromDateTime(System.DateTime.Now);
    private List<DateTime?> _prevValue = [null, null];
    protected List<DateOnly?> InternalDate = [null, null];
    protected List<TimeOnly?> InternalTime = [null, null];
    private bool IsEndView => _currentIndex == 1;
    private int _currentIndex;
    private TimeOnly? CurrentTime => InternalTime[_currentIndex];

    private DateTime? _min;
    private DateTime? _max;
    protected DateOnly? MaxDate;
    protected DateOnly? MinDate;
    protected TimeOnly? MaxTime;
    protected TimeOnly? MinTime;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var date = IsEndView ? End : Start;
        var index = IsEndView ? 1 : 0;
        var prevDate = _prevValue[index];

        if (date is null)
        {
            if (prevDate != null)
            {
                UpdateInternalDateTime(null);
            }
        }
        else
        {
            if (prevDate != date)
            {
                UpdateInternalDateTime(date);
            }
        }
    }

    private void UpdateInternalDateTime(DateTime? value)
    {
        var index = IsEndView ? 1 : 0;

        _prevValue[index] = value;

        if (value is null || value.Value == default)
        {
            InternalDate[index] = null;
            InternalTime[index] = null;
        }
        else
        {
            InternalDate[index] = System.DateOnly.FromDateTime(value.Value);
            InternalTime[index] = System.TimeOnly.FromDateTime(value.Value);
        }
    }

    private async Task OnDateClick(DateOnly date)
    {
        var currentDate = InternalDate[_currentIndex];
        if (currentDate.HasValue && currentDate.Value == date)
        {
            return;
        }

        InternalDate[_currentIndex] = date;
        _tableDate = date;
        Console.Out.WriteLine("[PDateTimeRangePickerView] OnDateClick: " + date);

        await UpdateValue();
    }
    
    private void OnPickerDateUpdate(DateOnly date)
    {
        Console.Out.WriteLine("[PDateTimeRangePickerView] OnPickerDateUpdate: " + date);
        _tableDate = date;
    }
    
    protected async Task TimeChanged(TimeOnly? time)
    {
        if (!time.HasValue)
        {
            return;
        }

        var currentTime = InternalTime[_currentIndex];
        if (currentTime.HasValue && currentTime == time.Value)
        {
            return;
        }

        InternalTime[_currentIndex] = time;

        await UpdateValue();
    }

    private async Task UpdateValue()
    {
        var newValue = MergeDateAndTime();

        if (IsEndView)
        {
            if (Start > newValue)
            {
                await StartChanged.InvokeAsync(newValue);
                await EndChanged.InvokeAsync(Start);
                (InternalDate[0], InternalDate[1]) = (InternalDate[1], InternalDate[0]);
            }
            else
            {
                await EndChanged.InvokeAsync(newValue);
            }
        }
        else
        {
            if (End < newValue)
            {
                await EndChanged.InvokeAsync(newValue);
                await StartChanged.InvokeAsync(End);
                (InternalDate[0], InternalDate[1]) = (InternalDate[1], InternalDate[0]);
            }
            else
            {
                await StartChanged.InvokeAsync(newValue);
            }
        }
    }
    
    private void ToggleTo(int index)
    {
        _currentIndex = index;
        var date = IsEndView ? End : Start;

        if (date.HasValue)
        {
            _tableDate = DateOnly.FromDateTime(date.Value);
        }

        UpdateInternalDateTime(date);
    }

    private DateTime MergeDateAndTime()
    {
        int year, month, day, hour, minute, second;

        var now = DateTime.Now;

        var date = InternalDate[_currentIndex];
        var time = InternalTime[_currentIndex];

        if (date.HasValue)
        {
            year = date.Value.Year;
            month = date.Value.Month;
            day = date.Value.Day;
        }
        else
        {
            year = now.Year;
            month = now.Month;
            day = now.Day;
        }

        if (time.HasValue)
        {
            hour = time.Value.Hour;
            minute = time.Value.Minute;
            second = UseSeconds ? time.Value.Second : 0;
        }
        else
        {
            hour = now.Hour;
            minute = now.Minute;
            second = UseSeconds ? now.Second : 0;
        }

        return new DateTime(year, month, day, hour, minute, second);
    }
}