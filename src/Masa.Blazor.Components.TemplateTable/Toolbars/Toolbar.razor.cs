namespace Masa.Blazor.Components.TemplateTable.Toolbars;

public partial class Toolbar
{
    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public Guid DefaultViewId { get; set; }

    [Parameter] public Guid ActiveView { get; set; }

    [Parameter] public EventCallback<Guid> ActiveViewChanged { get; set; }

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public EventCallback<RowHeight> RowHeightChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public IList<ViewInfo> Views { get; set; } = [];

    [Parameter] public IList<ColumnInfo> Columns { get; set; } = [];

    [Parameter] public EventCallback OnViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnViewReset { get; set; }

    [Parameter] public EventCallback<View> OnViewDelete { get; set; }

    [Parameter] public EventCallback<(Guid id, string Name)> OnViewRename { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback OnFilterClick { get; set; }

    [Parameter] public EventCallback OnSortClick { get; set; }

    [Parameter] public bool HasCustom { get; set; }

    [Parameter] public bool HasFilter { get; set; }

    [Parameter] public bool HasSort { get; set; }

    private bool _configDialog;
    private bool _filterDialog;
    private Guid _renamingView;
    private string? _newViewName;

    private bool IsDefaultView => ActiveView == DefaultViewId;

    private async Task SaveAsNewView()
    {
        var confirmed = await PopupService.ConfirmAsync(
            "Save as new view",
            "Do you want to save the current view as a new view?");

        if (confirmed)
        {
            await OnViewAdd.InvokeAsync();
        }
    }

    private async Task ResetView()
    {
        var confirmed = await PopupService.ConfirmAsync(
            "Reset view",
            "Do you want to reset the current view to the default view?");

        if (confirmed)
        {
            await OnViewReset.InvokeAsync();
        }
    }

    private void ActiveChanged(StringNumber value)
    {
        var viewId = Guid.Parse(value.ToString()!);
        if (viewId == ActiveView)
        {
            return;
        }

        ActiveViewChanged.InvokeAsync(viewId);
    }

    private void RenameView(View view)
    {
        _renamingView = view.Id;
        _newViewName = view.Name;
    }

    private async Task UpdateViewName()
    {
        if (string.IsNullOrWhiteSpace(_newViewName))
        {
            return;
        }

        await OnViewRename.InvokeAsync((_renamingView, _newViewName));

        _renamingView = Guid.Empty;
        _newViewName = null;
    }
}