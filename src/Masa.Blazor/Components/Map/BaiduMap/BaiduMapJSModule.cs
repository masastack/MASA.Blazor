namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IJSObjectReference> InitAsync(string containerId, BaiduMapInitOptions options, DotNetObjectReference<MBaiduMap> obj)
            => await InvokeAsync<IJSObjectReference>("init", containerId, options, obj);

        public async ValueTask<IJSObjectReference> InitAndAddOverlayAsync(MBaiduOverlay overlay, IJSObjectReference map)
        {
            return overlay switch
            {
                MBaiduCircle circle => await InvokeAsync<IJSObjectReference>("initAndAddCircle", circle, map),
                MBaiduMarker marker => await InvokeAsync<IJSObjectReference>("initAndAddMarker", marker, map),
                MBaiduLabel label => await InvokeAsync<IJSObjectReference>("initAndAddLabel", label, map),
                MBaiduPolyline polyline => await InvokeAsync<IJSObjectReference>("initAndAddPolyline", polyline, map),
                MBaiduPolygon polygon => await InvokeAsync<IJSObjectReference>("initAndAddPolygon", polygon, map),
                _ => null
            };
        }

    }
}
