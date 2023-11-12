using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor;

public class MDrag : BDomComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JS { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? DataValue { get; set; }

    private const string DATA_VALUE_ATTR = "data-value";

    private ElementReference _elementReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.RegisterDragEvent, _elementReference, DATA_VALUE_ATTR);
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddAttribute(2, "draggable", "true");
        builder.AddAttribute(3, "class", Class);
        builder.AddAttribute(4, "style", Style);
        builder.AddAttribute(5, DATA_VALUE_ATTR, DataValue);
        builder.AddContent(6, ChildContent);
        builder.AddElementReferenceCapture(7, e => _elementReference = e);
        builder.CloseComponent();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await JS.InvokeVoidAsync(JsInteropConstants.UnregisterDragEvent, _elementReference);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
