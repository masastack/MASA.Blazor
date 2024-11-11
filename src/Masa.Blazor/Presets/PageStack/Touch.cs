namespace Masa.Blazor.Presets.PageStack;

public class Touch : IAsyncDisposable
{
    private readonly Action<bool> _onEnd;
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly DotNetObjectReference<Touch> _dotNetObjectReference;

    public Touch(IJSRuntime jsRuntime, Action<bool> onEnd)
    {
        _onEnd = onEnd;
        _dotNetObjectReference = DotNetObjectReference.Create(this);

        _moduleTask = new Lazy<Task<IJSObjectReference>>(
            () => jsRuntime
                .InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/components/page-stack-touch.js")
                .AsTask());
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
        return new TouchJSObjectResult(Dispose);

        void Dispose()
        {
            _ = jsObjectReference.InvokeVoidAsync("dispose");
            _ = jsObjectReference.DisposeAsync();
        }
    }

    [JSInvokable]
    public void TouchEnd(bool isActive)
    {
        _onEnd.Invoke(isActive);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}

public record TouchJSObjectResult(Action Un);

public record TouchState(bool IsActive, string Position);