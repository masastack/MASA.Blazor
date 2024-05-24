namespace Masa.Blazor;

public class IfTransitionElement : ToggleableTransitionElement
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (LazyValue)
        {
            base.BuildRenderTree(builder);
        }
    }
}