using Masa.Blazor.Components.Drawflow;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDrawflow : MDrop
{
    [Inject] private DrawflowJSModule DrawflowJSModule { get; set; } = null!;

    [Parameter] public DrawflowEditorMode Mode { get; set; }

    private DrawflowEditorMode? _prevMode;
    private IDrawflowJSObjectReferenceProxy? _drawflowProxy;

    private Block Block => new("m-drawflow");

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
            _drawflowProxy = await DrawflowJSModule.Init(ElementReference.GetSelector()!, Mode);
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

        return await _drawflowProxy.AddNodeAsync(name, inputs, outputs, clientX, clientY, offsetX, offsetY, className, data, html).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task RemoveNodeAsync(int id)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.RemoveNodeAsync(id).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task UpdateNodeDataAsync(int id, object data)
    {
        if (_drawflowProxy == null) return;

        await _drawflowProxy.UpdateNodeDataAsync(id, data).ConfigureAwait(false);
    }

    [ApiPublicMethod]
    public async Task<string?> ExportAsync(bool withoutData = false)
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.ExportAsync(withoutData).ConfigureAwait(false);
    }
}
