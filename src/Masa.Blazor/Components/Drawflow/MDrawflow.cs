using Masa.Blazor.Components.Drawflow;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDrawflow : MDrop
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
            var interopHandleReference = DotNetObjectReference.Create<object>(new DrawflowInteropHandle(this));
            _drawflowProxy = await DrawflowJSModule.Init(ElementReference.GetSelector()!, interopHandleReference, Mode);
        }
    }

    [ApiPublicMethod]
    public async Task<int> AddNodeAsync(
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
        if (_drawflowProxy == null) return 0;

        return await _drawflowProxy.AddNodeAsync(name, inputs, outputs, clientX, clientY, offsetX, offsetY, className, data, html)
                                   .ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task RemoveNodeAsync(int nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveNodeAsync(nodeId).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task<DrawflowNode<TData>?> GetNodeFromIdAsync<TData>(string nodeId)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.GetNodeFromIdAsync<TData>(nodeId).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task UpdateNodeDataAsync(int nodeId, object data)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.UpdateNodeDataAsync(nodeId, data).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task UpdateNodeHTMLAsync(int nodeId, string html)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.UpdateNodeHTMLAsync(nodeId, html).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task ClearAsync()
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.ClearAsync().ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task ImportAsync(string json)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.ImportAsync(json).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task<string?> ExportAsync(bool withoutData = false, bool indented = false)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.ExportAsync(withoutData, indented).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task AddInputAsync(int nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.AddInputAsync(nodeId).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task AddOutputAsync(int nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.AddOutputAsync(nodeId).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task RemoveInputAsync(int nodeId, string inputClass)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveInputAsync(nodeId, inputClass).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task RemoveOutputAsync(int nodeId, string outputClass)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveOutputAsync(nodeId, outputClass).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task FocusNodeAsync(string nodeId)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.FocusNodeAsync(nodeId).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task CenterNodeAsync(string nodeId, bool animate = true)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.CenterNodeAsync(nodeId, animate).ConfigureAwait(false);
    }
}
