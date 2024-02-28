namespace Masa.Blazor;

public record SortableItemContext<TItem>(TItem Item, IDictionary<string, object?> Attrs);