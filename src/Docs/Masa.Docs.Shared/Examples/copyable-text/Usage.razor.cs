using Masa.Blazor.Presets;
namespace Masa.Docs.Shared.Examples.copyable_text;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(PCopyableText))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, "There is a text that can be copied.");
    };
}
