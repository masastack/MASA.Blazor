namespace Masa.Blazor;

public class SwiperJsModule : JSModule
{
    public SwiperJsModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/swiper-proxy.js")
    {
    }

    public async ValueTask<ISwiperJSObjectReferenceProxy> Init(
        ElementReference el,
        SwiperOptions options
    )
    {
        var obj = await InvokeAsync<IJSObjectReference>("init", el, options);
        return new SwiperJSObjectReferenceProxy(obj);
    }
}

public interface ISwiperJSObjectReferenceProxy
{
}

public class SwiperJSObjectReferenceProxy : ISwiperJSObjectReferenceProxy
{
    private readonly IJSObjectReference _jsObjectReference;

    public SwiperJSObjectReferenceProxy(IJSObjectReference jsObjectReference)
    {
        _jsObjectReference = jsObjectReference;
    }
}
