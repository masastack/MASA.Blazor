using System.Text.Json.Serialization;
namespace Masa.Docs.Indexing.Data;

public class Record
{
    [JsonPropertyName("objectID")]
    public string ObjectId => Weight!.Position + "-" + Lang + "-" + UrlWithoutAnchor;

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
    public string Lang { get; }

    [JsonPropertyName("language")]
    public string Language { get; }

    [JsonPropertyName("type")]
    public string? Type { get; private set; }

    [JsonPropertyName("no_variables")]
    public bool NoVariables { get; set; } = true;

    [JsonPropertyName("weight")]
    public Weight? Weight { get; }

    [JsonPropertyName("hierarchy")]
    public Hierarchy Hierarchy { get; private set; }

    [JsonPropertyName("recordVersion")]
    public string RecordVersion { get; }

    public Record(string lang, string? docUrl, string? rootName, RecordType type, int? level, int? position, int pageRank = 0) : this(lang, docUrl, rootName, position, pageRank)
    {
        SetType(type);
        Weight ??= new();
        Weight.Level = level;
    }

    public Record(string lang, string? docUrl, string? rootName, int? position, int pageRank = 0)
    {
        Url = docUrl;
        UrlWithoutAnchor = docUrl;
        UrlWithoutVariables = docUrl;
        Lang = lang;
        Language = lang;
        Hierarchy = new Hierarchy
        {
            Lvl0 = rootName
        };
        Tags = new List<object>();
        Weight ??= new();
        Weight.PageRank = pageRank.ToString();
        Weight.Position = position;
        RecordVersion = Data.RecordVersion.V3.ToString().ToLower();//now it support v3 only
    }

    public void ClonePrerecord(Record fromRecord)
    {
        Hierarchy = fromRecord.Hierarchy with{ };
        Anchor = fromRecord.Anchor;
        UrlWithoutVariables = fromRecord.UrlWithoutVariables;
        Url = fromRecord.Url;
    }

    public void SetType(RecordType type)
    {
        Type = type.ToString().ToLowerInvariant();
    }
}
