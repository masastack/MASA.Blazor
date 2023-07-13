using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Playground.Pages;

public class MTransition : ComponentBase
{
    [Parameter] public string? Name { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool LeaveAbsolute { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var sequence = 0;
        builder.OpenComponent<CascadingValue<MTransition>>(sequence++);

        builder.AddAttribute(sequence++, nameof(CascadingValue<MTransition>.Value), this);
        builder.AddAttribute(sequence++, nameof(CascadingValue<MTransition>.IsFixed), true);
        builder.AddAttribute(sequence++, nameof(CascadingValue<MTransition>.ChildContent), ChildContent);

        builder.CloseComponent();
    }
}
