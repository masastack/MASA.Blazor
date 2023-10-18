using System.Drawing;

namespace Masa.Blazor.ColorPicker;

public static class Util
{
    public static Color HexToRGBA(string hex)
    {
        hex = hex[1..];

        var rgba = Enumerable.Range(0, hex.Length / 2)
                             .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                             .ToArray();

        Console.Out.WriteLine("HexToRGBA: " + string.Join(", ", rgba));

        return Color.FromArgb(
            alpha: rgba[3], // test?
            red: rgba[0],
            green: rgba[1],
            blue: rgba[2]);
    }

    public static(double h, double s, double v, double a) RGBAtoHSVA(Color rgba)
    {
        // return

        var r = rgba.R / 255d;
        var g = rgba.G / 255d;
        var b = rgba.B / 255d;
        var max = Math.Max(r, Math.Max(g, b));
        var min = Math.Min(r, Math.Min(g, b));

        double h = 0;

        if (max != min)
        {
            if (max == r)
            {
                h = 60 * (0 + ((g - b) / (max - min)));
            }
            else if (max == g)
            {
                h = 60 * (2 + ((b - r) / (max - min)));
            }
            else
            {
                h = 60 * (4 + ((r - g) / (max - min)));
            }
        }

        if (h < 0)
        {
            h = h + 360;
        }

        var s = (max == 0) ? 0 : (max - min) / max;
        var hsv = (h, s, v: max);
        return (h: hsv.h, s: hsv.s, v: hsv.v, a: rgba.A / 255d);
    }
}
