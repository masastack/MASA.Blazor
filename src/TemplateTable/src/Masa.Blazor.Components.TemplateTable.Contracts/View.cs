using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class View
{
    public View(string name, List<ViewColumn> viewColumns)
    {
        Name = name;
        Columns = viewColumns;
    }

    public View()
    {
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ViewType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RowHeight RowHeight { get; set; }

    public List<ViewColumn> Columns { get; set; } = [];

    public bool ShowActions { get; set; } = true;

    public bool ShowSelect { get; set; } = true;

    public bool ShowBulkDelete { get; set; } = true;

    public Filter Filter { get; set; } = new();

    public Sort Sort { get; set; } = new();
}

public enum RowHeight
{
    Low,
    Medium,
    High
}

public enum ViewType
{
    Grid = 0,
}