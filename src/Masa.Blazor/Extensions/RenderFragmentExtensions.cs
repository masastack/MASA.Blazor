using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorComponent;

public static class RenderFragmentExtensions
{
    public static string GetTextContent(this RenderFragment renderFragment)
    {
#pragma warning disable BL0006 // Do not use RenderTree types
        var builder = new RenderTreeBuilder();
        renderFragment?.Invoke(builder);

        var frame = builder.GetFrames().Array
                           .FirstOrDefault(u => u.FrameType is RenderTreeFrameType.Text or RenderTreeFrameType.Markup);

        char[] charsToTrim = { '\r', ' ', '\n' };
        return frame.TextContent.Trim(charsToTrim);
#pragma warning restore BL0006 // Do not use RenderTree types
    }
}
