using System.Drawing;

namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask InjectBaiduMapScriptAsync(string serviceKey)
        => await InvokeVoidAsync("injectBaiduMapScript", serviceKey);

        public async ValueTask LoadMapAsync(string divID, (bool canZoom, byte zoom, PointF mapCenter) args)
        => await InvokeVoidAsync
            (
                "loadMap",
                divID,
                new
                {
                    CanZoom = args.canZoom,
                    Zoom = args.zoom,
                    MapCenter = new { Lng = args.mapCenter.X, Lat = args.mapCenter.Y }
                }
            );

    }
}
