﻿#nullable enable

using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PPageContainerItem : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Value { get; set; }

    protected override bool ShouldRender() => Value;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}
