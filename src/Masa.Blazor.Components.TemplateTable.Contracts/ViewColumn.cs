namespace Masa.Blazor.Components.TemplateTable;

public class ViewColumn
{
    public required string ColumnId { get; init; }

    public double Width { get; set; }

    public bool Hidden { get; set; }
}