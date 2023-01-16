namespace Masa.Blazor;

public class EChartsJSModule : JSModule
{
    public EChartsJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/echarts-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Init(ElementReference el, string theme, EChartsInitOptions options)
        => await InvokeAsync<IJSObjectReference>("init", el, theme, options);

    public async ValueTask SetOption(IJSObjectReference instance, object option, bool notMerge = false, bool lazyUpdate = false)
        => await InvokeVoidAsync("setOption", instance, option, notMerge, lazyUpdate);

    public async ValueTask Dispose(IJSObjectReference instance)
        => await InvokeVoidAsync("dispose", instance);

    public async ValueTask Resize(IJSObjectReference instance, double width, double height)
        => await InvokeVoidAsync("resize", instance, width, height);
}
