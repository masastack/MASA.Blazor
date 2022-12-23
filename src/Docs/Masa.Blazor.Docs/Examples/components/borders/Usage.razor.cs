namespace Masa.Blazor.Docs.Examples.components.borders;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Border>(0);
        builder.CloseComponent();
    };

    public Usage() : base(typeof(MBorder)) { }
}