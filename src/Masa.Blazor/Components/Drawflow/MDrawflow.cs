using Masa.Blazor.Components.Drawflow;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDrawflow : BDomComponentBase
{
    [Inject] private DrawflowJSModule DrawflowJSModule { get; set; } = null!;

    [Parameter] public DrawflowEditorMode Mode { get; set; }

    private DrawflowEditorMode? _prevMode;
    private IDrawflowJSObjectReferenceProxy? _drawflowProxy;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddAttribute(2, "id", Id);
        builder.AddAttribute(3, "class", CssProvider.GetClass());
        builder.AddAttribute(4, "style", CssProvider.GetStyle());
        builder.AddElementReferenceCapture(5, e => Ref = e);
        builder.CloseElement();
    }

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
            _drawflowProxy = await DrawflowJSModule.Init($"#{Id}", Mode);
        }
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(cssBuilder => cssBuilder.Add("m-drawflow"));
    }

    [ApiPublicMethod]
    public async Task AddNodeAsync(
        string name,
        int inputs,
        int outputs,
        int positionX,
        int positionY,
        string? className,
        object? data,
        string html)
    {
        if (_drawflowProxy != null)
        {
            await _drawflowProxy.AddNodeAsync(name, inputs, outputs, positionX, positionY, className, data, html).ConfigureAwait(false);
        }
    }

    [ApiPublicMethod]
    public async Task RemoveNodeByIdAsync(int id)
    {
        if (_drawflowProxy != null)
        {
            await _drawflowProxy.RemoveNodeByIdAsync(id).ConfigureAwait(false);
        }
    }

    [ApiPublicMethod]
    public async Task<string?> ExportAsync()
    {
        if (_drawflowProxy == null) return null;

        return await _drawflowProxy.ExportAsync().ConfigureAwait(false);
    }
}
