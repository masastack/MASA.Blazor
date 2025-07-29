using System.Diagnostics.CodeAnalysis;
using Masa.Blazor.JSInterop;

namespace Masa.Blazor;

public class GridstackJSModule : JSModule
{
    private readonly DotNetObjectReference<GridstackJSModule> _dotNetObjectReference;
    private IJSObjectReference? _instance;

    public GridstackJSModule(IJSRuntime js) : base(js,
        "./_content/Masa.Blazor.JSComponents.Gridstack/gridstack-interop.js")
    {
        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    public event EventHandler<GridstackResizeEventArgs>? Resize;

    public async ValueTask Init(GridstackOptions options, ElementReference el)
    {
        _instance = await InvokeAsync<IJSObjectReference>("init", options, el, _dotNetObjectReference);
    }

    public ValueTask SetStatic(bool staticValue)
    {
        AssertInit();

        return _instance.InvokeVoidAsync("setStatic", staticValue);
    }

    [MemberNotNull(nameof(_instance))]
    private void AssertInit()
    {
        if (_instance is null)
        {
            throw new InvalidOperationException("Gridstack instance is not initialized, please call Init first.");
        }
    }

    public async ValueTask Reload()
    {
        AssertInit();

        await _instance.InvokeVoidAsync("reload");
    }

    public async ValueTask<List<GridstackWidget>> Save()
    {
        AssertInit();

        return await _instance.InvokeAsync<List<GridstackWidget>>("save");
    }

    [JSInvokable]
    public void OnResize(GridstackResizeEventArgs args)
    {
        Resize?.Invoke(this, args);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _dotNetObjectReference.Dispose();

        if (_instance is not null)
        {
            await _instance.DisposeAsync();
            _instance = null;
        }

        await base.DisposeAsyncCore();
    }
}