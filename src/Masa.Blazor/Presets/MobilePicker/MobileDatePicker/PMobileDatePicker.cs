using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobileDatePicker : MobilePickerBase<DateColumn, DateColumn, int, DateOnly>
{
    [Parameter]
    public DateOnly Max { get; set; }

    [Parameter]
    public DateOnly Min { get; set; }

    [Parameter]
    public Func<DatePrecision, int, string>? Formatter { get; set; }

    [Parameter]
    public DatePrecision Precision { get; set; } = DatePrecision.Day;

    private DateOnly _prevMax;
    private DateOnly _prevMin;

    private DateOnly Now { get; }

    public PMobileDatePicker()
    {
        Now = DateOnly.FromDateTime(DateTime.Now);
    }

    protected override string ClassPrefix => "p-mobile-date-picker";

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

        if (needChange || Value == default)
        {
            Value = GetDateWithinBoundary(Value);
            Columns = GetYears(Value);
        }
    }

    protected override bool TryConvertInternalValueToValue(List<int> internalValue, out DateOnly value)
    {
        value = default;

        if (internalValue is null || internalValue.Count == 0)
        {
            return false;
        }

        var datetime = new int[3];
        for (var i = 0; i < internalValue.Count; i++)
        {
            datetime[i] = internalValue[i];
        }

        var year   = datetime[0];
        var month  = datetime[1];
        var day    = datetime[2];

        year   =   year < 1 ? 1 : year;
        month  =  month < 1 ? 1 : month;
        day    =    day < 1 ? 1 : day;

        var daysInMonth = DateTime.DaysInMonth(year, month);
        if (day > daysInMonth)
        {
            day = daysInMonth;
        }

        value = new DateOnly(
            year,
            Precision >= DatePrecision.Month ? month : 1,
            Precision >= DatePrecision.Day ? day : 1
        );

        return true;
    }

    protected override bool TryConvertValueToInternalValue(DateOnly value, out List<int> internalValue)
    {
        internalValue = new();

        if (value == default)
        {
            return false;
        }

        var year   = value.Year;
        var month  = value.Month;
        var day    = value.Day;

        internalValue.Add(year);
        internalValue.Add(month);
        internalValue.Add(day);

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
        dateTime = GetDateWithinBoundary(dateTime);
        TryConvertValueToInternalValue(dateTime, out val);

        var column = Columns.FirstOrDefault(c => c.Value == first);

        index++;
        while (val.Count > index && column?.Children != null && column.Children.Any())
        {
            first = val[index];
            column = column.Children.FirstOrDefault(c => c.Value == first);
            index++;
        }

        dateTime = GetDateWithinBoundary(dateTime);

        if (column is { Children.Count: 0 })
        {
            if ((int)Precision >= index)
            {
                column.Children = GetColumns((DatePrecision)index, dateTime);
            }
        }

        base.HandleValueChanged(val);
    }

    private List<DateColumn> GetColumns(DatePrecision precision, DateOnly dateTime)
    {
        return precision switch
        {
            DatePrecision.Year => GetYears(dateTime),
            DatePrecision.Month => GetMonths(dateTime),
            DatePrecision.Day => GetDays(dateTime),
            _ => throw new ArgumentOutOfRangeException(nameof(precision), precision, null)
        };
    }

    private DateOnly GetDateWithinBoundary(DateOnly input)
    {
        var date = input;

        if (input == default)
        {
            date = Now;
        }

        if (date < Min)
        {
            date = Min;
        }
        else if (date > Max)
        {
            date = Max;
        }

        return date;
    }

    private List<DateColumn> GetYears(DateOnly dateTime)
    {
        List<DateColumn> yearList = new();

        var years = Enumerable.Range(Min.Year, Max.Year - Min.Year + 1).ToArray();

        foreach (var year in years)
        {
            var yearColumn = new DateColumn(DatePrecision.Year, year, Formatter);
            if (Precision == DatePrecision.Year)
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

    private List<DateColumn> GetMonths(DateOnly dateTime)
    {
        List<DateColumn> monthList = new();

        var from = 1;
        var to = 12;

        if (Equals(dateTime, Min, DatePrecision.Year))
        {
            from = Min.Month;
        }

        if (Equals(dateTime, Max, DatePrecision.Year))
        {
            to = Max.Month;
        }

        var count = to - from + 1;
        var months = Enumerable.Range(from, count).ToArray();

        foreach (var month in months)
        {
            var monthColumn = new DateColumn(DatePrecision.Month, month, Formatter);
            if (Precision == DatePrecision.Month)
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

    private List<DateColumn> GetDays(DateOnly dateTime)
    {
        var from = 1;
        var to = 0;

        if (Equals(dateTime, Min, DatePrecision.Month))
        {
            from = Min.Day;
        }

        if (Equals(dateTime, Max, DatePrecision.Month))
        {
            to = Max.Day;
        }

        if (to == 0)
        {
            to = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        var count = to - from + 1;
        var days = Enumerable.Range(from, count).ToArray();
        return days.Select(day => new DateColumn(DatePrecision.Day, day, Formatter)).ToList();
    }

    private static bool Equals(DateOnly d1, DateOnly d2, DatePrecision precision)
    {
        var sameYear = d1.Year == d2.Year;
        if (precision == DatePrecision.Year)
        {
            return sameYear;
        }

        var sameMonth = sameYear && d1.Month == d2.Month;
        if (precision == DatePrecision.Month)
        {
            return sameMonth;
        }

        var sameDay = sameMonth && d1.Day == d2.Day;
        if (precision == DatePrecision.Day)
        {
            return sameDay;
        }

        return false;
    }
}
