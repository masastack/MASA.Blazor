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

        public async ValueTask<ElementReference?> LoadMapAsync(string divID, BaiduMapInitOption args)
            => await InvokeAsync<ElementReference?>
                (
                    "loadMap",
                    divID,
                    new
                    {
                        args.CanZoom,
                        args.Zoom,
                        MapCenter = new { Lng = args.MapCenter.X, Lat = args.MapCenter.Y }
                    }
                );

    }
}
