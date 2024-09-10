namespace Masa.Blazor.MasaTable;

public class ColumnTemplate<TItem, TValue>
{
    public Func<TItem, TValue>? Value { get; }

    public Column Column { get; internal set; }

    internal ViewColumn ViewColumn { get; set; }

    public ColumnTemplate(string id, ColumnType type)
    {
        Column = new()
        {
            Id = id,
            Type = type
        };

        ViewColumn = new ViewColumn()
        {
            ColumnId = id
        };
    }

    public ColumnTemplate(string id, ColumnType type, Func<TItem, TValue> value) : this(id, type)
    {
        Value = value;
    }
}