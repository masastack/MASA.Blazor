using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public struct GeoPoint
    {
        [JsonPropertyName("lng")]
        public float Lng { get; set; }

        [JsonPropertyName("lat")]
        public float Lat { get; set; }

        public PointF ToPointF() => new (Lng, Lat);

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
