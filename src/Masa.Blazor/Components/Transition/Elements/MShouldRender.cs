namespace Masa.Blazor;

public class MShouldRender : ComponentBase
{
    [Parameter] public bool Value { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override bool ShouldRender()
    {
        return Value;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}