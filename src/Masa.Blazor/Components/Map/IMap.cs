using System.Drawing;

namespace Masa.Blazor
{
    public interface IMap
    {
        public string ServiceKey { get; set; }

        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public byte Zoom { get; set; }

        public PointF MapCenter { get; set; }

        public bool CanZoom { get; set; }

    }
}
