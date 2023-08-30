namespace Masa.Blazor;

public class SwiperJsModule : JSModule
{
    public SwiperJsModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/swiper-proxy.js")
    {
    }

    public async ValueTask<ISwiperJSObjectReferenceProxy> Init(
        ElementReference el,
        SwiperOptions options,
        DotNetObjectReference<object> interopHandle
    )
    {
        var obj = await InvokeAsync<IJSObjectReference>("init", el, options, interopHandle);
        return new SwiperJSObjectReferenceProxy(obj);
    }
}