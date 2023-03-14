namespace Masa.Blazor.Presets;

public record PathPattern(string Pattern, bool Self = false)
{
    public string Path { get; set; }

    public virtual bool Equals(PathPattern other)
    {
        return Pattern == other?.Pattern && Self == other?.Self;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Self, Pattern);
    }
}
