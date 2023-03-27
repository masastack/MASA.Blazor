using System.Text.Json.Serialization;

public class Weight
{
    [JsonPropertyName("pageRank")]
    public string PageRank { get; set; } = "0";

    [JsonPropertyName("level")]
    public int? Level { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

    public override string ToString()
    {
        return $"PageRank:{PageRank};Level：{Level};Postion:{Position}";
    }
}

