using Newtonsoft.Json;

namespace Masa.Docs.Indexing.Data;

public record Hierarchy
{
    [JsonProperty("lvl0")]
    public string? Lvl0 { get; set; }

    [JsonProperty("lvl1")]
    public string? Lvl1 { get; set; }

    [JsonProperty("lvl2")]
    public string? Lvl2 { get; set; }

    [JsonProperty("lvl3")]
    public string? Lvl3 { get; set; }

    [JsonProperty("lvl4")]
    public string? Lvl4 { get; set; }

    [JsonProperty("lvl5")]
    public string? Lvl5 { get; set; }

    [JsonProperty("lvl6")]
    public string? Lvl6 { get; set; }

    public override string ToString()
    {
        return $"{Lvl0}-{Lvl1}-{Lvl2}-{Lvl3}-{Lvl4}-{Lvl5}-{Lvl6}";
    }
}