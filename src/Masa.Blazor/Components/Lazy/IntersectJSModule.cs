namespace Masa.Blazor;

public class IntersectJSModule : JSModule
{
    public IntersectJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/intersect.js")
    {
    }

    public async ValueTask ObserverAsync(
        ElementReference el,
        DotNetObjectReference<IntersectInvoker> handle,
        IntersectionObserverInit? options = null)
    {
        await InvokeVoidAsync("observe", el, handle, options);
    }

    public async ValueTask UnobserveAsync(ElementReference el)
    {
        await InvokeVoidAsync("unobserve", el);
    }
}
