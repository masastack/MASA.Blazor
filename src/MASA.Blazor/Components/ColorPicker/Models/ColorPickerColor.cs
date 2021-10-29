using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class ColorPickerColor
    {
        public double Alpha { get; set; }

        public string Hex { get; set; }

        public string Hexa { get; set; }

        public HSLA Hsla { get; set; }

        public HSVA Hsva { get; set; }

        public double Hue { get; set; }

        public RGBA Rgba { get; set; }
    }
}
