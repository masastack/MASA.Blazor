namespace Masa.Blazor.Components.TemplateTable;

public record Row(string Key, IReadOnlyDictionary<string, JsonElement> Data)
{
    public virtual bool Equals(Row? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Key == other.Key;
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }
}
