namespace Masa.Blazor;

public record DataTableGroupHeaderContext<TItem>(
    string Group,
    IList<string> GroupBy,
    List<TItem> Items,
    List<DataTableHeader<TItem>> Headers,
    bool IsOpen,
    EventCallback Toggle,
    EventCallback Remove);
