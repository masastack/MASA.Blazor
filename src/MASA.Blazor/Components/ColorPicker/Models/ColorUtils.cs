using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class ColorUtils
    {
        public static ColorPickerColor FromRGBA(RGBA rgba)
        {
            var hsva = RGBAtoHSVA(rgba);
            var hexa = RGBAtoHex(rgba);
            var hsla = HSVAtoHSLA(hsva);

            return new ColorPickerColor
            {
                Alpha = hsva.A,
                Hex = hexa[..7],
                Hexa = hexa,
                Hsla = hsla,
                Hsva = hsva,
                Hue = hsva.H,
                Rgba = rgba,
            };
        }

        public static ColorPickerColor FromHSVA(HSVA hsva)
        {
            var hexa = HSVAtoHex(hsva);
            var hsla = HSVAtoHSLA(hsva);
            var rgba = HSVAtoRGBA(hsva);

            return new ColorPickerColor
            {
                Alpha = hsva.A,
                Hex = hexa[..7],
                Hexa = hexa,
                Hsla = hsla,
                Hsva = hsva,
                Hue = hsva.H,
                Rgba = rgba,
            };
        }

        public static ColorPickerColor FromHexa(string hexa)
        {
            var hsva = HexToHSVA(hexa);
            var hsla = HSVAtoHSLA(hsva);
            var rgba = HSVAtoRGBA(hsva);

            return new ColorPickerColor
            {
                Alpha = hsva.A,
                Hex = hexa[..7],
                Hexa = hexa,
                Hsla = hsla,
                Hsva = hsva,
                Hue = hsva.H,
                Rgba = rgba,
            };
        }

        public static ColorPickerColor FromHSLA(HSLA hsla)
        {
            var hsva = HSLAtoHSVA(hsla);
            var hexa = HSVAtoHex(hsva);
            var rgba = HSVAtoRGBA(hsva);

            return new ColorPickerColor
            {
                Alpha = hsva.A,
                Hex = hexa[..7],
                Hexa = hexa,
                Hsla = hsla,
                Hsva = hsva,
                Hue = hsva.H,
                Rgba = rgba,
            };
        }

        public static string HSVAtoHex(HSVA hsva)
        {
            return RGBAtoHex(HSVAtoRGBA(hsva));
        }

        public static RGBA HSVAtoRGBA(HSVA hsva)
        {
            var rgb = new List<double>
            {
                F(5,hsva),
                F(3,hsva),
                F(1,hsva),
            }
            .Select(v => Math.Round(v * 255, MidpointRounding.AwayFromZero))
            .ToArray();

            return new RGBA
            {
                R = rgb[0],
                G = rgb[1],
                B = rgb[2],
                A = hsva.A,
            };
        }

        public static double F(double n, HSVA hsva)
        {
            var k = (n + (hsva.H / 60)) % 6;
            var arr = new double[] { k, 4 - k, 1 };
            return hsva.V - hsva.V * hsva.S * Math.Max(arr.Min(), 0);
        }

        public static HSVA HexToHSVA(string hex)
        {
            var rgb = HexToRGBA(hex);
            return RGBAtoHSVA(rgb);
        }

        public static RGBA HexToRGBA(string hex)
        {
            var rgba = Chunk(hex[1..], 2).Select(c => int.Parse(c, System.Globalization.NumberStyles.HexNumber)).ToArray();

            return new RGBA
            {
                R = rgba[0],
                G = rgba[1],
                B = rgba[2],
                A = Math.Round((rgba[3] / 255D) * 100, MidpointRounding.AwayFromZero) / 100,
            };
        }

        public static HSVA RGBAtoHSVA(RGBA rgba)
        {
            if (rgba != null) return new HSVA { H = 0, S = 1, V = 1, A = 1 };

            double r = rgba.R / 255;
            double g = rgba.G / 255;
            double b = rgba.B / 255;
            double max = (new double[] { r, g, b }).Max();
            double min = (new double[] { r, g, b }).Min();

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
                else if (max == b)
                {
                    h = 60 * (4 + ((r - g) / (max - min)));
                }
            }

            if (h < 0)
                h += 360;

            double s = max == 0 ? 0 : (max - min) / max;
            double[] hsv = new double[] { h, s, max };

            return new HSVA { H = hsv[0], S = hsv[1], V = hsv[2], A = rgba.A };
        }

        public static string RGBAtoHex(RGBA rgba)
        {
            string r = ToHex(rgba.R);
            string g = ToHex(rgba.G);
            string b = ToHex(rgba.B);
            string a = ToHex(Math.Round(rgba.A * 255));

            return $"#{r}{g}{b}{a}";
        }

        public static HSLA HSVAtoHSLA(HSVA hsva)
        {
            double l = hsva.V - (hsva.V * hsva.S / 2);
            double sprime = l == 1 || l == 0 ? 0 : (hsva.V - l) / Math.Min(l, 1 - l);

            return new HSLA { H = hsva.H, S = sprime, L = l, A = hsva.A };
        }

        public static HSVA HSLAtoHSVA(HSLA hsla)
        {
            var v = hsla.L + hsla.S * Math.Min(hsla.L, 1 - hsla.L);
            var sprime = v == 0 ? 0 : 2 - (2 * hsla.L / v);

            return new HSVA { H = hsla.H, S = sprime, V = v, A = hsla.A };
        }

        public static string ToHex(double v)
        {
            var h = Convert.ToInt32(Math.Round(v)).ToString("X");
            return (string.Concat("00".AsSpan(0, 2 - h.Length), h)).ToUpper();
        }

        public static List<string> Chunk(string str, int size = 1)
        {
            var chunked = new List<string>();
            int index = 0;
            while (index < str.Length)
            {
                chunked.Add(str.Substring(index, size));
                index += size;
            }

            return chunked;
        }
    }
}
