using Masa.Blazor.Presets;
namespace Masa.Blazor.Docs.Examples.components.copyable_text;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(PCopyableText))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, "There is a text that can be copied.");
    };
}
