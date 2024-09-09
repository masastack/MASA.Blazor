namespace Masa.Blazor.Components.ItemGroup;

/// <summary>
/// The base class for groupable components.
/// The active state can be controlled by its parent group component or IsActive property.
/// </summary>
/// <typeparam name="TGroup"></typeparam>
public abstract class MGroupable<TGroup> : MGroupableBase<TGroup>, IGroupable
    where TGroup : MItemGroupBase
{
    protected MGroupable(GroupType groupType) : base(groupType)
    {
    }

    protected MGroupable(GroupType groupType, bool bootable) : base(groupType, bootable)
    {
    }

    [Parameter]
    public bool IsActive
    {
        get => UserActive ?? false;
        set => UserActive = value;
    }
}