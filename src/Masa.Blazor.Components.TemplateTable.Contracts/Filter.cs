using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

public class Filter
{
    public List<FilterOption> Options { get; set; } = [];

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterOperator Operator { get; set; } = FilterOperator.And;
}

public enum FilterOperator
{
    And,
    Or
}

public class FilterOption
{
    public string ColumnId { get; set; }

    public string Func { get; set; }

    public string Expected { get; set; } = string.Empty;
}