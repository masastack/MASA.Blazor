namespace Masa.Blazor.Docs.Examples.components.item_groups;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MItemGroup)) { }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<Index>(0);
        builder.CloseComponent();
    };
}