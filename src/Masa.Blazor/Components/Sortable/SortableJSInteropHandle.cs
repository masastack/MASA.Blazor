namespace Masa.Blazor;

public class SortableJSInteropHandle
{
    private readonly ISortable _sortable;

    public SortableJSInteropHandle(ISortable sortable)
    {
        _sortable = sortable;
    }

    [JSInvokable]
    public ValueTask UpdateOrder(List<string> order)
        => _sortable.UpdateOrder(order);

    [JSInvokable]
    public ValueTask HandleOnAdd(string key, List<string> order)
        => _sortable.HandleOnAdd(key, order);

    [JSInvokable]
    public ValueTask HandleOnRemove(string key, List<string> order)
        => _sortable.HandleOnRemove(key, order);
}