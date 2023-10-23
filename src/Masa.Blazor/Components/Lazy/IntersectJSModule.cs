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

    public async ValueTask ObserverAsync(
        ElementReference el,
        IntersectInvoker handle,
        IntersectionObserverInit? options = null)
    {
        await InvokeVoidAsync("observe", el, DotNetObjectReference.Create(handle), options);
    }

    public async ValueTask ObserverAsync(
        ElementReference el,
        Func<IntersectEventArgs, Task> handle,
        IntersectionObserverInit? options = null)
    {
        await InvokeVoidAsync("observe", el, DotNetObjectReference.Create(new IntersectInvoker(handle)), options);
    }

    public async ValueTask ObserverAsync(
        string selector,
        Func<IntersectEventArgs, Task> handle,
        IntersectionObserverInit? options = null)
    {
        await InvokeVoidAsync("observeSelector", selector,  DotNetObjectReference.Create(new IntersectInvoker(handle)), options);
    }

    public async ValueTask UnobserveAsync(ElementReference el)
    {
        await InvokeVoidAsync("unobserve", el);
    }

    public async ValueTask UnobserveAsync(string selector)
    {
        await InvokeVoidAsync("unobserveSelector", selector);
    }
}
