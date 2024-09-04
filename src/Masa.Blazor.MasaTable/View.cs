using System.Text.Json.Serialization;

namespace Masa.Blazor.MasaTable;

public class View
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public ViewType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RowHeight RowHeight { get; set; }

    public List<ViewColumn> Columns { get; set; } = [];
}

public enum RowHeight
{
    Low,
    Medium,
    High
}

public class ViewColumn
{
    [JsonPropertyName("id")] public string ColumnId { get; init; }

    public int Width { get; set; }

    public bool Hidden { get; set; }

    // [JsonIgnore]
    // internal Column Column { get; set; }

    public ViewColumn()
    {
    }

    public ViewColumn(string columnColumnId)
    {
        ColumnId = columnColumnId;
    }

    public ViewColumn Create(string columnId)
    {
        return new ViewColumn(columnId);
    }

    // public ViewColumn(Column column)
    // {
    //     Column = column;
    //     Id = column.Id!;
    // }
    //
    // public ViewColumn ShallowClone()
    // {
    //     return new ViewColumn(Column)
    //     {
    //         Width = Width,
    //         Hidden = Hidden
    //     };
    // }
}

public enum ViewType
{
    Grid = 0,
}