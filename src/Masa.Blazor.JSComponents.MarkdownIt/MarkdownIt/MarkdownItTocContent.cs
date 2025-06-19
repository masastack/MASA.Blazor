namespace Masa.Blazor;

public class MarkdownItTocContent
{
    public string? Content { get; set; }

    public string? Anchor { get; set; }

    public int Level { get; set; }

    public Dictionary<string, object?> Attrs { get; set; } = new();
}
