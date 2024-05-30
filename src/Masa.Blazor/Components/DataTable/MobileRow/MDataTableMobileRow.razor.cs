using Element = BemIt.Element;

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

    private static Block _block = new("m-data-table");
    private static Element _row = _block.Element("mobile-table-row");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _row.Name;

        if (IsSelected?.Invoke(Item) is true)
        {
            yield return "m-data-table__selected";
        }

        if (IsExpanded?.Invoke(Item) is true)
        {
            yield return "m-data-table__expanded m-data-table__expanded__row";
        }
        
        if (IsStripe)
        {
            yield return "stripe";
        }

        yield return ItemClass?.Invoke(Item) ?? "";

    }
}
