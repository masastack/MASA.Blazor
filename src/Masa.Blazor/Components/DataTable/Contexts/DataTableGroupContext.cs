namespace Masa.Blazor;

public record DataTableGroupContext<TItem>(
    string Group,
    DataOptions Options,
    bool IsMobile,
    List<TItem> Items,
    List<DataTableHeader<TItem>> Headers);
