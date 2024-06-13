﻿using Masa.Blazor.Components.ItemGroup;
using Masa.Blazor.Mixins;

namespace Masa.Blazor;

public abstract class MGroupItem<TGroup> : MGroupable<TGroup>
    where TGroup : MItemGroupBase
{
    protected MGroupItem(GroupType groupType) : base(groupType)
    {
    }

    protected MGroupItem(GroupType groupType, bool bootable) : base(groupType, bootable)
    {
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }
}