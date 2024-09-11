using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class Viewer<TItem>
{
    [Parameter] public List<ColumnTemplate<TItem, object>> ColumnTemplates { get; set; } = [];

    [Parameter] public IList<string> ColumnOrder { get; set; } = [];

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public IEnumerable<TItem> Rows { get; set; } = [];

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<TItem> OnUpdate { get; set; }

    [Parameter] public EventCallback<TItem> OnDelete { get; set; }

    [Parameter] public EventCallback<TItem> OnAction1 { get; set; }

    [Parameter] public EventCallback<TItem> OnAction2 { get; set; }

    private bool _imageViewer;
    private IList<string> _imagesToView = [];

    private bool HasActions => OnUpdate.HasDelegate || OnDelete.HasDelegate ||
                               OnAction1.HasDelegate || OnAction2.HasDelegate;

    private IEnumerable<ColumnTemplate<TItem, object>> ComputedColumnTemplates
    {
        get
        {
            return ColumnTemplates.Where(c => !HiddenColumnIds.Contains(c.Column.Id))
                .OrderBy(u => ColumnOrder.IndexOf(u.Column.Id));
        }
    }

    private void OpenImageViewer(IList<string> images)
    {
        _imageViewer = true;
        _imagesToView = images;
    }

    private void OnImageViewerChanged()
    {
        if (_imageViewer == false)
        {
            _imagesToView = [];
        }
    }
}