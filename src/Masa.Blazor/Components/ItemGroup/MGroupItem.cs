using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public abstract class MGroupItem<TGroup> : MGroupable<TGroup>
    where TGroup : ItemGroupBase
{
    protected MGroupItem(GroupType groupType) : base(groupType)
    {
    }

    protected MGroupItem(GroupType groupType, bool bootable) : base(groupType, bootable)
    {
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }
}