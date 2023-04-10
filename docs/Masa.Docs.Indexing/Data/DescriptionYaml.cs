namespace Masa.Docs.Indexing.Data;

public class DescriptionYaml
{
    public string? Desc { get; set; }

    public string? Title { get; set; }

    public List<string> Related { get; set; } = new();

    public string? Tag { get; set; }

    public string? Content { get; set; } = null;

    public DateTime? Date { get; set; } = null;
}