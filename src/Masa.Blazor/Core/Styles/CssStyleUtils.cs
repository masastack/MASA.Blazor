namespace Masa.Blazor.Core;

public static class CssStyleUtils
{
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
        return color.StartsWith("#") || color.StartsWith("rgb") || color.StartsWith("hsl");
    }
}