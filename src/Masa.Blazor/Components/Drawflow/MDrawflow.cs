using Masa.Blazor.Components.Drawflow;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDrawflow : MDrop, IAsyncDisposable
{
    [Inject] private DrawflowJSModule DrawflowJSModule { get; set; } = null!;

    [Parameter] public DrawflowEditorMode Mode { get; set; }

    [Parameter] public EventCallback<string> OnNodeCreated { get; set; }

    [Parameter] public EventCallback<string> OnNodeRemoved { get; set; }

    [Parameter] public EventCallback<string> OnNodeSelected { get; set; }

    [Parameter] public EventCallback<string> OnNodeUnselected { get; set; }

    [Parameter] public EventCallback<string> OnNodeDataChanged { get; set; }

    [Parameter] public EventCallback OnImport { get; set; }

    private DrawflowEditorMode? _prevMode;
    private IDrawflowJSObjectReferenceProxy? _drawflowProxy;
    private DotNetObjectReference<object>? _interopHandleReference;

    protected override string ClassString => new Block("m-drawflow").Modifier(Mode, "mode").AddClass(base.ClassString).Build();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevMode.HasValue && _prevMode != Mode)
        {
            _prevMode = Mode;
            _drawflowProxy?.SetMode(Mode);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        { 
            _interopHandleReference = DotNetObjectReference.Create<object>(new DrawflowInteropHandle(this));
            _drawflowProxy = await DrawflowJSModule.Init(ElementReference.GetSelector()!, _interopHandleReference, Mode);
        }
    }

    [MasaApiPublicMethod]
    public async Task<string?> AddNodeAsync(
        string name,
        int inputs,
        int outputs,
        double clientX,
        double clientY,
        double offsetX,
        double offsetY,
        string? className,
        object? data,
        string html)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.AddNodeAsync(name, inputs, outputs, clientX, clientY, offsetX, offsetY, className, data, html)
                                   .ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task RemoveNodeAsync(string nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveNodeAsync(nodeId).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task<DrawflowNode<TData>?> GetNodeFromIdAsync<TData>(string nodeId)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.GetNodeFromIdAsync<TData>(nodeId).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task UpdateNodeDataAsync(string nodeId, object data)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.UpdateNodeDataAsync(nodeId, data).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task UpdateNodeHTMLAsync(string nodeId, string html)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.UpdateNodeHTMLAsync(nodeId, html).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task ClearAsync()
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.ClearAsync().ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task ImportAsync(string json)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.ImportAsync(json).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task<string?> ExportAsync(bool indented = false)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.ExportAsync(indented).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task AddInputAsync(string nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.AddInputAsync(nodeId).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task AddOutputAsync(string nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.AddOutputAsync(nodeId).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task RemoveInputAsync(string nodeId, string inputClass)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveInputAsync(nodeId, inputClass).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task RemoveOutputAsync(string nodeId, string outputClass)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveOutputAsync(nodeId, outputClass).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task FocusNodeAsync(string nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.FocusNodeAsync(nodeId).ConfigureAwait(false);
    }

    [MasaApiPublicMethod]
    public async Task CenterNodeAsync(string nodeId, bool animate = true)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.CenterNodeAsync(nodeId, animate).ConfigureAwait(false);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            _interopHandleReference?.Dispose();

            if (_drawflowProxy != null)
            {
                await _drawflowProxy.DisposeAsync();
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
