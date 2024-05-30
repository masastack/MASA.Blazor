using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor.Components.DataTable;

public partial class MDataTableRow<TItem>
{
    [Parameter]
    public List<DataTableHeader<TItem>> Headers { get; set; } = null!;

    [Parameter]
    public TItem Item { get; set; } = default!;

    [Parameter]
    public int Index { get; set; }

    [Parameter]
    public Func<ItemColProps<TItem>, bool> HasSlot { get; set; } = null!;

    [Parameter]
    public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; } = null!;

    [Inject]
    private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public Func<TItem, bool>? IsSelected { get; set; }

    [Parameter]
    public Func<TItem, bool>? IsExpanded { get; set; }

    [Parameter]
    public Func<TItem, string?>? ItemClass { get; set; }

    [Parameter]
    public bool Stripe { get; set; }

    public bool IsStripe => Stripe && Index % 2 == 1;

    protected override IEnumerable<string?> BuildComponentClass()
    {
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

        yield return ItemClass?.Invoke(Item);
    }

    private string GetCellClass(DataTableHeader header)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"text-{header.Align.ToString().ToLowerInvariant()}");

        if (header.Divider)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append("m-data-table__divider");
        }

        if (header.Fixed == DataTableFixed.Right)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append("m-data-table__column--fixed-right");
        }

        if (header.Fixed == DataTableFixed.Left)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append("m-data-table__column--fixed-left");
        }

        if (header.IsFixedShadowColumn)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append("first-fixed-column");
        }

        if (header.HasEllipsis)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append("m-data-table__column--ellipsis");
        }

        stringBuilder.Append(' ');
        stringBuilder.Append(header.Class);
        
        return stringBuilder.ToString();
    }
    
    private string GetCellStyle(DataTableHeader<TItem> header)
    {
        var styleBuilder = StyleBuilder.Create();

        if (header.Fixed == DataTableFixed.Right)
        {
            var count = Headers.Count;
            var lastIndex = Headers.LastIndexOf(header);
            if (lastIndex > -1)
            {
                var widths = Headers.TakeLast(count - lastIndex - 1).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                styleBuilder.Add(MasaBlazor.RTL ? "left" : "right", $"{widths}px");
            }
        }
        else if (header.Fixed == DataTableFixed.Left)
        {
            var index = Headers.IndexOf(header);
            if (index > -1)
            {
                var widths = Headers.Take(index).Sum(u => u.Width?.ToDouble() ?? u.RealWidth);
                styleBuilder.Add(MasaBlazor.RTL ? "right" : "left", $"{widths}px");
            }
        }

        return styleBuilder.Build();
    }
}
