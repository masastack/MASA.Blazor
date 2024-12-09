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

    [Parameter] public EventCallback OnSearch { get; set; }

    [Parameter] public EventCallback OnSave { get; set; }

    [Parameter] public EventCallback OnRowRemove { get; set; }

    [Parameter] public bool HasSelectedKeys { get; set; }

    [Parameter] public bool HasActions { get; set; }

    [Parameter] public bool HasSelect { get; set; }

    [Parameter] public bool HasCustom { get; set; }

    [Parameter] public bool HasFilter { get; set; }

    [Parameter] public bool HasSort { get; set; }

    [Parameter] public bool ShowDetail { get; set; }

    [Parameter] public EventCallback<bool> ShowDetailChanged { get; set; }

    [Parameter] public bool ShowBulkDelete { get; set; }

    [Parameter] public EventCallback<bool> ShowBulkDeleteChanged { get; set; }

    [Parameter] public List<int> PageSizeOptions { get; set; } = [];

    [Parameter] public EventCallback<List<int>> PageSizeOptionsChanged { get; set; }

    [Parameter] public RenderFragment? ViewActionsContent { get; set; }

    private bool _configDialog;
    private bool _filterDialog;
    private string? _newViewName;
    private bool _renameMenu;

    private ViewInfo? _activeViewInfo;

    private List<int> _itemsPerPage = [5, 10, 20, 50];
    private IList<int> _itemsPerPageOptions = [5, 10, 20, 50];

    private bool IsDefaultView => ActiveView == DefaultViewId;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _activeViewInfo ??= Views.FirstOrDefault(v => v.Value.Id == ActiveView);
    }

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

    private bool _removing;

    private async Task HandleOnRowRemove()
    {
        if (OnRowRemove.HasDelegate)
        {
            _removing = true;
            StateHasChanged();

            try
            {
                await OnRowRemove.InvokeAsync();
            }
            finally
            {
                _removing = false;
            }
        }
    }

    private void OnActiveViewChanged(ViewInfo viewInfo)
    {
        _activeViewInfo = viewInfo;
    }

    private void RenameView()
    {
        _newViewName = _activeViewInfo?.Value.Name;
    }

    private async Task UpdateViewName()
    {
        if (string.IsNullOrWhiteSpace(_newViewName))
        {
            return;
        }

        await OnViewRename.InvokeAsync((ActiveView, _newViewName));

        _newViewName = null;
        _renameMenu = false;
    }

    private Task HandleOnDelete()
    {
        return OnViewDelete.InvokeAsync(_activeViewInfo!.Value);
    }

    private List<string> _messages = [];

    private void HandleOnPageSizeOptionsChange(List<int> val)
    {
        if (val.Count == 0)
        {
            _messages.Add("At least one option is required.");
            return;
        }

        _messages.Clear();

        PageSizeOptionsChanged.InvokeAsync(val);
    }
}