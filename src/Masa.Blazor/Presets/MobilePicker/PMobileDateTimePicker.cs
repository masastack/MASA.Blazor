using System.Text.Json;
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
    public DateTimePickerPrecision Precision { get; set; } = DateTimePickerPrecision.Day;

    public override List<DateTimeColumn> Columns { get; set; } = new();
    public override Func<DateTimeColumn, string> ItemText { get; set; }
    public override Func<DateTimeColumn, int> ItemValue { get; set; }
    public override Func<DateTimeColumn, List<DateTimeColumn>> ItemChildren { get; set; }

    private DateTime _prevMax;
    private DateTime _prevMin;

    // TODO: UtcNow?
    private DateTime Now { get; init; }
    // private DateTime Value { get; set; }

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
            Value = ValidateValue();

            Columns = GetYears(Value);
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

    protected override void HandleValueChanged(List<int> val)
    {
        Console.WriteLine(JsonSerializer.Serialize(val));

        int first = -1;
        var index = 0;
        if (val.Count > index)
        {
            first = val[index];
        }

        var column = Columns.FirstOrDefault(c => c.Value == first);

        index++;
        while (val.Count > index && column?.Children != null && column.Children.Any())
        {
            first = val[index++];
            column = column.Children.FirstOrDefault(c => c.Value == first);
        }

        if (column is { Children.Count: 0 })
        {
            if ((int)Precision >= index)
            {
                TryConvertInternalValueToValue(val, out var dateTime);
                column.Children = GetColumns((DateTimePickerPrecision)index, dateTime);
            }
        }

        base.HandleValueChanged(val);
    }

    private List<DateTimeColumn> GetColumns(DateTimePickerPrecision precision, DateTime dateTime)
    {
        return precision switch
        {
            DateTimePickerPrecision.Year => GetYears(dateTime),
            DateTimePickerPrecision.Month => GetMonths(dateTime),
            DateTimePickerPrecision.Day => GetDays(dateTime),
            DateTimePickerPrecision.Hour => GetHours(dateTime),
            DateTimePickerPrecision.Minute => GetMinutes(dateTime),
            DateTimePickerPrecision.Second => GetSeconds(dateTime),
            _ => throw new ArgumentOutOfRangeException(nameof(precision), precision, null)
        };
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

    private List<DateTimeColumn> GetYears(DateTime dateTime)
    {
        List<DateTimeColumn> yearList = new();

        var years = Enumerable.Range(Min.Year, Max.Year - Min.Year + 1).ToArray();

        foreach (var year in years)
        {
            var yearColumn = new DateTimeColumn(DateTimeColumnType.Year, year, Formatter(DateTimeColumnType.Year, year));
            if (Precision == DateTimePickerPrecision.Year)
            {
                yearList.Add(yearColumn);
                continue;
            }

            if (year == dateTime.Year)
            {
                yearColumn.Children = GetMonths(dateTime);
            }

            yearList.Add(yearColumn);
        }

        return yearList;
    }

    private List<DateTimeColumn> GetMonths(DateTime dateTime)
    {
        List<DateTimeColumn> monthList = new();

        var from = 1;
        var to = 12;

        if (dateTime.Year == Min.Year)
        {
            from = Min.Month;
        }

        if (dateTime.Year == Max.Year)
        {
            to = Max.Month;
        }

        var count = to - from + 1;
        var months = Enumerable.Range(from, count).ToArray();

        foreach (var month in months)
        {
            var monthColumn = new DateTimeColumn(DateTimeColumnType.Month, month, Formatter(DateTimeColumnType.Month, month));
            if (Precision == DateTimePickerPrecision.Month)
            {
                monthList.Add(monthColumn);
                continue;
            }

            if (month == dateTime.Month)
            {
                monthColumn.Children = GetDays(dateTime);
            }

            monthList.Add(monthColumn);
        }

        return monthList;
    }

    private List<DateTimeColumn> GetDays(DateTime dateTime)
    {
        var from = 1;
        var to = 0;

        if (dateTime.Year == Min.Year && dateTime.Month == Min.Month)
        {
            from = Min.Day;
        }

        if (dateTime.Year == Max.Year && dateTime.Month == Max.Month)
        {
            to = Max.Day;
        }

        if (to == 0)
        {
            to = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        var count = to - from + 1;
        var dayList = new List<DateTimeColumn>();

        var days = Enumerable.Range(from, count).ToArray();
        foreach (var day in days)
        {
            var dayColumn = new DateTimeColumn(DateTimeColumnType.Day, day, Formatter(DateTimeColumnType.Day, day));
            if (Precision == DateTimePickerPrecision.Day)
            {
                dayList.Add(dayColumn);
                continue;
            }

            if (day == dateTime.Day)
            {
                dayColumn.Children = GetHours(dateTime);
            }

            dayList.Add(dayColumn);
        }

        return dayList;
    }

    private List<DateTimeColumn> GetHours(DateTime dateTime)
    {
        var from = 0;
        var to = 23;

        if (dateTime.Year == Min.Year && dateTime.Month == Min.Month && dateTime.Day == Min.Day)
        {
            from = Min.Hour;
        }

        if (dateTime.Year == Max.Year && dateTime.Month == Max.Month && dateTime.Day == Max.Day)
        {
            to = Max.Hour;
        }

        var count = to - from + 1;
        var hourList = new List<DateTimeColumn>();

        var hours = Enumerable.Range(from, count).ToArray();
        foreach (var hour in hours)
        {
            var hourColumn = new DateTimeColumn(DateTimeColumnType.Hour, hour, Formatter(DateTimeColumnType.Hour, hour));
            if (Precision == DateTimePickerPrecision.Hour)
            {
                hourList.Add(hourColumn);
                continue;
            }

            if (hour == dateTime.Hour)
            {
                hourColumn.Children = GetMinutes(dateTime);
            }

            hourList.Add(hourColumn);
        }

        return hourList;
    }

    private List<DateTimeColumn> GetMinutes(DateTime dateTime)
    {
        var from = 0;
        var to = 59;

        if (dateTime.Year == Min.Year && dateTime.Month == Min.Month && dateTime.Day == Min.Day && dateTime.Hour == Min.Hour)
        {
            from = Min.Minute;
        }

        if (dateTime.Year == Max.Year && dateTime.Month == Max.Month && dateTime.Day == Max.Day && dateTime.Hour == Max.Hour)
        {
            to = Max.Minute;
        }

        var count = to - from + 1;
        var minuteList = new List<DateTimeColumn>();

        var minutes = Enumerable.Range(from, count).ToArray();
        foreach (var minute in minutes)
        {
            var minuteColumn = new DateTimeColumn(DateTimeColumnType.Minute, minute, Formatter(DateTimeColumnType.Minute, minute));
            if (Precision == DateTimePickerPrecision.Minute)
            {
                minuteList.Add(minuteColumn);
                continue;
            }

            if (minute == dateTime.Minute)
            {
                minuteColumn.Children = GetSeconds(dateTime);
            }

            minuteList.Add(minuteColumn);
        }

        return minuteList;
    }

    private List<DateTimeColumn> GetSeconds(DateTime dateTime)
    {
        var from = 0;
        var to = 59;

        // TODO: refactor this
        if (dateTime.Year == Min.Year && dateTime.Month == Min.Month && dateTime.Day == Min.Day && dateTime.Hour == Min.Hour &&
            dateTime.Minute == Min.Minute)
        {
            from = Min.Second;
        }

        if (dateTime.Year == Max.Year && dateTime.Month == Max.Month && dateTime.Day == Max.Day && dateTime.Hour == Max.Hour &&
            dateTime.Minute == Max.Minute)
        {
            to = Max.Second;
        }

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
