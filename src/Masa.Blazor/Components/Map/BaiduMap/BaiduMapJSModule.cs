namespace Masa.Blazor
{
    public class BaiduMapJSModule : JSModule
    {
        public BaiduMapJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/baidumap-proxy.js")
        {
        }

        public async ValueTask<IBaiduMapJSObjectReferenceProxy?> InitAsync(
            string containerId,
            BaiduMapInitOptions options,
            IBaiduMapJsCallbacks owner)
        {
            try
            {
                var jsObjectReference = await InvokeAsync<IJSObjectReference>("init", containerId, options);
                return new BaiduMapJSObjectReferenceProxy(jsObjectReference, owner);
            }
            catch (JSException e)
            {
                // TODO: after https://github.com/dotnet/aspnetcore/issues/52070 is fixed, remove this
                if (e.Message.StartsWith("Cannot create a JSObjectReference from the"))
                {
                    return null;
                }

                throw;
            }
        }
    }
}
