namespace Masa.Blazor.Core;

internal static class ColorParser
{
    private const string CssColorPattern = @"^(?<fn>(?:rgb|hsl)a?)\((?<values>.+)\)";

    private static readonly Regex CssColorRegex = new(CssColorPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static (byte R, byte G, byte B) ParseColor(string color)
    {
        if (CssColorRegex.IsMatch(color))
        {
            return ParseCssColor(color);
        }

        if (color.StartsWith("#"))
        {
            return ParseHexColor(color);
        }

        throw new ArgumentException($"Invalid color format: {color}, only rgb, hsl and hex are supported");
    }

    internal static string ParseColorAsString(string color)
    {
        var parsedColor = ParseColor(color);
        return $"{parsedColor.R}, {parsedColor.G}, {parsedColor.B}";
    }

    private static (byte R, byte G, byte B) ParseCssColor(string color)
    {
        var match = CssColorRegex.Match(color);
        var functionName = match.Groups["fn"].Value.ToLower();
        var values = ExtractAndParseValues(match, functionName);

        return functionName switch
        {
            "rgb" or "rgba" => ((byte)values[0], (byte)values[1], (byte)values[2]),
            "hsl" or "hsla" => HslToRgb(values[0], values[1], values[2]),
            _ => throw new ArgumentException($"Unsupported color function: {functionName}")
        };
    }

    private static float[] ExtractAndParseValues(Match match, string functionName)
    {
        var values = match.Groups["values"].Value
            .Split([','], StringSplitOptions.RemoveEmptyEntries)
            .Select(v => ParseValue(v, functionName))
            .ToArray();

        return values;
    }

    private static float ParseValue(string value, string functionName)
    {
        value = value.Trim();
        var isPercentage = value.EndsWith("%");

        var number = float.Parse(
            isPercentage ? value.TrimEnd('%') : value,
            CultureInfo.InvariantCulture
        );

        if (isPercentage && functionName.StartsWith("hsl"))
            return number / 100f;

        return isPercentage ? number / 100f : number;
    }

    private static (byte R, byte G, byte B) ParseHexColor(string color)
    {
        var hex = color[1..];

        hex = hex.Length switch
        {
            3 or 4 => string.Concat(hex.Select(c => $"{c}{c}")),
            6 or 8 => hex,
            _ => throw new ArgumentException($"Invalid hex color: {color}")
        };

        return (
            Convert.ToByte(hex[..2], 16),
            Convert.ToByte(hex[2..4], 16),
            Convert.ToByte(hex[4..6], 16)
        );
    }

    private static (byte R, byte G, byte B) HslToRgb(float fh, float fs, float fl)
    {
        var h = fh % 360;
        var s = Math.Clamp(fs, 0f, 1f);
        var l = Math.Clamp(fl, 0f, 1f);

        var c = (1 - Math.Abs(2 * l - 1)) * s;
        var x = c * (1 - Math.Abs((h / 60f % 2) - 1));
        var m = l - c / 2;

        var (r, g, b) = (h / 60f) switch
        {
            < 1 => (c, x, 0f),
            < 2 => (x, c, 0f),
            < 3 => (0f, c, x),
            < 4 => (0f, x, c),
            < 5 => (x, 0f, c),
            _ => (c, 0f, x)
        };

        return (
            (byte)((r + m) * 255),
            (byte)((g + m) * 255),
            (byte)((b + m) * 255)
        );
    }
}