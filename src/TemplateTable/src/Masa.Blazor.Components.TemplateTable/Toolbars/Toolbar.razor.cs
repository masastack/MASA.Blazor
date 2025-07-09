﻿using Masa.Blazor.Components.TemplateTable.Actions;

namespace Masa.Blazor.Components.TemplateTable.Toolbars;

public partial class Toolbar
{
    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public Guid DefaultViewId { get; set; }

    [Parameter] public Guid ActiveView { get; set; }

    [Parameter] public Role Role { get; set; }

    [Parameter] public EventCallback<Guid> ActiveViewChanged { get; set; }

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public EventCallback<RowHeight> RowHeightChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public List<ViewInfo> Views { get; set; } = [];

    [Parameter] public IList<ColumnInfo> Columns { get; set; } = [];

    [Parameter] public EventCallback OnViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnViewReset { get; set; }

    [Parameter] public EventCallback<ViewInfo> OnViewDelete { get; set; }

    [Parameter] public EventCallback<(Guid id, string Name)> OnViewRename { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback OnFilterClick { get; set; }

    [Parameter] public EventCallback OnSortClick { get; set; }

    [Parameter] public EventCallback OnSearch { get; set; }

    [Parameter] public EventCallback OnSave { get; set; }

    [Parameter] public EventCallback OnViewSave { get; set; }

    [Parameter] public EventCallback OnRowRemove { get; set; }

    [Parameter] public bool HasSelectedKeys { get; set; }

    [Parameter] public bool HasActions { get; set; }

    [Parameter] public bool HasSelect { get; set; }

    [Parameter] public bool HasCustom { get; set; }

    [Parameter] public bool HasFilter { get; set; }

    [Parameter] public bool HasSort { get; set; }

    [Parameter] public bool Editable { get; set; }

    [Parameter] public bool ShowBulkDelete { get; set; }

    [Parameter] public EventCallback<bool> ShowBulkDeleteChanged { get; set; }

    [Parameter] public List<int> PageSizeOptions { get; set; } = [];

    [Parameter] public EventCallback<List<int>> PageSizeOptionsChanged { get; set; }

    [Parameter] public RenderFragment? ViewActionsContent { get; set; }

    private bool _configDialog;
    private bool _filterDialog;
    private string? _newViewName;
    private bool _renameMenu;
    private bool _viewActionLoading;

    private ViewInfo? _activeViewInfo;

    private List<string> _messages = [];
    private List<int> _itemsPerPage = [5, 10, 20, 50];
    private IList<int> _itemsPerPageOptions = [5, 10, 20, 50];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Views.Sort((a, b) =>
        {
            if (a.IsDefaultView || b.IsDefaultView)
            {
                return 0;
            }

            return a.AccessRole.CompareTo(b.AccessRole);
        });

        if (_activeViewInfo?.Value.Id != ActiveView)
        {
            _activeViewInfo = Views.FirstOrDefault(v => v.Value.Id == ActiveView);
        }
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

    private bool _deleting;

    private async Task HandleOnBulkDelete()
    {
        if (OnRowRemove.HasDelegate)
        {
            _deleting = true;
            StateHasChanged();

            try
            {
                await OnRowRemove.InvokeAsync();
            }
            finally
            {
                _deleting = false;
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

    private async Task UpdateViewName(string newName)
    {
        await OnViewRename.InvokeAsync((ActiveView, newName));
    }

    private async Task HandleOnViewSave()
    {
        _viewActionLoading = true;
        StateHasChanged();

        try
        {
            await OnViewSave.InvokeAsync();
        }
        finally
        {
            _viewActionLoading = false;
        }
    }

    private Task HandleOnDelete()
    {
        return OnViewDelete.InvokeAsync(_activeViewInfo);
    }

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