namespace Masa.Blazor.Components.NavigationDrawer;

public class Touch : IAsyncDisposable
{
    private readonly Action<bool, double> _onMove;
    private readonly Action<bool> _onEnd;
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly DotNetObjectReference<Touch> _dotNetObjectReference;

    private bool _prevIsDragging;
    private double _prevDragProgress;

    public Touch(IJSRuntime jsRuntime, Action<bool, double> onMove, Action<bool> onEnd)
    {
        _onMove = onMove;
        _onEnd = onEnd;
        _dotNetObjectReference = DotNetObjectReference.Create(this);

        _moduleTask = new Lazy<Task<IJSObjectReference>>(
            () => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                $"./_content/Masa.Blazor/js/{JSManifest.NavigationDrawerTouchJs}").AsTask()
        );
    }

    public async ValueTask<TouchJSObjectResult> UseTouchAsync(ElementReference el, TouchState state)
    {
        var moduleTask = await _moduleTask.Value;
        var jsObjectReference =
            await moduleTask.InvokeAsync<IJSObjectReference>("useTouch", el, _dotNetObjectReference, state);
        return CreateTouchJSObjectResult(jsObjectReference);
    }

    public async ValueTask<TouchJSObjectResult> UseTouchAsync(string selector, TouchState state)
    {
        var moduleTask = await _moduleTask.Value;
        var jsObjectReference =
            await moduleTask.InvokeAsync<IJSObjectReference>("useTouch", selector, _dotNetObjectReference, state);
        return CreateTouchJSObjectResult(jsObjectReference);
    }

    private TouchJSObjectResult CreateTouchJSObjectResult(IJSObjectReference jsObjectReference)
    {
        return new TouchJSObjectResult(SyncState, Dispose);

        void SyncState(TouchState newState)
        {
            _ = jsObjectReference.InvokeVoidAsync("syncState", newState);
        }

        void Dispose()
        {
            _ = jsObjectReference.InvokeVoidAsync("dispose");
            _ = jsObjectReference.DisposeAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    [JSInvokable]
    public void TouchMove(bool isDragging, double dragProgress)
    {
        if (_prevIsDragging == isDragging && !(Math.Abs(_prevDragProgress - dragProgress) > 0.000001)) return;

        _prevDragProgress = dragProgress;
        _prevIsDragging = isDragging;

        _onMove.Invoke(isDragging, dragProgress);
    }

    [JSInvokable]
    public void TouchEnd(bool isActive)
    {
        _onEnd.Invoke(isActive);
    }
}

public record TouchJSObjectResult(Action<TouchState> SyncState, Action Un);

public record TouchState(bool IsActive, bool IsTemporary, double Width, bool Touchless, string Position);