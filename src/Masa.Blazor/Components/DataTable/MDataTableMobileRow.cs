namespace Masa.Blazor;

public class MDataTableMobileRow<TItem> : BDataTableMobileRow<TItem>
{
    [Parameter]
    public Func<TItem, bool>? IsSelected { get; set; }

    [Parameter]
    public Func<TItem, bool>? IsExpanded { get; set; }

    [Parameter]
    public Func<TItem, string?>? ItemClass { get; set; }

    [Parameter]
    public bool Stripe { get; set; }

    public bool IsStripe => Stripe && Index % 2 == 1;

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(cssBuilder =>
            {
                cssBuilder
                    .Add("m-data-table__mobile-table-row")
                    .AddIf("m-data-table__selected", () => IsSelected != null && IsSelected.Invoke(Item))
                    .AddIf("m-data-table__expanded m-data-table__expanded__row", () => IsExpanded != null && IsExpanded(Item))
                    .AddIf("stripe", () => IsStripe)
                    .Add(() => ItemClass?.Invoke(Item) ?? "");
            })
            .Apply("mobile-row", cssBuilder =>
            {
                cssBuilder.Add("m-data-table__mobile-row");
            })
            .Apply("mobile-row__header", cssBuilder =>
            {
                cssBuilder.Add("m-data-table__mobile-row__header");
            })
            .Apply("mobile-row__cell", cssBuilder =>
            {
                cssBuilder.Add("m-data-table__mobile-row__cell");
            });
    }
}
