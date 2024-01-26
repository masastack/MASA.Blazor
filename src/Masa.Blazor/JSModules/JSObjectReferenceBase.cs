namespace Masa.Blazor.JSModules;

public class JSObjectReferenceBase : IAsyncDisposable
{
    protected IJSObjectReference JSObjectReference { get; init; }

    public JSObjectReferenceBase(IJSObjectReference jsObjectReference)
    {
        JSObjectReference = jsObjectReference;
    }

    public async ValueTask InvokeVoidAsync(string identity, params object[] args)
    {
        await JSObjectReference.InvokeVoidAsync("invokeVoid", identity, args);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await JSObjectReference.DisposeAsync();
    }
}
