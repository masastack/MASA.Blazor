using System.Drawing;
using System.Text.Json;

namespace Masa.Blazor
{
    public static class DataConverterExtension
    {
        public static object ToGeoPoint(this PointF pointF)
            => new { Lng = pointF.X, Lat = pointF.Y };

        public static T ToObject<T>(this string json) 
            => JsonSerializer.Deserialize<T>(json);

    }
}
