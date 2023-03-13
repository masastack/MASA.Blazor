using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Presets;

public class PWindowContent : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override bool ShouldRender() => false;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}
