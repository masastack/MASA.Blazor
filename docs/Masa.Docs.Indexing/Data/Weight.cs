using System.Text.Json.Serialization;

namespace Masa.Docs.Indexing.Data;

public record Weight
{
    [JsonPropertyName("pageRank")]
    public string PageRank { get; set; } = "0";

    [JsonPropertyName("level")]
    public int? Level { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

    public override string ToString()
    {
        return $"PageRank:{PageRank};Level：{Level}; Position: {Position}";
    }
}