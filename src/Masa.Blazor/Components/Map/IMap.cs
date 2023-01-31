using System.Drawing;

namespace Masa.Blazor
{
    public interface IMap
    {
        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public float Zoom { get; set; }

        public GeoPoint Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

    }
}
