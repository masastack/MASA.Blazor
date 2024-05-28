namespace BlazorComponent;

public class JSObjectReferenceProxy : IJSObjectReference
{
    private readonly IJSObjectReference _jsObjectReference;

    protected JSObjectReferenceProxy(IJSObjectReference jsObjectReference)
    {
        _jsObjectReference = jsObjectReference;
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[]? args)
        => _jsObjectReference.InvokeAsync<TValue>(identifier, args);

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, params object?[]? args)
        => _jsObjectReference.InvokeAsync<TValue>(identifier, cancellationToken, args);

    public ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
        => _jsObjectReference.InvokeVoidAsync(identifier, args);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await DisposeAsync();
            await _jsObjectReference.DisposeAsync();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected virtual ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
