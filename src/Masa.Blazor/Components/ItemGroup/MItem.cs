using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public class MItem : BGroupable<ItemGroupBase>
{
    public MItem() : base(GroupType.ItemGroup)
    {
    }

    public MItem(GroupType groupType) : base(groupType)
    {
    }

    [Parameter] public RenderFragment<ItemContext>? ChildContent { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Ref = RefBack.Current;
        }
    }

    private ItemContext GenItemContext()
    {
        return new ItemContext(InternalIsActive, InternalIsActive ? ComputedActiveClass : "", ToggleAsync, RefBack,
            Value);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent?.Invoke(GenItemContext()));
    }
}