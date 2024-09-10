using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class Toolbar
{
    [Parameter] public string? Mode { get; set; }
    
    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public Guid ActiveView { get; set; }

    [Parameter] public EventCallback<Guid> ActiveViewChanged { get; set; }

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public EventCallback<RowHeight> RowHeightChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public IList<View> Views { get; set; } = [];

    [Parameter] public IList<Column> Columns { get; set; } = [];

    [Parameter] public EventCallback OnViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnViewDelete { get; set; }

    [Parameter] public EventCallback<(Guid id, string Name)> OnViewRename { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    private bool _configDialog;
    private Guid _renamingView;
    private string? _newViewName;
    
    private bool IsEditMode => Mode == "edit";

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