namespace Masa.Blazor.Presets;

public class StackPatternPath : PatternPath
{
    public StackPatternPath(string absolutePath) : base(absolutePath)
    {
        Active = true;
    }

    public StackPatternPath(string pattern, string absolutePath) : base(pattern, absolutePath)
    {
        Active = true;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public bool Active { get; set; }

    public string Selector { get; set; }
}