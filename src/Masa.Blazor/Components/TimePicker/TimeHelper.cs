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
