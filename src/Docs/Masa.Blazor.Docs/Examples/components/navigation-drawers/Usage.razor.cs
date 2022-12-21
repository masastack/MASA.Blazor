namespace Masa.Blazor.Docs.Examples.components.navigation_drawers;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };

    public Usage() : base(typeof(MNavigationDrawer))
    {
    }
}