namespace Masa.Blazor;

public class MAppBarNavIcon : BDomComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private RenderFragment DefaultChildContent => builder =>
    {
        builder.OpenComponent<MIcon>(0);
        builder.AddAttribute(1, "Icon", (Icon)"$menu");
        builder.CloseComponent();
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<MButton>(0);
        builder.AddAttribute(1, "Class", $"m-app-bar__nav-icon {Class}");
        builder.AddAttribute(2, nameof(MButton.Icon), true);
        builder.AddMultipleAttributes(3, Attributes);
        builder.AddAttribute(4, "ChildContent", ChildContent ?? DefaultChildContent);
        builder.CloseComponent();
    }
}