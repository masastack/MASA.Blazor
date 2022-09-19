using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobileDateTimePicker : MobilePickerBase<DateTimeColumn, DateTimeColumn, int, DateTime>
{
    [Parameter]
    public DateTime Max { get; set; }

    [Parameter]
    public DateTime Min { get; set; }

    [Parameter]
    public Func<DateTimePrecision, int, string> Formatter { get; set; }

    [Parameter]
    public DateTimePrecision Precision { get; set; } = DateTimePrecision.Second;

    private DateTime _prevMax;
    private DateTime _prevMin;

    private DateTime Now { get; init; }

    public PMobileDateTimePicker()
    {
        Now = DateTime.Now;
    }

    protected override string ClassPrefix => "p-mobile-datetime-picker";

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
            Value = GetDateTimeWithinBoundary(Value);
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

        var daysInMonth = DateTime.DaysInMonth(year, month);
        if (day > daysInMonth)
        {
            day = daysInMonth;
        }

        value = new DateTime(
            year,
            Precision >= DateTimePrecision.Month ? month : 1,
            Precision >= DateTimePrecision.Day ? day : 1,
            Precision >= DateTimePrecision.Hour ? hour : 0,
            Precision >= DateTimePrecision.Minute ? minute : 0,
            Precision >= DateTimePrecision.Second ? second : 0
        );

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
        int first = -1;
        var index = 0;
        if (val.Count > index)
        {
            first = val[index];
        }

        TryConvertInternalValueToValue(val, out var dateTime);
        dateTime = GetDateTimeWithinBoundary(dateTime);
        TryConvertValueToInternalValue(dateTime, out val);

        var column = Columns.FirstOrDefault(c => c.Value == first);

        index++;
        while (val.Count > index && column?.Children != null && column.Children.Any())
        {
            first = val[index];
            column = column.Children.FirstOrDefault(c => c.Value == first);
            index++;
        }

        dateTime = GetDateTimeWithinBoundary(dateTime);

        if (column is { Children.Count: 0 })
        {
            if ((int)Precision >= index)
            {
                column.Children = GetColumns((DateTimePrecision)index, dateTime);
            }
        }

        base.HandleValueChanged(val);
    }

    private List<DateTimeColumn> GetColumns(DateTimePrecision precision, DateTime dateTime)
    {
        return precision switch
        {
            DateTimePrecision.Year => GetYears(dateTime),
            DateTimePrecision.Month => GetMonths(dateTime),
            DateTimePrecision.Day => GetDays(dateTime),
            DateTimePrecision.Hour => GetHours(dateTime),
            DateTimePrecision.Minute => GetMinutes(dateTime),
            DateTimePrecision.Second => GetSeconds(dateTime),
            _ => throw new ArgumentOutOfRangeException(nameof(precision), precision, null)
        };
    }

    private DateTime GetDateTimeWithinBoundary(DateTime input)
    {
        var dateTime = input;

        if (input == default)
        {
            dateTime = Now;
        }

        if (dateTime < Min)
        {
            dateTime = Min;
        }
        else if (dateTime > Max)
        {
            dateTime = Max;
        }

        return dateTime;
    }

    private List<DateTimeColumn> GetYears(DateTime dateTime)
    {
        List<DateTimeColumn> yearList = new();

        var years = Enumerable.Range(Min.Year, Max.Year - Min.Year + 1).ToArray();

        foreach (var year in years)
        {
            var yearColumn = new DateTimeColumn(DateTimePrecision.Year, year, Formatter);
            if (Precision == DateTimePrecision.Year)
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

        if (Equals(dateTime, Min, DateTimePrecision.Year))
        {
            from = Min.Month;
        }

        if (Equals(dateTime, Max, DateTimePrecision.Year))
        {
            to = Max.Month;
        }

        var count = to - from + 1;
        var months = Enumerable.Range(from, count).ToArray();

        foreach (var month in months)
        {
            var monthColumn = new DateTimeColumn(DateTimePrecision.Month, month, Formatter);
            if (Precision == DateTimePrecision.Month)
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

        if (Equals(dateTime, Min, DateTimePrecision.Month))
        {
            from = Min.Day;
        }

        if (Equals(dateTime, Max, DateTimePrecision.Month))
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
            var dayColumn = new DateTimeColumn(DateTimePrecision.Day, day, Formatter);
            if (Precision == DateTimePrecision.Day)
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

        if (Equals(dateTime, Min, DateTimePrecision.Day))
        {
            from = Min.Hour;
        }

        if (Equals(dateTime, Max, DateTimePrecision.Day))
        {
            to = Max.Hour;
        }

        var count = to - from + 1;
        var hourList = new List<DateTimeColumn>();

        var hours = Enumerable.Range(from, count).ToArray();
        foreach (var hour in hours)
        {
            var hourColumn = new DateTimeColumn(DateTimePrecision.Hour, hour, Formatter);
            if (Precision == DateTimePrecision.Hour)
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

        if (Equals(dateTime, Min, DateTimePrecision.Hour))
        {
            from = Min.Minute;
        }

        if (Equals(dateTime, Max, DateTimePrecision.Hour))
        {
            to = Max.Minute;
        }

        var count = to - from + 1;
        var minuteList = new List<DateTimeColumn>();

        var minutes = Enumerable.Range(from, count).ToArray();
        foreach (var minute in minutes)
        {
            var minuteColumn = new DateTimeColumn(DateTimePrecision.Minute, minute, Formatter);
            if (Precision == DateTimePrecision.Minute)
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

        if (Equals(dateTime, Min, DateTimePrecision.Minute))
        {
            from = Min.Second;
        }

        if (Equals(dateTime, Max, DateTimePrecision.Minute))
        {
            to = Max.Second;
        }

        var count = to - from + 1;
        var seconds = Enumerable.Range(from, count).ToArray();
        return seconds.Select(second => new DateTimeColumn(DateTimePrecision.Second, second, Formatter)).ToList();
    }

    private static bool Equals(DateTime d1, DateTime d2, DateTimePrecision precision)
    {
        var sameYear = d1.Year == d2.Year;
        if (precision == DateTimePrecision.Year)
        {
            return sameYear;
        }

        var sameMonth = sameYear && d1.Month == d2.Month;
        if (precision == DateTimePrecision.Month)
        {
            return sameMonth;
        }

        var sameDay = sameMonth && d1.Day == d2.Day;
        if (precision == DateTimePrecision.Day)
        {
            return sameDay;
        }

        var sameHour = sameDay && d1.Hour == d2.Hour;
        if (precision == DateTimePrecision.Hour)
        {
            return sameHour;
        }

        var sameMinute = sameHour && d1.Minute == d2.Minute;
        if (precision == DateTimePrecision.Minute)
        {
            return sameMinute;
        }

        var sameSecond = sameMinute && d1.Second == d2.Second;
        if (precision == DateTimePrecision.Second)
        {
            return sameSecond;
        }

        return false;
    }
}
