namespace Masa.Blazor;

public class SortableJSInteropHandle
{
    private readonly ISortable _sortable;

    public SortableJSInteropHandle(ISortable sortable)
    {
        _sortable = sortable;
    }

    [JSInvokable]
    public ValueTask UpdateOrder(IEnumerable<string> order)
        => _sortable.UpdateOrder(order);
}