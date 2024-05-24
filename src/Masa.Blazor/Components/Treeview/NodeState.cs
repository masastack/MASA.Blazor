namespace Masa.Blazor.Components.Treeview;

public class NodeState<TItem, TKey>
{
    public NodeState(TItem item, IEnumerable<TKey> children, TKey? parent = default)
    {
        Item = item;
        Children = children;
        Parent = parent;
    }

    public TItem Item { get; }

    public IEnumerable<TKey> Children { get; }

    public TKey? Parent { get; }

    public MTreeviewNode<TItem, TKey>? Node { get; set; }

    public bool IsActive { get; set; }

    public bool IsSelected { get; set; }

    public bool IsIndeterminate { get; set; }

    public bool IsOpen { get; set; }
}