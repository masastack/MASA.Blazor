using System.Drawing;

namespace Masa.Blazor
{
    public static class DataConverterExtension
    {
        public static object ToGeoPoint(this PointF pointF)
            => new { Lng = pointF.X, Lat = pointF.Y };

    }
}
