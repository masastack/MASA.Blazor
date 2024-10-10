using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

public class ViewColumn
{
    public required string ColumnId { get; init; }

    public double Width { get; set; }

    public bool Hidden { get; set; }

    [JsonIgnore]
    internal Column Column { get; set; }

    internal void AttachColumn(Column column)
    {
        Column = column;
    }
}