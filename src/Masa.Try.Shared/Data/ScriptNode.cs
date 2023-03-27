using System.Text.Json.Serialization;

namespace Masa.Try.Shared
{
    public class ScriptNode
    {
        public ScriptNode(string scriptName, ScriptNodeType scriptNodeType, string content)
        {
            Id = Guid.NewGuid().ToString();
            ScriptName = scriptName;
            NodeType = scriptNodeType;
            Content = content;
        }

        public string Id { get; init; }

        [JsonIgnore]
        public string ScriptName { get; set; }

        public ScriptNodeType NodeType { get; set; }

        public string Content { get; set; }
    }

    public enum ScriptNodeType
    {
        JS = 0,
        CSS = 1,
    }
}