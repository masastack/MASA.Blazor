namespace Masa.Blazor.MasaTable;

public class Record
{
    public IEnumerable<RecordColumn> Columns { get; set; }
}

public class RecordColumn(ColumnType type, string jsonValue)
{
    public Guid ColumnId { get; set; }

    public ColumnType Type { get; set; } = type;

    public string Value { get; set; } = jsonValue;
}