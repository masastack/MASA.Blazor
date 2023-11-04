using System.Drawing;
using static Masa.Blazor.ColorPicker.Util;

namespace Masa.Blazor;

public static class ColorExtensions
{
    public static double GetAlpha(this Color color)
    {
        return color.A / 255d;
    }

    public static Color FromHex(this Color color, string hex)
    {
        return HexToRGBA(hex);
    }

    public static Color FromHSVA(this Color color, double h, double s, double v, double a)
    {
        return HSVAToRGBA(h, s, v, a);
    }

    public static(double H, double S, double V, double A) ToHSVA(this Color color)
    {
        return RGBAtoHSVA(color);
    }
}
