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

    }
}
