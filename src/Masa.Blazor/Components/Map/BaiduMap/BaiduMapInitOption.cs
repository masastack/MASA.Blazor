using System.Drawing;

namespace Masa.Blazor
{
    public class BaiduMapInitOption
    {
        public byte Zoom { get; set; } 

        public PointF Center { get; set; } 

        public bool CanZoom { get; set; } 
    }
}
