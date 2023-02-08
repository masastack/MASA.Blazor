namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IJSObjectReference> InitAsync(string containerId, BaiduMapInitOptions options, DotNetObjectReference<MBaiduMap> obj)
            => await InvokeAsync<IJSObjectReference>("init", containerId, options, obj);

        public async ValueTask<IJSObjectReference> ConstructOverlayAsync<TOverlay, TMap>(TOverlay overlay)
            where TOverlay : IMapOverlay<TMap>
            where TMap : IMap
        {
            if (overlay is MBaiduCircle circle)
                return await InvokeAsync<IJSObjectReference>("constructCircle", circle);

            if (overlay is MBaiduMarker marker)
                return await InvokeAsync<IJSObjectReference>("constructMarker", marker);

            if (overlay is MBaiduLabel label)
                return await InvokeAsync<IJSObjectReference>("constructLabel", label);

            if (overlay is MBaiduPolyline polyline)
                return await InvokeAsync<IJSObjectReference>("constructPolyline", polyline);

            if (overlay is MBaiduPolygon polygon)
                return await InvokeAsync<IJSObjectReference>("constructPolygon", polygon);

            return null;
        }

    }
}
