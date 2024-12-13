using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

public class ViewColumn
{
    public ViewColumn()
    {
    }

    public ViewColumn(string columnId)
    {
        ColumnId = columnId;
    }

    public string ColumnId { get; init; }

    public double Width { get; set; }

    public bool Hidden { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnFixed Fixed { get; set; }
}

public enum ColumnFixed
{
    None,
    Left,
    Right
}