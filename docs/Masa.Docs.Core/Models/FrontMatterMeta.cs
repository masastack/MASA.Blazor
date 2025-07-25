using YamlDotNet.Serialization;

namespace Masa.Docs.Core.Models;

public class FrontMatterMeta
{
    [YamlMember(Alias = "title")]
    public string Title { get; set; } = null!;

    [YamlMember(Alias = "desc")]
    public string Description { get; set; } = null!;

    [YamlMember(Alias = "tag")]
    public string? Tag { get; set; }

    [YamlMember(Alias = "related")]
    public string[]? Related { get; set; }

    [YamlMember(Alias = "release")]
    public string? Release { get; set; }
}