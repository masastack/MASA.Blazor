using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Playground.Pages;

public class MIfTransitionElement : MToggleTransitionElement
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (LazyValue)
        {
            base.BuildRenderTree(builder);
        }
    }
}
