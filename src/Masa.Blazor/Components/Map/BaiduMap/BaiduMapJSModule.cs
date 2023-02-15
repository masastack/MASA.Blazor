namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IBaiduMapJSObjectReferenceProxy> InitAsync(
            string containerId,
            BaiduMapInitOptions options,
            IBaiduMapJsCallbacks owner)
            => new BaiduMapJSObjectReferenceProxy(await InvokeAsync<IJSObjectReference>("init", containerId, options), owner);

        public async ValueTask<IJSObjectReference> InitOverlayAsync(MBaiduOverlay overlay)
        {
            return overlay switch
            {
                MBaiduCircle circle => await InvokeAsync<IJSObjectReference>("initCircle", circle),
                MBaiduMarker marker => await InvokeAsync<IJSObjectReference>("initMarker", marker),
                MBaiduLabel label => await InvokeAsync<IJSObjectReference>("initLabel", label),
                MBaiduPolyline polyline => await InvokeAsync<IJSObjectReference>("initPolyline", polyline),
                MBaiduPolygon polygon => await InvokeAsync<IJSObjectReference>("initPolygon", polygon),
                _ => null
            };
        }

    }
}
