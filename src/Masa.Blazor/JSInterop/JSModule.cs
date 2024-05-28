namespace BlazorComponent.JSInterop;

/// <summary>
/// Helper for loading any JavaScript (ES6) module and calling its exports
/// </summary>
public abstract class JSModule : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    protected readonly CancellationTokenSource _cts = new();
    private bool _isDisposed;

    protected JSModule(IJSRuntime js, string moduleUrl)
        => _moduleTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask());

    protected async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        if (_cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            await module.InvokeVoidAsync(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }

    protected async ValueTask<T?> InvokeAsync<T>(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        if (_cts.Token.IsCancellationRequested)
        {
            return default;
        }

        try
        {
            return await module.InvokeAsync<T>(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            return default;
        }
    }

    protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        _cts.Cancel();

        if (_moduleTask.IsValueCreated && !_moduleTask.Value.IsFaulted)
        {
            var module = await _moduleTask.Value;

            try
            {
                await DisposeAsyncCore().ConfigureAwait(false);
                await module.DisposeAsync().ConfigureAwait(false);
            }
            catch (JSDisconnectedException)
            {
                // ignored
            }
            // HACK: remove this after https://github.com/dotnet/aspnetcore/issues/52119 is fixed
            catch (JSException e) when (e.Message.Contains("has it been disposed", StringComparison.InvariantCulture)
                                        && (OperatingSystem.IsWindows() || OperatingSystem.IsAndroid() ||
                                            OperatingSystem.IsIOS()))
            {
                // ignored
            }
            catch (InvalidOperationException e) when (e.Message.Contains("prerendering",
                                                          StringComparison.InvariantCulture))
            {
                // ignored
            }
        }

        _isDisposed = true;
        //_cts.Dispose();
        GC.SuppressFinalize(this);
    }
}