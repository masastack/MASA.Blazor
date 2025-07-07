namespace Masa.Blazor.Components.Sortable;

public class SortableJSModule : JSModule
{
    public SortableJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/wrappers/sortable.js")
    {
    }

    public async ValueTask<SortableJSObjectReference> InitAsync(
        ElementReference elementReference,
        SortableOptions? options,
        IEnumerable<string>? order,
        DotNetObjectReference<SortableJSInteropHandle> interopHandle)
    {
        var jsObjectReference =
            await InvokeAsync<IJSObjectReference>("init", elementReference, options, order, interopHandle);
        return new SortableJSObjectReference(jsObjectReference);
    }

    public async ValueTask<SortableJSObjectReference> InitAsync(
        string selector,
        SortableOptions? options,
        IEnumerable<string>? order,
        DotNetObjectReference<SortableJSInteropHandle> interopHandle)
    {
        var jsObjectReference =
            await InvokeAsync<IJSObjectReference>("init", selector, options, order, interopHandle);
        return new SortableJSObjectReference(jsObjectReference);
    }
}