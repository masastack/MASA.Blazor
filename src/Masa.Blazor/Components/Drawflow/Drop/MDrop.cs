﻿namespace Masa.Blazor;

public class MDrop : MasaComponentBase
{
    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback<ExDragEventArgs> OnDrop { get; set; }

    protected ElementReference ElementReference { get; private set; }

    private readonly ModifierBuilder _modifierBuilder = new Block("m-drop").CreateModifierBuilder();
    
    protected virtual string ClassString => _modifierBuilder.AddClass(Class).Build();

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(2, "class", ClassString);
        builder.AddAttribute(3, "style", Style);
        builder.AddAttribute(4, "onexdrop", EventCallback.Factory.Create(this, OnDrop));
        builder.AddAttribute(5, "ondragover", EventCallback.Empty);
        builder.AddAttribute(6, "__internal_preventDefault_ondragover", true);
        builder.AddContent(7, ChildContent);
        builder.AddElementReferenceCapture(8, e => ElementReference = e);
        builder.CloseComponent();
    }

    private async Task HandleOnDragOver()
    {
        // ignored
    }
}
