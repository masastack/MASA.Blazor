using Masa.Blazor.JSModules;

namespace Masa.Blazor;

public class SortableJSObjectReference : JSObjectReferenceBase
{
    public SortableJSObjectReference(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public ValueTask<IEnumerable<string>> GetOrderAsync()
        => JSObjectReference.InvokeAsync<IEnumerable<string>>("getOrder");
}