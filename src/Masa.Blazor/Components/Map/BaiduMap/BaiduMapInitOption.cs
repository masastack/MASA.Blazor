namespace Masa.Blazor
{
    public class BaiduMapInitOptions
    {
        public float Zoom { get; set; }

        public GeoPoint Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

        public bool Dark { get; set; }

        public string? DarkThemeId { get; set; }

        public float MaxZoom { get; set; }

        public float MinZoom { get; set; }

        public string? MapTypeString { get; set; }

        public bool TrafficOn { get; set; }

    }
}