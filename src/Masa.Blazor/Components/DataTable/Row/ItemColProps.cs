namespace Masa.Blazor;

public class ItemColProps<TItem>
{
    public DataTableHeader<TItem> Header { get; set; }

    public object? Value => Header.ItemValue.Invoke(Item);

    public TItem Item { get; set; }

    public ItemColProps(DataTableHeader<TItem> header, TItem item)
    {
        Header = header;
        Item = item;
    }
}