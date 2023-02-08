namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IJSObjectReference> InitAsync(string containerId, BaiduMapInitOptions options, DotNetObjectReference<MBaiduMap> obj)
            => await InvokeAsync<IJSObjectReference>("init", containerId, options, obj);

        public async ValueTask<IJSObjectReference> ConstructOverlayAsync<TOverlay>(TOverlay overlay) where TOverlay : IMapOverlay
        {
            if (overlay is MBaiduCircle circle)
                return await InvokeAsync<IJSObjectReference>("constructCircle", circle);
            
            return null;
        }

    }
}
