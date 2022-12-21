namespace Masa.Blazor.Docs.Examples.components.expansion_panels;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MExpansionPanel)) { }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };
}