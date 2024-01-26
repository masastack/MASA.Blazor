using Masa.Blazor.JSModules;

namespace Masa.Blazor.Components.ScrollToTarget;

public class ScrollToTargetJSObjectReference : JSObjectReferenceBase
{
    public ScrollToTargetJSObjectReference(IJSObjectReference jsObjectReference) : base(jsObjectReference)
    {
    }

    public ValueTask ObserveAsync(string id) => JSObjectReference.InvokeVoidAsync("observe", id);

    public ValueTask UnobserveAsync(string id) => JSObjectReference.InvokeVoidAsync("unobserve", id);

    public ValueTask DisposeAsync() => JSObjectReference.InvokeVoidAsync("dispose");
}
