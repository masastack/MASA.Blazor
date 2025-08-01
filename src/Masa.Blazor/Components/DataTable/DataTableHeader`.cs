namespace Masa.Blazor;

public class DataTableHeader<TItem> : DataTableHeader
{
    public DataTableHeader()
    {
    }

    public DataTableHeader(string text, string value, Func<TItem, object?>? valueExpression = null)
    {
        Text = text;
        Value = value;
        ValueExpression = valueExpression;
    }

    public Func<object?, string?, TItem, bool>? Filter { get; set; }

    public bool Filterable { get; set; } = true;

    /// <summary>
    /// Simple way to render the cell content.
    /// </summary>
    public Func<TItem, OneOf<string, RenderFragment>>? CellRender { get; set; }

    /// <summary>
    /// Gets the expression for the item property value to be displayed in the column.
    /// It is recommended to set this property;
    /// otherwise the system will use reflection to get the property value, which may affect performance.
    /// </summary>
    public Func<TItem, object?>? ValueExpression { get; init; }

    public ItemValue<TItem>? ItemValue => field ??= GetItemValue();

    private ItemValue<TItem>? GetItemValue()
    {
        var hasValue = Value != null;
        return !hasValue ? null : new ItemValue<TItem>(Value, ValueExpression);
    }
}