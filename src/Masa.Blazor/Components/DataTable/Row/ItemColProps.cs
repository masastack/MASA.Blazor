namespace Masa.Blazor;

public class ItemColProps<TItem>(DataTableHeader<TItem> header, TItem item)
{
    public DataTableHeader<TItem> Header { get; } = header;

    public TItem Item { get; } = item;

    /// <summary>
    /// The value of current cell.
    /// It's recommended to use <see cref="ValueContent"/>
    /// to render cell content as it has better compatibility.
    /// </summary>
    public object? Value
    {
        get
        {
            if (Header.CellRender is not null)
            {
                var render = Header.CellRender.Invoke(Item);
                if (render.IsT0)
                {
                    return render.AsT0;
                }

                return render.AsT1;
            }

            return Header.ItemValue.Invoke(Item);
        }
    }

    /// <summary>
    /// The render fragment of current cell.
    /// </summary>
    public RenderFragment ValueContent
        => Value as RenderFragment ?? (RenderFragment)(builder => builder.AddContent(0, Value));
}