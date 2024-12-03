using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

public class View
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ViewType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RowHeight RowHeight { get; set; }

    public List<ViewColumn> Columns { get; set; } = [];

    public bool HasActions { get; set; } = true;

    public bool ShowSelect { get; set; } = true;

    public Filter? Filter { get; set; }

    public Sort? Sort { get; set; }
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