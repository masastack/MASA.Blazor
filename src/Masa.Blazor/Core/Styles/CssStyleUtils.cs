namespace Masa.Blazor.Core;

public static class CssStyleUtils
{
    private static Regex _colorRegex = new(@"rgb\((\d+),\s*(\d+),\s*(\d+)\)", RegexOptions.Compiled);

    public static string? GetTextColor(string? color, bool condition = true)
    {
        if (!condition || string.IsNullOrWhiteSpace(color) || !IsCssColor(color))
        {
            return null;
        }

        return $"color: {color}; caret-color: {color};";
    }

    public static string? GetBackgroundColor(string? color, bool condition = true)
    {
        if (!condition || string.IsNullOrWhiteSpace(color) || !IsCssColor(color))
        {
            return null;
        }

        return $"background-color: {color}; border-color: {color};";
    }

    public static string? GetHeight(StringNumber? height)
    {
        return height != null ? $"height: {height.ToUnit()};" : null;
    }

    public static string? GetWidth(StringNumber? width)
    {
        return width != null ? $"width: {width.ToUnit()};" : null;
    }

    private static bool IsCssColor(string color)
    {
        return _colorRegex.Match(color).Success;
    }
}