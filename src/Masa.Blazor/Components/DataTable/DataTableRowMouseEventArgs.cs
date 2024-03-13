namespace Masa.Blazor;

public class DataTableRowMouseEventArgs<TItem> : EventArgs
{
    public TItem Item { get; init; }

    public bool IsMobile { get; init; }

    public bool IsSelected { get; init; }

    public bool IsExpanded { get; init; }

    public MouseEventArgs MouseEventArgs { get; init; }

    public DataTableRowMouseEventArgs(TItem item, bool isMobile, bool isSelected, bool isExpanded, MouseEventArgs mouseEventArgs)
    {
        Item = item;
        IsMobile = isMobile;
        IsExpanded = isExpanded;
        IsSelected = isSelected;
        MouseEventArgs = mouseEventArgs;
    }
}
