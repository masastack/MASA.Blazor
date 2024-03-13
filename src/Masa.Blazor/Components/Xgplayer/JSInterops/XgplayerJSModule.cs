namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSModule : JSModule
{
    public XgplayerJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/xgplayer-proxy.js")
    {
    }

    public async ValueTask<XgplayerJSObjectReference> InitAsync(
        string selector,
        IEnumerable<MediaStreamUrl> urls,
        XgplayerOptions? options,
        DotNetObjectReference<XgplayerJSInteropHandle> interopHandle)
    {
        var jsObjectReference = await InvokeAsync<IJSObjectReference>("init", selector, urls, options, interopHandle);
        return new XgplayerJSObjectReference(jsObjectReference);
    }
}
