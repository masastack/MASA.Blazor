namespace Masa.Blazor.Docs.Examples.components.subheaders;

public class Usage : Components.Usage
{
    public Usage() : base(typeof(MSubheader))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, "Subheader");
    };
}
