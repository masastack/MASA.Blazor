using System.Drawing;

namespace Masa.Blazor
{
    public class BaiduMapInitOption
    {
        public byte Zoom { get; set; } 

        public PointF Center { get; set; } 

        public bool EnableScrollWheelZoom { get; set; }

        public bool Dark { get; set; }

        public string DarkThemeId { get; set; }

    }
}
