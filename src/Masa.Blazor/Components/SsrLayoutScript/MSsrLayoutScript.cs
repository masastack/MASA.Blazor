#if NET8_0_OR_GREATER
using BlazorPageScript;
using Microsoft.AspNetCore.Components.Rendering;
#endif

namespace Masa.Blazor;

public class MSsrLayoutScript : ComponentBase
{
#if NET8_0_OR_GREATER
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<PageScript>(0);
        builder.AddAttribute(1, nameof(PageScript.Src), "./_content/Masa.Blazor/js/ssr-layout-script.js");
        builder.CloseComponent();
    }
#endif
}
