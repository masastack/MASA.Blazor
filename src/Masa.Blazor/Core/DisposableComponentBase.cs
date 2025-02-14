namespace Masa.Blazor.Core;

public abstract class DisposableComponentBase : ComponentBase, IAsyncDisposable
{
    protected bool IsDisposed { get; private set; }

    protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        if (IsDisposed)
        {
            return;
        }

        try
        {
            await DisposeAsyncCore().ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
        // HACK: remove this after https://github.com/dotnet/aspnetcore/issues/52119 is fixed
        catch (JSException e) when (e.Message.Contains("has it been disposed")
                                    && (OperatingSystem.IsWindows() || OperatingSystem.IsAndroid() ||
                                        OperatingSystem.IsIOS()))
        {
            // ignored
        }
        catch (InvalidOperationException e) when (e.Message.Contains("prerendering"))
        {
            // ignored
        }

        GC.SuppressFinalize(this);
        IsDisposed = true;
    }
}