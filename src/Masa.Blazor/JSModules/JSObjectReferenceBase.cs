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
        await DisposeAsyncCore();
        await JSObjectReference.DisposeAsync();
    }

    protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;
}