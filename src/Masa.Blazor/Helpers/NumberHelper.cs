using System.Text.RegularExpressions;

namespace BlazorComponent;

public static class NumberHelper
{
    private static readonly Regex LeadingInteger = new(@"^(-?\d+)");
    private static readonly Regex LeadingDouble = new(@"^(-?\d+)(\.?\d+)");

    // TODO: test

    /// <summary>
    /// Same as parseInt in javascript. Return 0 if input is invalid.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ParseInt(string? str)
    {
        if (str == null) return 0;

        var match = LeadingInteger.Match(str);
        return !match.Success ? 0 : int.Parse(match.Value);
    }

    // TODO: test

    /// <summary>
    /// Same as parseFloat in javascript. Return 0 if input is invalid.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double ParseDouble(string? str)
    {
        if (str == null) return 0;

        var match = LeadingDouble.Match(str);
        return !match.Success ? 0 : double.Parse(match.Value);
    }

    public static bool TryParseDouble(string? str, out double value)
    {
        value = 0;

        if (str == null) return false;

        var match = LeadingDouble.Match(str);

        if (!match.Success) return false;

        value = double.Parse(match.Value);

        return true;
    }
}
