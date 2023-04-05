using Newtonsoft.Json;

namespace Masa.Docs.Indexing.Data;

public class Record
{
    [JsonProperty("objectID")]
    public string ObjectId => Weight!.Position + "-" + Lang + "-" + UrlWithoutAnchor;

    [JsonProperty("version")]
    public string Version { get; set; } = string.Empty;

    [JsonProperty("tags")]
    public List<object> Tags { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("url_without_variables")]
    public string? UrlWithoutVariables { get; set; }

    [JsonProperty("url_without_anchor")]
    public string? UrlWithoutAnchor { get; set; }

    [JsonProperty("anchor")]
    public string? Anchor { get; set; } = null;

    [JsonProperty("content")]
    public string? Content { get; set; } = null;

    [JsonProperty("content_camel")]
    public string? ContentCamel { get; set; } = null;

    [JsonProperty("lang")]
    public string Lang { get; }

    [JsonProperty("language")]
    public string Language { get; }

    [JsonProperty("type")]
    public string? Type { get; private set; }

    [JsonProperty("no_variables")]
    public bool NoVariables { get; set; } = true;

    [JsonProperty("weight")]
    public Weight? Weight { get; }

    [JsonProperty("hierarchy")]
    public Hierarchy Hierarchy { get; private set; }

    [JsonProperty("recordVersion")]
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
