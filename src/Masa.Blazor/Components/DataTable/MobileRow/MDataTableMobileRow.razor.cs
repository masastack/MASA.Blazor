namespace Masa.Blazor.Components.DataTable;

public partial class MDataTableMobileRow<TItem>
{
    [Parameter]
    public List<DataTableHeader<TItem>> Headers { get; set; } = null!;

    [Parameter]
    public bool HideDefaultHeader { get; set; }

    [Parameter]
    public TItem Item { get; set; } = default!;

    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public Func<ItemColProps<TItem>, bool> HasSlot { get; set; } = null!;

    [Parameter]
    public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; } = null!;

    [Parameter]
    public RenderFragment<DataTableHeader>? HeaderColContent { get; set; }

    [Parameter]
    public Func<TItem, bool>? IsSelected { get; set; }

    [Parameter]
    public Func<TItem, bool>? IsExpanded { get; set; }

    [Parameter]
    public Func<TItem, string?>? ItemClass { get; set; }

    [Parameter]
    public bool Stripe { get; set; }

    public bool IsStripe => Stripe && Index % 2 == 1;

    protected override void SetComponentCss()
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
            .Apply("mobile-row", cssBuilder => { cssBuilder.Add("m-data-table__mobile-row"); })
            .Apply("mobile-row__header", cssBuilder => { cssBuilder.Add("m-data-table__mobile-row__header"); })
            .Apply("mobile-row__cell", cssBuilder => { cssBuilder.Add("m-data-table__mobile-row__cell"); });
    }
}
