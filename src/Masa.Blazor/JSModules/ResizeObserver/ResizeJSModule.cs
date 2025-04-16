namespace Masa.Blazor.JSModules;

public class ResizeJSModule(IJSRuntime js)
    : JSModule(js, $"./_content/Masa.Blazor/js/{JSManifest.ResizeIndexJs}"), IResizeJSModule
{
    public async ValueTask ObserverAsync(ElementReference el, Func<Task> handle)
    {
        await InvokeVoidAsync("observe", el, DotNetObjectReference.Create(new Invoker(handle)));
    }

    public async ValueTask UnobserveAsync(ElementReference el)
    {
        await InvokeVoidAsync("unobserve", el);
    }

    public async ValueTask ObserverAsync(string selector, Func<Task> handle)
    {
        await InvokeVoidAsync("observeSelector", selector, DotNetObjectReference.Create(new Invoker(handle)));
    }

    public async ValueTask UnobserveAsync(string selector)
    {
        await InvokeVoidAsync("unobserveSelector", selector);
    }
}