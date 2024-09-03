namespace Masa.Blazor;

public record BaiduMapIcon(string Url, BaiduMapSize Size, BaiduMapIconOptions? Options = null);

public record BaiduMapSize(int Width, int Height);

public record BaiduMapIconOptions(BaiduMapSize? Anchor = null, BaiduMapSize? ImageOffset = null);