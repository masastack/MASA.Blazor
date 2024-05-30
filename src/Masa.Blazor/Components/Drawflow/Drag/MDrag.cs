﻿namespace Masa.Blazor;

public class MDrag : MasaComponentBase
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

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-drag";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, Attributes);
        builder.AddAttribute(2, "draggable", "true");
        builder.AddAttribute(3, "class", GetClass());
        builder.AddAttribute(4, "style", GetStyle());
        builder.AddAttribute(5, DATA_VALUE_ATTR, DataValue);
        builder.AddContent(6, ChildContent);
        builder.AddElementReferenceCapture(7, e => _elementReference = e);
        builder.CloseComponent();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await JS.InvokeVoidAsync(JsInteropConstants.UnregisterDragEvent, _elementReference);
    }
}
