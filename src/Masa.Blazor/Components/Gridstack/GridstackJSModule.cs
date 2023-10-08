namespace Masa.Blazor;

public class GridstackJSModule : JSModule
{
    private readonly DotNetObjectReference<GridstackJSModule> _dotNetObjectReference;

    public GridstackJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/gridstack-proxy.js")
    {
        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    public event EventHandler<GridstackResizeEventArgs>? Resize;

    public async ValueTask<IJSObjectReference> Init(GridstackOptions options, ElementReference el)
        => await InvokeAsync<IJSObjectReference>("init", options, el, _dotNetObjectReference);

    public async ValueTask SetStatic(IJSObjectReference instance, bool staticValue)
        => await InvokeVoidAsync("setStatic", instance, staticValue);

    public async ValueTask<IJSObjectReference> Reload(IJSObjectReference instance)
        => await InvokeAsync<IJSObjectReference>("reload", instance);

    public async ValueTask<List<GridstackWidget>> Save(IJSObjectReference instance)
        => await InvokeAsync<List<GridstackWidget>>("save", instance);

    [JSInvokable]
    public void OnResize(GridstackResizeEventArgs args)
    {
        Resize?.Invoke(this, args);
    }

    public override async ValueTask DisposeAsync()
    {
        _dotNetObjectReference.Dispose();
        await base.DisposeAsync();
    }
}
