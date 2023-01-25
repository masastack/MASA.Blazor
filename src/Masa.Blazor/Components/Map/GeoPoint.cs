using System.Drawing;

namespace Masa.Blazor
{
    public struct GeoPoint
    {
        public float Lng { get; set; }

        public float Lat { get; set; }

        public PointF ToPoint()
        {
            return new PointF(Lng, Lat);
        }
    }
}
