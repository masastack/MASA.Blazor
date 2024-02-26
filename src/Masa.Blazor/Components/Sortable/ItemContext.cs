namespace Masa.Blazor;

public record SortableItemContext<TItem>(TItem Item, Dictionary<string, object?> Attrs);