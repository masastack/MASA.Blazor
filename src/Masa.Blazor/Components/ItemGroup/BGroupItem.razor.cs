using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public abstract class BGroupItem<TGroup> : BGroupable<TGroup>
    where TGroup : ItemGroupBase
{
    protected BGroupItem(GroupType groupType) : base(groupType)
    {
    }

    protected BGroupItem(GroupType groupType, bool bootable) : base(groupType, bootable)
    {
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }
}