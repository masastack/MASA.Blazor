namespace Masa.Blazor;

public class DataTableHeader<TItem> : DataTableHeader
{
    private ItemValue<TItem>? _itemValue;

    public DataTableHeader()
    {
    }

    public DataTableHeader(string text, string value)
        : base(text, value)
    {
    }

    public ItemValue<TItem> ItemValue
    {
        get
        {
            if (_itemValue == null)
            {
                _itemValue = new ItemValue<TItem>(Value);
            }

            return _itemValue;
        }
    }

    public Func<object?, string?, TItem, bool>? Filter { get; set; }

    public bool Filterable { get; set; } = true;

    public Func<TItem, OneOf<string, RenderFragment>>? CellRender { get; set; }
}