using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Docs.Shared;

// TODO: move to BlazorComponent
public static class RenderTreeBuilderExtensions
{
    public static void AddChildContent(this RenderTreeBuilder builder, int sequence, string content)
    {
        builder.AddAttribute(sequence, "ChildContent", new RenderFragment(childBuilder => childBuilder.AddContent(0, content)));
    }
}
