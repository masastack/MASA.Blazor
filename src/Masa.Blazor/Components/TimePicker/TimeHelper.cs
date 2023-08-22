namespace Masa.Blazor;

public static class TimeHelper
{
    public static int Convert24To12(int hour)
    {
        return hour > 0 ? ((hour - 1) % 12 + 1) : 12;
    }

    public static int Convert12To24(int hour, TimePeriod period)
    {
        return hour % 12 + (period == TimePeriod.Pm ? 12 : 0);
    }

    public static Func<TimeOnly, bool>? IsAllowedTime(
        OneOf<Func<TimeOnly, bool>, List<TimeOnly>> allowedTimes,
        TimeOnly? max,
        TimeOnly? min)
    {
        var cb = allowedTimes.IsT1 ? val => allowedTimes.AsT1.Contains(val) : allowedTimes.AsT0;

        if (min == null && max == null)
        {
            return cb;
        }
        
        return val => val >= min && val <= max && (cb == null || cb(val));
    }

    public static Func<int, bool>? IsAllowedHour24(
        OneOf<Func<int, bool>, List<int>> allowedHours,
        TimeOnly? max,
        TimeOnly? min)
    {
        var cb = allowedHours.IsT1 ? val => allowedHours.AsT1.Contains(val) : allowedHours.AsT0;

        if (min == null && max == null)
        {
            return cb;
        }

        var minHour = min?.Hour ?? 0;
        var maxHour = max?.Hour ?? 23;

        return val => val >= minHour && val <= maxHour && (cb == null || cb(val));
    }

    public static Func<int, bool>? IsAllowedHourAmPm(
        Func<int, bool>? isAllowedHour,
        TimeFormat format,
        TimePeriod period)
    {
        if (isAllowedHour != null && format == TimeFormat.AmPm && period == TimePeriod.Pm)
        {
            return v => isAllowedHour(v + 12);
        }

        return isAllowedHour;
    }

    public static Func<int, bool>? IsAllowedMinute(
        Func<int, bool>? isAllowedHour,
        OneOf<Func<int, bool>, List<int>> allowedMinutes,
        TimeOnly? max,
        TimeOnly? min,
        int? hour)
    {
        var isHourAllowed = isAllowedHour == null || hour == null || isAllowedHour(hour.Value);

        var cb = allowedMinutes.IsT1 ? val => allowedMinutes.AsT1.Contains(val) : allowedMinutes.AsT0;

        if (min == null && max == null)
        {
            return isHourAllowed ? cb : val => false;
        }

        var (minHour, minMinute) = min != null ? (min.Value.Hour, min.Value.Minute) : (0, 0);
        var (maxHour, maxMinute) = max != null ? (max.Value.Hour, max.Value.Minute) : (23, 59);
        var minTime = minHour * 60 + minMinute;
        var maxTime = maxHour * 60 + maxMinute;

        return val =>
        {
            var time = 60 * hour + val;
            return time >= minTime && time <= maxTime && isHourAllowed && (cb == null || cb(val));
        };
    }

    public static Func<int, bool>? IsAllowedSecond(
        Func<int, bool>? isAllowedHour,
        Func<int, bool>? isAllowedMinute,
        OneOf<Func<int, bool>, List<int>> allowedSeconds,
        TimeOnly? max,
        TimeOnly? min,
        int? hour,
        int? minute)
    {
        var isHourAllowed = isAllowedHour == null || hour == null || isAllowedHour(hour.Value);
        var isMinuteAllowed = isHourAllowed && (isAllowedMinute == null || minute == null || isAllowedMinute(minute.Value));

        var cb = allowedSeconds.IsT1 ? val => allowedSeconds.AsT1.Contains(val) : allowedSeconds.AsT0;

        if (min == null && max == null)
        {
            return isMinuteAllowed ? cb : _ => false;
        }

        var (minHour, minMinute, minSecond) = min != null ? (min.Value.Hour, min.Value.Minute, min.Value.Second) : (0, 0, 0);
        var (maxHour, maxMinute, maxSecond) = max != null ? (max.Value.Hour, max.Value.Minute, max.Value.Second) : (23, 59, 59);
        var minTime = minHour * 3600 + minMinute * 60 + minSecond;
        var maxTime = maxHour * 3600 + maxMinute * 60 + maxSecond;

        return val =>
        {
            var time = 3600 * hour + 60 * minute + val;
            return time >= minTime && time <= maxTime && isMinuteAllowed && (cb == null || cb(val));
        };
    }

    /// <summary>
    /// Format hour to 12 hour format
    /// </summary>
    /// <param name="hour">0~11</param>
    /// <param name="format">AmPm or 24 hour</param>
    /// <returns></returns>
    public static string FormatAmHour(int hour, TimeFormat format)
    {
        if (format == TimeFormat.AmPm && hour == 0)
        {
            return "12";
        }

        return hour.ToString("00");
    }
}
