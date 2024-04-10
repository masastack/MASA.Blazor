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
        builder.AddMultipleAttributes(4, Attributes);
        builder.AddElementReferenceCapture(5, r => Ref = r);
        builder.AddContent(6, GenChildContent());
        builder.CloseElement();
    }

    protected virtual RenderFragment? GenChildContent()
    {
        return ChildContent;
    }
}

public abstract class ThemeContainer : Container
{
    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<bool>>(0);
        builder.AddAttribute(1, "Value", IsDark);
        builder.AddAttribute(2, "Name", "IsDark");
        builder.AddAttribute(3, "ChildContent", (RenderFragment)base.BuildRenderTree);
        builder.CloseComponent();
    }
}