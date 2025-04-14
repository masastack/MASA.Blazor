using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobileTimePicker : MobilePickerBase<TimeColumn, TimeColumn, int, TimeOnly>
{
    [Parameter]
    public TimeOnly Max { get; set; }

    [Parameter]
    public TimeOnly Min { get; set; }

    [Parameter]
    public Func<TimePrecision, int, string>? Formatter { get; set; }

    [Parameter]
    public TimePrecision Precision { get; set; } = TimePrecision.Second;

    private TimeOnly _prevMax;
    private TimeOnly _prevMin;

    private TimeOnly Now { get; }

    public PMobileTimePicker()
    {
        Now = TimeOnly.FromDateTime(DateTime.Now);
    }

    protected override string ClassPrefix => "p-mobile-time-picker";

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

        if (Min == default) Min = TimeOnly.MinValue;
        if (Max == default) Max = TimeOnly.MaxValue;
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
            Value = GetTimeWithinBoundary(Value);
            Columns = GetHours(Value);
        }
    }

    protected override bool TryConvertInternalValueToValue(List<int> internalValue, out TimeOnly value)
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

        var hour   = datetime[0];
        var minute = datetime[1];
        var second = datetime[2];

        hour   =   hour < 0 ? 0 : hour;
        minute = minute < 0 ? 0 : minute;
        second = second < 0 ? 0 : second;

        value = new TimeOnly(
            hour,
            Precision >= TimePrecision.Minute ? minute : 0,
            Precision >= TimePrecision.Second ? second : 0
        );

        return true;
    }

    protected override bool TryConvertValueToInternalValue(TimeOnly value, out List<int> internalValue)
    {
        internalValue = new();

        var hour   = value.Hour;
        var minute = value.Minute;
        var second = value.Second;

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

        TryConvertInternalValueToValue(val, out var time);
        time = GetTimeWithinBoundary(time);
        TryConvertValueToInternalValue(time, out val);

        var column = Columns.FirstOrDefault(c => c.Value == first);

        index++;
        while (val.Count > index && column?.Children != null && column.Children.Any())
        {
            first = val[index];
            column = column.Children.FirstOrDefault(c => c.Value == first);
            index++;
        }

        time = GetTimeWithinBoundary(time);

        if (column is { Children.Count: 0 })
        {
            if ((int)Precision >= index)
            {
                column.Children = GetColumns((TimePrecision)index, time);
            }
        }

        base.HandleValueChanged(val);
    }

    private List<TimeColumn> GetColumns(TimePrecision precision, TimeOnly time)
    {
        return precision switch
        {
            TimePrecision.Hour => GetHours(time),
            TimePrecision.Minute => GetMinutes(time),
            TimePrecision.Second => GetSeconds(time),
            _ => throw new ArgumentOutOfRangeException(nameof(precision), precision, null)
        };
    }

    private TimeOnly GetTimeWithinBoundary(TimeOnly input)
    {
        var time = input;

        if (time < Min)
        {
            time = Min;
        }
        else if (time > Max)
        {
            time = Max;
        }

        return time;
    }

    private List<TimeColumn> GetHours(TimeOnly time)
    {
        var from = 0;
        var to = 23;

        if (Min != default)
        {
            from = Min.Hour;
        }

        if (Max != default)
        {
            to = Max.Hour;
        }

        var count = to - from + 1;
        var hourList = new List<TimeColumn>();

        var hours = Enumerable.Range(from, count).ToArray();
        foreach (var hour in hours)
        {
            var hourColumn = new TimeColumn(TimePrecision.Hour, hour, Formatter);
            if (Precision == TimePrecision.Hour)
            {
                hourList.Add(hourColumn);
                continue;
            }

            if (hour == time.Hour)
            {
                hourColumn.Children = GetMinutes(time);
            }

            hourList.Add(hourColumn);
        }

        return hourList;
    }

    private List<TimeColumn> GetMinutes(TimeOnly dateTime)
    {
        var from = 0;
        var to = 59;

        if (Equals(dateTime, Min, TimePrecision.Hour))
        {
            from = Min.Minute;
        }

        if (Equals(dateTime, Max, TimePrecision.Hour))
        {
            to = Max.Minute;
        }

        var count = to - from + 1;
        var minuteList = new List<TimeColumn>();

        var minutes = Enumerable.Range(from, count).ToArray();
        foreach (var minute in minutes)
        {
            var minuteColumn = new TimeColumn(TimePrecision.Minute, minute, Formatter);
            if (Precision == TimePrecision.Minute)
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

    private List<TimeColumn> GetSeconds(TimeOnly dateTime)
    {
        var from = 0;
        var to = 59;

        if (Equals(dateTime, Min, TimePrecision.Minute))
        {
            from = Min.Second;
        }

        if (Equals(dateTime, Max, TimePrecision.Minute))
        {
            to = Max.Second;
        }

        var count = to - from + 1;
        var seconds = Enumerable.Range(from, count).ToArray();
        return seconds.Select(second => new TimeColumn(TimePrecision.Second, second, Formatter)).ToList();
    }

    private static bool Equals(TimeOnly d1, TimeOnly d2, TimePrecision precision)
    {
        var sameHour = d1.Hour == d2.Hour;
        if (precision == TimePrecision.Hour)
        {
            return sameHour;
        }

        var sameMinute = sameHour && d1.Minute == d2.Minute;
        if (precision == TimePrecision.Minute)
        {
            return sameMinute;
        }

        var sameSecond = sameMinute && d1.Second == d2.Second;
        if (precision == TimePrecision.Second)
        {
            return sameSecond;
        }

        return false;
    }
}
