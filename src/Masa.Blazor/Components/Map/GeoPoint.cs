using System.Drawing;
using System.Text.Json;

namespace Masa.Blazor
{
    public struct GeoPoint
    {
        public float Lng { get; set; }

        public float Lat { get; set; }

        public PointF ToPointF() => new (Lng, Lat);

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
