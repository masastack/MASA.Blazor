namespace Masa.Blazor.Presets;

public record PathPattern
{
    public PathPattern(string path)
    {
        Path = path;
        Pattern = path;
    }

    public PathPattern(string pattern, string path)
    {
        Pattern = pattern;
        Path = path;
        Self = true;
    }

    public string Path { get; private set; }

    public string Pattern { get; }

    public bool Self { get; }

    public void UpdatePath(string path)
    {
        Path = path;
    }

    public virtual bool Equals(PathPattern other)
    {
        return Pattern == other?.Pattern && Self == other?.Self;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Self, Pattern);
    }
}
