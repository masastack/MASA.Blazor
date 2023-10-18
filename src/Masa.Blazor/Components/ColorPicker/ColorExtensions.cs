using System.Drawing;
using static Masa.Blazor.ColorPicker.Util;

namespace Masa.Blazor;

public static class ColorExtensions
{
    public static Color FromHex(this Color color, string hex)
    {
        return HexToRGBA(hex);
    }

    public static(double H, double S, double V, double A) ToHSVA(this Color color)
    {
        return RGBAtoHSVA(color);
    }
}
