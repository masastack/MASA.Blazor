namespace Masa.Blazor;

public static class DateFormatters
{
    /// <summary>
    /// Get year name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> Year(CultureInfo locale)
    {
        return date => date.ToString("yyyy", locale.DateTimeFormat);
    }

    /// <summary>
    /// Get day of month name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> Day(CultureInfo locale)
    {
        return date => date.ToString("dd", locale.DateTimeFormat);
    }

    /// <summary>
    /// Get abbreviated day of week name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> DayOfWeek(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek);
    }

    /// <summary>
    /// Get month and day name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> MonthDay(CultureInfo locale)
    {
        return date => date.ToString(locale.DateTimeFormat.MonthDayPattern, locale.DateTimeFormat);
    }

    /// <summary>
    /// Get year and month name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> YearMonth(CultureInfo locale)
    {
        return date => date.ToString(locale.DateTimeFormat.YearMonthPattern, locale.DateTimeFormat);
    }

    /// <summary>
    /// Get abbreviated month name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> Month(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetAbbreviatedMonthName(date.Month);
    }
}