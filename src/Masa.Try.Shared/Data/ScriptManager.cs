namespace Masa.Try.Shared
{
    internal class ScriptManager
    {
        public List<Module> Modules { get; } = new List<Module>()
        {
            new()
            {
                ModuleName= "BaiduMap",
                RelatedScripts = new()
                {
                    new(ScriptNodeType.JS, "https://api.map.baidu.com/getscript?v=1.0&&type=webgl&ak=bgALNYvfp7HFsQKE1TX2RGuH0UN0ENC4")
                }
            }
        };
    }

    internal struct Module
    {
        public string ModuleName { get; set; }

        public List<ScriptNode> RelatedScripts { get; set; }

        public bool Loaded { get; set; }
    }

    public struct ScriptNode
    {
        public ScriptNode(ScriptNodeType scriptNodeType, string content)
        {
            Id = Guid.NewGuid().ToString();
            NodeType = scriptNodeType;
            Content = content;
        }

        public string Id { get; set; }

        public ScriptNodeType NodeType { get; set; }

        public string Content { get; set; }
    }

    public enum ScriptNodeType
    {
        JS = 0,
        CSS = 1,
    }
}
