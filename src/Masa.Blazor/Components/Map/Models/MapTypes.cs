namespace Masa.Blazor
{
    public enum BaiduMapType
    {
        NormalMap = 0,
        EarthMap = 1,
        SatelliteMap = 2,
    };

    public static class MapTypeNameMapping
    {
        public static Dictionary<BaiduMapType, string> BaiduMapTypeName { get; } = new()
        {
            { BaiduMapType.NormalMap, "B_NORMAL_MAP" },
            { BaiduMapType.EarthMap, "B_EARTH_MAP" },
            { BaiduMapType.SatelliteMap, "B_SATELLITE_MAP" },
        };

    }
}
