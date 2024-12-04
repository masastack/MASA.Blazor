namespace Masa.Blazor.Components.DataTable;

public record SelectionKey<TItem>(string Key, TItem? Item)
{
    public virtual bool Equals(SelectionKey<TItem>? other)
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