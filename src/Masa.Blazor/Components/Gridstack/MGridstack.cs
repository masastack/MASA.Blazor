namespace Masa.Blazor;

public class MGridstack<TItem> : BGridstack<TItem>
{
    [Parameter]
    public string? ItemClass { get; set; }

    [Parameter]
    public string? ItemStyle { get; set; }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(cssBuilder => cssBuilder.Add("m-gridstack"))
                   .Apply("item",
                       cssBuilder => cssBuilder.Add("m-gridstack-item").Add(ItemClass),
                       styleBuilder => styleBuilder.Add(ItemStyle));
    }
}
