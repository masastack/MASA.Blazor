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

    public bool Active { get; set; }
}