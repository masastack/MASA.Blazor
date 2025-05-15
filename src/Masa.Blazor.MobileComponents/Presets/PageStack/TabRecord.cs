using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

internal record TabRecord(TabRule Rule, string AbsolutePath)
{
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