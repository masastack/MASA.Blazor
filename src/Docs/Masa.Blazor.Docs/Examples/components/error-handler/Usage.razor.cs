namespace Masa.Blazor.Docs.Examples.components.error_handler;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MErrorHandler)) { }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };
}