namespace Masa.Blazor.Core;

public abstract class Container : MasaComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected virtual string TagName => "div";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        builder.AddAttribute(1, "class", GetClass());
        builder.AddAttribute(2, "style", GetStyle());
        builder.AddAttribute(3, "id", Id);
        builder.AddElementReferenceCapture(4, r => Ref = r);
        builder.AddContent(5, ChildContent);
        builder.CloseElement();
    }
}