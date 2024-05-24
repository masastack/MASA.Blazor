namespace Masa.Blazor;

public class TreeviewItem<TItem>
{
    public TreeviewItem(TItem item, bool leaf, bool selected, bool indeterminate, bool active, bool open)
    {
        Item = item;
        Leaf = leaf;
        Selected = selected;
        Indeterminate = indeterminate;
        Active = active;
        Open = open;
    }

    public TItem Item { get; }

    public bool Leaf { get; }

    public bool Selected { get; }

    public bool Indeterminate { get; }

    public bool Active { get; }

    public bool Open { get; }
}
