using Newtonsoft.Json;

namespace Masa.Docs.Indexing.Data;

public record Weight
{
    [JsonProperty("pageRank")]
    public string PageRank { get; set; } = "0";

    [JsonProperty("level")]
    public int? Level { get; set; }

    [JsonProperty("position")]
    public int? Position { get; set; }

    public override string ToString()
    {
        return $"PageRank:{PageRank};Level：{Level}; Position: {Position}";
    }
}