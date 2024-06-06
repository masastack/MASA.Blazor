namespace Masa.Blazor.JSModules;

public class JSObjectReferenceBase(IJSObjectReference jsObjectReference) : IAsyncDisposable
{
    protected IJSObjectReference JSObjectReference { get; init; } = jsObjectReference;

    // TODO: may i rename this method to InvokeTargetMethodAsync?
    public async ValueTask InvokeVoidAsync(string identity, params object[] args)
    {
        await JSObjectReference.InvokeVoidAsync("invokeVoid", args.Prepend(identity).ToArray());
    }

    public async ValueTask DisposeAsync()
    {
        // TODO: need try-catch?
        await JSObjectReference.DisposeAsync();
        await DisposeAsyncCore();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await JSObjectReference.DisposeAsync();
    }
}