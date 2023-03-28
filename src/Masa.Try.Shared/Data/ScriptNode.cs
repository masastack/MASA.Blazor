namespace Masa.Try.Shared
{
    public class ScriptNode
    {
        public ScriptNode(string content, ScriptNodeType scriptNodeType)
        {
            Id = Guid.NewGuid().ToString();
            NodeType = scriptNodeType;
            Content = content;
        }

        public string Id { get; init; }

        public ScriptNodeType NodeType { get; init; }

        public string Content { get; set; }
    }

    public enum ScriptNodeType
    {
        Js = 0,
        Css = 1,
    }
}