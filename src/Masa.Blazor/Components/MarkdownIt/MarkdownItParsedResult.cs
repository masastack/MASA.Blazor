using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class MarkdownItParsedResult
{
    public string? FrontMatter { get; set; }

    [JsonPropertyName("markup")]
    public string? MarkupContent { get; set; }

    public List<MarkdownItTocContent>? Toc { get; set; }
}
