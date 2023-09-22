namespace Masa.Blazor;

public class ResizeJSModule : JSModule, IResizeJSModule
{
    public ResizeJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/resize.js")
    {
    }

    public async ValueTask ObserverAsync(ElementReference el, Func<Task> handle)
    {
        await InvokeVoidAsync("observe", el, DotNetObjectReference.Create(new Invoker(handle)));
    }

    public async ValueTask UnobserveAsync(ElementReference el)
    {
        await InvokeVoidAsync("unobserve", el);
    }
}
