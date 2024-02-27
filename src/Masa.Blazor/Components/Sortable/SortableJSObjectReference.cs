using Masa.Blazor.JSModules;

namespace Masa.Blazor;

public class SortableJSObjectReference : JSObjectReferenceBase
{
    public SortableJSObjectReference(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public ValueTask<List<string>> ToArrayAsync()
        => JSObjectReference.InvokeAsync<List<string>>("invoke", "toArray");

    public ValueTask SortAsync(List<string> order, bool useAnimation)
        => InvokeVoidAsync("sort", order, useAnimation);
        // => JSObjectReference.InvokeVoidAsync("invokeVoid", "sort", order, useAnimation);
}