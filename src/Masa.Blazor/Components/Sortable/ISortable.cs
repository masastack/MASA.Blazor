namespace Masa.Blazor;

public interface ISortable
{
    ValueTask UpdateOrder(List<string> order);

    ValueTask HandleOnAdd(string key, List<string> order);

    ValueTask HandleOnRemove(string key, List<string> order);
}