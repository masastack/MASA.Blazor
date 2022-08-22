using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobileDateTimePicker : MobilePickerBase<DateTimeColumn, DateTimeColumn, int, DateTime>
{
    [Parameter]
    public DateTime Max { get; set; }

    [Parameter]
    public DateTime Min { get; set; }

    [Parameter]
    public Func<DateTimeColumnType, int, string> Formatter { get; set; }

    [Parameter]
    public DateTimePickerPrecision Type { get; set; } = DateTimePickerPrecision.Day;

    public override List<DateTimeColumn> Columns { get; set; } = new();
    public override Func<DateTimeColumn, string> ItemText { get; set; }
    public override Func<DateTimeColumn, int> ItemValue { get; set; }
    public override Func<DateTimeColumn, List<DateTimeColumn>> ItemChildren { get; set; }

    private DateTime _prevMax;
    private DateTime _prevMin;

    // TODO: UtcNow?
    private DateTime Now { get; init; }
    private DateTime ValidDateTime { get; set; }

    public PMobileDateTimePicker()
    {
        Now = DateTime.Now;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ItemText = c => c.Text;
        ItemValue = c => c.Value;
        ItemChildren = c => c.Children;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Min == default) Min = Now.AddYears(-10);
        if (Max == default) Max = Now.AddYears(11);
        Formatter ??= (_, v) => v.ToString("00");

        var needChange = false;

        if (_prevMax != Max)
        {
            _prevMax = Max;
            needChange = true;
        }

        if (_prevMin != Min)
        {
            _prevMin = Min;
            needChange = true;
        }

        if (needChange)
        {
            ValidDateTime = ValidateValue();

            Columns = GetYears();
        }
    }

    protected override bool TryConvertInternalValueToValue(List<int> internalValue, out DateTime value)
    {
        value = default;

        if (internalValue is null || internalValue.Count == 0)
        {
            return false;
        }

        var datetime = new int[6];

        for (var i = 0; i < internalValue.Count; i++)
        {
            datetime[i] = internalValue[i];
        }

        var year   = datetime[0];
        var month  = datetime[1];
        var day    = datetime[2];
        var hour   = datetime[3];
        var minute = datetime[4];
        var second = datetime[5];

        year   =   year < 1 ? 1 : year;
        month  =  month < 1 ? 1 : month;
        day    =    day < 1 ? 1 : day;
        hour   =   hour < 0 ? 0 : hour;
        minute = minute < 0 ? 0 : minute;
        second = second < 0 ? 0 : second;

        value = new DateTime(year, month, day, hour, minute, second);
        return true;
    }

    protected override bool TryConvertValueToInternalValue(DateTime value, out List<int> internalValue)
    {
        internalValue = new();

        if (value == default)
        {
            return false;
        }

        var year   = value.Year;
        var month  = value.Month;
        var day    = value.Day;
        var hour   = value.Hour;
        var minute = value.Minute;
        var second = value.Second;

        internalValue.Add(year);
        internalValue.Add(month);
        internalValue.Add(day);
        internalValue.Add(hour);
        internalValue.Add(minute);
        internalValue.Add(second);

        return true;
    }

    private DateTime ValidateValue()
    {
        var dateTime = Value;

        if (Value == default)
        {
            dateTime = DateTime.Now;
        }

        if (dateTime > Max)
        {
            dateTime = Max;
        }

        if (dateTime < Min)
        {
            dateTime = Min;
        }

        return dateTime;
    }

    private List<DateTimeColumn> GetYears()
    {
        List<DateTimeColumn> yearList = new();

        var years = Enumerable.Range(Min.Year, Max.Year - Min.Year + 1).ToArray();

        for (int i = 0; i < years.Length; i++)
        {
            var year = years[i];
            var yearColumn = new DateTimeColumn(DateTimeColumnType.Year, year, Formatter(DateTimeColumnType.Year, year));
            if (Type == DateTimePickerPrecision.Year)
            {
                yearList.Add(yearColumn);
                continue;
            }

            if (year == ValidDateTime.Year)
            {
                List<DateTimeColumn> monthColumns;

                if (i == 0)
                {
                    monthColumns = GetMonths(year, Min.Month, 12);
                }
                else if (i == years.Length - 1)
                {
                    monthColumns = GetMonths(year, 1, Max.Month);
                }
                else
                {
                    monthColumns = GetMonths(year);
                }

                yearColumn.Children = monthColumns;
            }

            yearList.Add(yearColumn);
        }

        return yearList;
    }

    private List<DateTimeColumn> GetMonths(int year, int from = 1, int to = 12)
    {
        List<DateTimeColumn> monthList = new();

        var count = to - from + 1;
        var months = Enumerable.Range(from, count).ToArray();

        for (int i = 0; i < months.Length; i++)
        {
            var month = months[i];
            var monthColumn = new DateTimeColumn(DateTimeColumnType.Month, month, Formatter(DateTimeColumnType.Month, month));
            if (Type == DateTimePickerPrecision.Month)
            {
                monthList.Add(monthColumn);
                continue;
            }

            if (month == ValidDateTime.Month)
            {
                List<DateTimeColumn> dayColumns;

                if (i == 0)
                {
                    dayColumns = GetDays(year, month, Min.Day);
                }
                else if (i == months.Length - 1)
                {
                    dayColumns = GetDays(year, month, 1, Max.Day);
                }
                else
                {
                    dayColumns = GetDays(year, month);
                }

                monthColumn.Children = dayColumns;
            }

            monthList.Add(monthColumn);
        }

        return monthList;
    }

    private List<DateTimeColumn> GetDays(int year, int month, int from = 1, int to = 0)
    {
        if (to == 0)
        {
            to = DateTime.DaysInMonth(year, month);
        }

        var count = to - from + 1;
        var dayList = new List<DateTimeColumn>();

        var days = Enumerable.Range(from, count).ToArray();
        for (int i = 0; i < days.Length; i++)
        {
            var day = days[i];
            var dayColumn = new DateTimeColumn(DateTimeColumnType.Day, day, Formatter(DateTimeColumnType.Day, day));
            if (Type == DateTimePickerPrecision.Day)
            {
                dayList.Add(dayColumn);
                continue;
            }

            if (day == ValidDateTime.Day)
            {
                List<DateTimeColumn> hourColumns;
                if (i == 0)
                {
                    hourColumns = GetHours(Min.Hour, 24);
                }
                else if (i == days.Length - 1)
                {
                    hourColumns = GetHours(0, Max.Hour);
                }
                else
                {
                    hourColumns = GetHours();
                }

                dayColumn.Children = hourColumns;
            }

            dayList.Add(dayColumn);
        }

        return dayList;
    }

    private List<DateTimeColumn> GetHours(int from = 0, int to = 23)
    {
        var count = to - from + 1;
        var hourList = new List<DateTimeColumn>();

        var hours = Enumerable.Range(from, count).ToArray();
        for (int i = 0; i < hours.Length; i++)
        {
            var hour = hours[i];
            var hourColumn = new DateTimeColumn(DateTimeColumnType.Hour, hour, Formatter(DateTimeColumnType.Hour, hour));
            if (Type == DateTimePickerPrecision.Hour)
            {
                hourList.Add(hourColumn);
                continue;
            }

            if (hour == ValidDateTime.Hour)
            {
                List<DateTimeColumn> minuteColumns;
                if (i == 0)
                {
                    minuteColumns = GetMinutes(Min.Minute);
                }
                else if (i == hours.Length - 1)
                {
                    minuteColumns = GetMinutes(0, Max.Minute);
                }
                else
                {
                    minuteColumns = GetMinutes();
                }

                hourColumn.Children = minuteColumns;
            }

            hourList.Add(hourColumn);
        }

        return hourList;
    }

    private List<DateTimeColumn> GetMinutes(int from = 0, int to = 59)
    {
        var count = to - from + 1;
        var minuteList = new List<DateTimeColumn>();

        var minutes = Enumerable.Range(from, count).ToArray();
        for (int i = 0; i < minutes.Length; i++)
        {
            var minute = minutes[i];
            var minuteColumn = new DateTimeColumn(DateTimeColumnType.Minute, minute, Formatter(DateTimeColumnType.Minute, minute));
            if (Type == DateTimePickerPrecision.Minute)
            {
                minuteList.Add(minuteColumn);
                continue;
            }

            if (minute == ValidDateTime.Minute)
            {
                List<DateTimeColumn> secondColumns;
                if (i == 0)
                {
                    secondColumns = GetSeconds(Min.Second);
                }
                else if (i == minutes.Length - 1)
                {
                    secondColumns = GetSeconds(0, Max.Second);
                }
                else
                {
                    secondColumns = GetSeconds();
                }

                minuteColumn.Children = secondColumns;
            }

            minuteList.Add(minuteColumn);
        }

        return minuteList;
    }

    private List<DateTimeColumn> GetSeconds(int from = 0, int to = 59)
    {
        var count = to - from + 1;
        var seconds = Enumerable.Range(from, count).ToArray();
        return seconds.Select(second => new DateTimeColumn(DateTimeColumnType.Second, second, Formatter(DateTimeColumnType.Second, second))).ToList();
    }
}

public class DateTimeColumn
{
    public int Value { get; }

    public string Text { get; }

    public DateTimeColumnType Type { get; }

    public List<DateTimeColumn> Children { get; set; } = new();

    public DateTimeColumn(DateTimeColumnType type, int value, string text)
    {
        Value = value;
        Type = type;
        Text = text;
    }

    public DateTimeColumn(DateTimeColumnType type, int value, string text, List<DateTimeColumn> children) : this(type, value, text)
    {
        Children = children;
    }
}

public enum DateTimeColumnType
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second
}

public enum DateTimePickerPrecision
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,
}
