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
    public string ColumnId { get; set; }

    public SortDirection Direction { get; set; }
    
    public int Index { get; set; }
}

public enum SortDirection
{
    Asc,
    Desc
}