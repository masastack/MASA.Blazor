namespace Masa.Blazor;

public interface ISortable
{
    ValueTask UpdateOrder(IEnumerable<string> order);
}