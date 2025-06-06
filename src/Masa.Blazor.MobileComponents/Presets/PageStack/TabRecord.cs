using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public record TabRecord(TabRule Rule, string AbsolutePath)
{
    internal int Id { get; init; }

    internal DateTime CreatedAt { get; } = DateTime.Now;

    public virtual bool Equals(TabRecord? other)
    {
        return other?.Rule == Rule;
    }

    public override int GetHashCode()
    {
        return Rule.GetHashCode();
    }
}