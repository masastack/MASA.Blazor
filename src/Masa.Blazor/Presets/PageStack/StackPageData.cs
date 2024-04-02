namespace Masa.Blazor.Presets;

public class StackPageData
{
    public StackPageData(string absolutePath)
    {
        Active = true;
        AbsolutePath = absolutePath;
        Pattern = absolutePath.ToLower();
    }

    public StackPageData(string pattern, string absolutePath)
    {
        Active = true;
        AbsolutePath = absolutePath;
        IsSelf = true;
        Pattern = pattern.ToLower();
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public string AbsolutePath { get; private set; }

    public string Pattern { get; init; }

    public bool IsSelf { get; init; }

    public bool Active { get; set; }

    public string Selector { get; set; }
    
    public void UpdatePath(string absolutePath) => AbsolutePath = absolutePath;
}