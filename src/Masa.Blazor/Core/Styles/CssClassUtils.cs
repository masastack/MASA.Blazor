namespace Masa.Blazor.Core;

public static class CssClassUtils
{
    public static string GetSize(bool xSmall, bool small, bool large, bool xLarge)
    {
        if (xSmall)
        {
            return "m-size--x-small";
        }

        if (small)
        {
            return "m-size--small";
        }

        if (large)
        {
            return "m-size--large";
        }

        if (xLarge)
        {
            return "m-size--x-large";
        }

        return "m-size--default";
    }

    public static string? GetColor(string? color, bool isText = false)
    {
        if (string.IsNullOrWhiteSpace(color) || color.StartsWith("#") || color.StartsWith("rgb"))
        {
            return null;
        }

        StringBuilder stringBuilder = new();

        if (isText)
        {
            var colors = color.Split(" ");
            var firstColor = colors[0];

            stringBuilder.Append($"{firstColor}--text ");

            if (colors.Length == 2)
            {
                // TODO: 是否需要正则表达式验证格式
                // {darken|lighten|accent}-{1|2}

                var secondColor = colors[1];
                stringBuilder.Append($"text--{secondColor} ");
            }
        }
        else
        {
            stringBuilder.Append(color);
        }

        return stringBuilder.Length > 0 ? stringBuilder.ToString().Trim() : null;
    }

    public static string? GetTextColor(string? color)
    {
        return GetColor(color, true);
    }

    public static string? GetBackgroundColor(string? color, bool condition = true)
    {
        if (!condition)
        {
            return null;
        }

        return GetColor(color, false);
    }

    public static string GetTheme(bool isDark, bool isIndependent = false)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append(isDark ? "theme--dark" : "theme--light");

        if (isIndependent)
        {
            stringBuilder.Append(" theme--independent");
        }

        return stringBuilder.ToString();
    }
}