using System.Text.Json.Serialization;

public class Hierarchy
{
    [JsonPropertyName("lvl0")]
    public string? Lvl0 { get; set; }

    [JsonPropertyName("lvl1")]
    public string? Lvl1 { get; set; }

    [JsonPropertyName("lvl2")]
    public string? Lvl2 { get; set; }

    [JsonPropertyName("lvl3")]
    public string? Lvl3 { get; set; }

    [JsonPropertyName("lvl4")]
    public string? Lvl4 { get; set; }

    [JsonPropertyName("lvl5")]
    public string? Lvl5 { get; set; }

    [JsonPropertyName("lvl6")]
    public string? Lvl6 { get; set; }
}

