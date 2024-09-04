using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class Viewer<TItem>
{
    [Parameter] public List<ColumnTemplate<TItem, object>> ColumnTemplates { get; set; } = [];

    [Parameter] public IList<string> ColumnOrder { get; set; } = [];

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public RowHeight RowHeight { get; set; }
    
    [Parameter] public IEnumerable<TItem> Rows { get; set; } = [];

    [Parameter] public EventCallback<string> OnColumnHide { get; set; }

    // TODO: column delete
    [Parameter] public EventCallback<string> OnColumnDelete { get; set; }

    private IEnumerable<ColumnTemplate<TItem, object>> ComputedColumnTemplates
    {
        get
        {
            return ColumnTemplates.Where(c => !HiddenColumnIds.Contains(c.Column.Id))
                .OrderBy(u => ColumnOrder.IndexOf(u.Column.Id));
        }
    }

    private void HandleOnColumnHide(string columnId)
    {
        _ = OnColumnHide.InvokeAsync(columnId);
    }

    private void HandleOnColumnDelete(string columnId)
    {
        _ = OnColumnDelete.InvokeAsync(columnId);
    }
}