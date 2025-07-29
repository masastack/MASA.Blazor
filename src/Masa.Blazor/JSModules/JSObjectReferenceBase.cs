namespace Masa.Blazor.JSModules;

public class JSObjectReferenceBase(IJSObjectReference jsObjectReference) : IAsyncDisposable
{
    protected IJSObjectReference JSObjectReference { get; private set; } = jsObjectReference;

    /// <summary>
    /// Invokes a JavaScript function on the JSObjectReference instance with the specified identity and arguments.
    /// For example, there is a js wrapper for echarts, and the invoke method is used to call the echarts instance method,
    /// but return void.
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="args"></param>
    public async ValueTask InvokeInstanceVoidAsync(string identity, params object[] args)
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