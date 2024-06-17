﻿namespace Masa.Blazor;

public abstract class NextTickComponentBase : ComponentBase, IAsyncDisposable
{
    private readonly Queue<Func<Task>> _nextTickQueue = new();

    protected bool IsDisposed { get; private set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_nextTickQueue.Count > 0)
        {
            var queue = _nextTickQueue.ToArray();
            _nextTickQueue.Clear();

            foreach (var callback in queue)
            {
                if (IsDisposed)
                {
                    return;
                }

                await callback();
            }
            
            if (_nextTickQueue.Count > 0)
            {
                StateHasChanged();
            }
        }
    }

    protected void NextTick(Func<Task> callback)
    {
        _nextTickQueue.Enqueue(callback);
    }

    protected void NextTick(Action callback)
    {
        NextTick(() =>
        {
            callback.Invoke();
            return Task.CompletedTask;
        });
    }

    /// <summary>
    /// If the predicate is true, the callback will be executed in the next tick, otherwise it will be executed immediately.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="predicate"></param>
    protected async Task NextTickIf(Func<Task> callback, Func<bool> predicate)
    {
        if (predicate.Invoke())
        {
            NextTick(callback);
        }
        else
        {
            await callback.Invoke();
        }
    }

    /// <summary>
    /// If the predicate is true, the callback will be executed in the next tick, otherwise it will be executed immediately.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="predicate"></param>
    protected void NextTickIf(Action callback, Func<bool> predicate)
    {
        if (predicate.Invoke())
        {
            NextTick(callback);
        }
        else
        {
            callback.Invoke();
        }
    }

    protected async Task Retry(Func<Task> callback, Func<bool> @while, int retryTimes = 20, int delay = 100,
        CancellationToken cancellationToken = default)
    {
        if (retryTimes > 0 && !cancellationToken.IsCancellationRequested)
        {
            if (@while.Invoke())
            {
                retryTimes--;

                await Task.Delay(delay, cancellationToken);

                await Retry(callback, @while, retryTimes, delay, cancellationToken);
            }
            else
            {
                await callback.Invoke();
            }
        }
    }

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