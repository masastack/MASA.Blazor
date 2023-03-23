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
    public static Func<DateOnly, string> AbbreviatedDayOfWeek(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek);
    }
    
    /// <summary>
    /// Get shortest day of week name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> ShortestDayOfWeek(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetShortestDayName(date.DayOfWeek);
    }

    /// <summary>
    /// Get abbreviated month and day name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> MonthDay(CultureInfo locale)
    {
        var abbreviatedMonthDay = locale.DateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM");
        return date => date.ToDateTime(TimeOnly.MinValue).ToString(abbreviatedMonthDay, locale.DateTimeFormat);
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
    /// Get month name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> Month(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetMonthName(date.Month);
    }

    /// <summary>
    /// Get abbreviated month name
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public static Func<DateOnly, string> AbbreviatedMonth(CultureInfo locale)
    {
        return date => locale.DateTimeFormat.GetAbbreviatedMonthName(date.Month);
    }
}