namespace Masa.Blazor.Presets;

public record PatternPath
{
    public PatternPath(string absolutePath)
    {
        AbsolutePath = absolutePath;
        Pattern = absolutePath;
    }

    public PatternPath(string pattern, string absolutePath)
    {
        AbsolutePath = absolutePath;
        Pattern = pattern;
        IsSelf = true;
    }

    public string AbsolutePath { get; private set; }

    /// <summary>
    /// The regular expression pattern to match.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Determine whether if the path is a self path.
    /// </summary>
    public bool IsSelf { get; }

    public void UpdatePath(string path)
    {
        AbsolutePath = path;
    }

    public virtual bool Equals(PatternPath other)
    {
        return Pattern == other?.Pattern && IsSelf == other?.IsSelf;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsSelf, Pattern);
    }

    public void Deconstruct(out string Pattern, out string AbsolutePath)
    {
        Pattern = this.Pattern;
        AbsolutePath = this.AbsolutePath;
    }
}
