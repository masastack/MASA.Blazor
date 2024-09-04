namespace Masa.Blazor.MasaTable;

public class ColumnTemplate<TItem, TValue>
{
    public Func<TItem, TValue>? Value { get; }

    public Column Column { get; internal set; }

    internal ViewColumn ViewColumn { get; set; }

    public ColumnTemplate(string id, ColumnType type, Func<TItem, TValue> value)
    {
        Column = new()
        {
            Id = id,
            Type = type
        };

        Value = value;
        // ViewColumn = new(Column);
        ViewColumn = new ViewColumn()
        {
            ColumnId = id
        };
    }
}