namespace Masa.Blazor;

public record DataTableItemExpandOrSelectContext<TItem>(
    TItem Item,
    int Index,
    EventCallback<bool> Expand,
    EventCallback<bool> Select,
    bool IsExpanded,
    bool IsSelected,
    List<DataTableHeader<TItem>> Headers,
    EventCallback<MouseEventArgs> OnClick,
    EventCallback<MouseEventArgs> OnDblClick,
    EventCallback<MouseEventArgs> OnContextmenu
);
