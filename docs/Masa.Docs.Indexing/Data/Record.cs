// Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
using System.Text.Json.Serialization;
namespace Masa.Docs.Indexing.Data;
public class Record
{
    public Record(string lang, string docUrl, string rootName, RecordType type, int level, int position, int pageRank = 0)
    {
        Url = docUrl;
        UrlWithoutAnchor = docUrl;
        UrlWithoutVariables = docUrl;
        Lang = lang;
        Language = lang;
        Hierarchy = new()
        {
            Lvl0 = rootName
        };
        Tags = new();
        Type = type.ToString().ToLowerInvariant();
        Weight = new Weight
        {
            Level = level,
            PageRank = pageRank.ToString(),
            Position = position,
        };
        this.RecordVersion = Data.RecordVersion.V3.ToString().ToLower();//now it support v3 only
    }

    [JsonPropertyName("objectID")]
    public string? ObjectID
    {
        get
        {
            return Weight.Position + "-" + Lang + "-" + UrlWithoutAnchor;
        }
    }

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public List<object> Tags { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("url_without_variables")]
    public string? UrlWithoutVariables { get; set; }

    [JsonPropertyName("url_without_anchor")]
    public string? UrlWithoutAnchor { get; set; }

    [JsonPropertyName("anchor")]
    public string? Anchor { get; set; } = null;

    [JsonPropertyName("content")]
    public string? Content { get; set; } = null;

    [JsonPropertyName("content_camel")]
    public string? ContentCamel { get; set; } = null;

    [JsonPropertyName("lang")]
    public string Lang { get; private set; }

    [JsonPropertyName("language")]
    public string Language { get; private set; }

    [JsonPropertyName("type")]
    public string Type { get; private set; }

    [JsonPropertyName("no_variables")]
    public bool NoVariables { get; set; } = false;

    [JsonPropertyName("weight")]
    public Weight Weight { get; private set; }

    [JsonPropertyName("hierarchy")]
    public Hierarchy Hierarchy { get; private set; }

    public void ClonePrerecord(Record fromRecord)
    {
        Hierarchy = fromRecord.Hierarchy;
        Anchor = fromRecord.Anchor;
        UrlWithoutVariables = fromRecord.UrlWithoutVariables;
        Url = fromRecord.Url;
    }

    [JsonPropertyName("recordVersion")]
    public string RecordVersion { get; private set; }
}

