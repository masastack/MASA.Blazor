using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class Filter
{
    public string? Search { get; set; }

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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StandardFilter Func { get; set; }

    public string Expected { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ExpectedType Type { get; set; }

    public bool Persistent { get; set; }
}

public enum ExpectedType
{
    String,
    Float,
    DateTime,
    Boolean,
    Expression
}