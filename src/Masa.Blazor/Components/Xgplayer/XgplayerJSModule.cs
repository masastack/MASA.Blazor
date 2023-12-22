namespace Masa.Blazor.Components.Xgplayer;

public class XgplayerJSModule : JSModule
{
    public XgplayerJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/xgplayer-proxy.js")
    {
    }

    public async ValueTask<XgplayerJSObjectReference> InitAsync(string selector, string url, XgplayerOptions? options = null)
    {
        return await InitAsync(selector, new[] { new MediaStreamUrl(url) }, options);
    }

    public async ValueTask<XgplayerJSObjectReference> InitAsync(
        string selector,
        IEnumerable<MediaStreamUrl> urls,
        XgplayerOptions? options = null)
    {
        Console.Out.WriteLine("[XgplayerJSModule] InitAsync");

        var jsObjectReference = await InvokeAsync<IJSObjectReference>("init", selector, urls, options);
        return new XgplayerJSObjectReference(jsObjectReference);
    }
}
