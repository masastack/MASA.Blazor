using Masa.Blazor.Mixins.Activatable;

namespace Masa.Blazor;

public class MHover : MActivatableBase
{
    [Parameter] public RenderFragment<HoverProps>? ChildContent { get; set; }

    public override Dictionary<string, object> ActivatorAttributes => new()
    {
        { ActivatorId, true }
    };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        OpenOnHover = true;
        OpenOnClick = false;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent?.Invoke(new HoverProps(IsActive, ActivatorAttributes)));
    }
}