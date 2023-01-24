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

        public async ValueTask<IJSObjectReference> LoadMapAsync(string containerId, BaiduMapInitOption options)
            => await InvokeAsync<IJSObjectReference>
                (
                    "loadMap",
                    containerId,
                    new
                    {
                        options.CanZoom,
                        options.Zoom,
                        MapCenter = options.MapCenter.ToGeoPoint()
                    }
                );

    }
}
