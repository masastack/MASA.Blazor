using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public struct GeoPoint
    {
        public GeoPoint()
        {

        }

        public GeoPoint(float lng, float lat)
        {
            Lng= lng;
            Lat= lat;
        }

        [JsonPropertyName("lng")]
        public float Lng { get; set; } = default;

        [JsonPropertyName("lat")]
        public float Lat { get; set; } = default;

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
