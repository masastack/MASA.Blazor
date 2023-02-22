using System.Drawing;
using System.Text.Json.Serialization;

namespace Masa.Blazor
{
    public class MBaiduMarker : MBaiduOverlay, IMarker
    {
        [Parameter]
        public GeoPoint Point { get; set; }

        [Parameter]
        public Size Offset { get; set; }

        [Parameter]
        public float Rotation { get; set; }

        [Parameter]
        public string Title { get; set; }
    }
}