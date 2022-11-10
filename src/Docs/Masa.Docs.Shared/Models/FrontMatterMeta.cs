using YamlDotNet.Serialization;

namespace Masa.Docs.Shared;

public class FrontMatterMeta
{
    [YamlMember(Alias = "title")]
    public string Title { get; set; }

    [YamlMember(Alias = "desc")]
    public string Description { get; set; }

    [YamlMember(Alias = "tag")]
    public string Tag { get; set; }

    [YamlMember(Alias = "related")]
    public string[] Related { get; set; }
}
