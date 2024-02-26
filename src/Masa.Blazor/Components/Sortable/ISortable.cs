namespace Masa.Blazor;

public interface ISortable
{
    ValueTask UpdateOrder(IEnumerable<string> order);

    ValueTask HandleOnAdd(string key);

    ValueTask HandleOnRemove(string key);
}