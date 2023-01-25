using System.Drawing;

namespace Masa.Blazor
{
    public interface IMap
    {
        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public byte Zoom { get; set; }

        public PointF Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

    }
}
