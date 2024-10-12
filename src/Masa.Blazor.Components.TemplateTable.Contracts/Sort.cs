using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

public class Sort
{
    public List<SortOption> Options { get; set; } = [];

    public Sort()
    {
    }

    public Sort(List<SortOption> options)
    {
        Options = options;
    }
}

public class SortOption
{
    public SortOption()
    {
    }

    public string ColumnId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortOrder OrderBy { get; set; }

    public int Index { get; set; }
}

public enum SortOrder
{
    Asc,
    Desc
}