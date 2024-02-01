namespace Masa.Blazor.Core;

public abstract class Container : BDomComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected virtual string TagName => "div";

    protected abstract string ClassName { get; }

    protected override void SetComponentCss()
    {
        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add(ClassName); });
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        builder.AddAttribute(1, "class", CssProvider.GetClass());
        builder.AddAttribute(2, "style", CssProvider.GetStyle());
        builder.AddAttribute(3, "id", Id);
        builder.AddElementReferenceCapture(4, r => Ref = r);
        builder.AddContent(5, ChildContent);
        builder.CloseElement();
    }
}