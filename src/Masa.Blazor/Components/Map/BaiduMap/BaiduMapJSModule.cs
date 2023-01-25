using System.Drawing;

namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IJSObjectReference> InitMapAsync(string containerId, BaiduMapInitOption options)
            => await InvokeAsync<IJSObjectReference>("initMap", containerId, options);

    }
}
