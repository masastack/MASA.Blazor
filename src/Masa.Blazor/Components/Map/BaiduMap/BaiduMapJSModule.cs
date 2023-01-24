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

        public async ValueTask<IJSObjectReference> LoadMapAsync(string containerID, BaiduMapInitOption args)
            => await InvokeAsync<IJSObjectReference>
                (
                    "loadMap",
                    containerID,
                    new
                    {
                        args.CanZoom,
                        args.Zoom,
                        MapCenter = args.MapCenter.ToGeoPoint()
                    }
                );

    }
}
