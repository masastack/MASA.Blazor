using System.Drawing;

namespace Masa.Blazor;

public class BaiduMapEventArgs
{
    public GeoPoint Latlng { get; set; }

    public Point Pixel { get; set; }
}