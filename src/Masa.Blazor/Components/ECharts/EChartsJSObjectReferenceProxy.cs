namespace Masa.Blazor;

public class IeChartsJSObjectReferenceProxy : JSObjectReferenceProxy, IEChartsJSObjectReferenceProxy
{
    public IeChartsJSObjectReferenceProxy(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public async ValueTask SetOptionAsync(object option, bool notMerge = false, bool lazyUpdate = false)
        => await InvokeVoidAsync("setOption", option, notMerge, lazyUpdate);

    public async ValueTask ResizeAsync()
        => await InvokeVoidAsync("resize");

    public async ValueTask ResizeAsync(double width, double height)
        => await InvokeVoidAsync("resize", width, height);

    public async ValueTask DisposeEChartsAsync()
        => await InvokeVoidAsync("dispose");
}
