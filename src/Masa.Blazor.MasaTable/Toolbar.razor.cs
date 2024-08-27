using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class Toolbar
{
    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public Guid ActiveView { get; set; }

    [Parameter] public EventCallback<Guid> ActiveViewChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public EventCallback<HashSet<string>> HiddenColumnIdsChanged { get; set; }

    [Parameter] public IList<View> Views { get; set; } = [];

    [Parameter] public IList<Column> Columns { get; set; } = [];

    [Parameter] public EventCallback OnViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnViewDelete { get; set; }

    [Parameter] public EventCallback<(Guid id, string Name)> OnViewRename { get; set; }

    [Parameter] public EventCallback OnRelease { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    private bool _configDialog;

    private void ActiveChanged(StringNumber value)
    {
        var guid = Guid.Parse(value.ToString()!);
        ActiveViewChanged.InvokeAsync(guid);
    }

    private void RenameView(View view)
    {
        // TODO: Implement rename view
    }
}