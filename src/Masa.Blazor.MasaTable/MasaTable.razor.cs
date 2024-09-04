using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class MasaTable<TItem>
{
    [Parameter] public Sheet? Sheet { get; set; }

    [Parameter] public Func<ValueTask<Sheet>>? SheetProvider { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public IEnumerable<TItem>? Items { get; set; }

    [Parameter] public Func<TItem, List<ColumnTemplate<TItem, object>>> ColumnTemplate { get; set; }

    // ReSharper disable once StaticMemberInGenericType
    private static string[] _modes = ["view", "edit"];

    private string? _selectedMode = "view";
    private Sheet? _internalSheet;
    private List<ColumnTemplate<TItem, object>> _activeViewColumns;

    private List<string> _columnOrder = [];

    private View? ActiveView => _internalSheet?.Views.FirstOrDefault(v => v.Id == _internalSheet.ActiveViewId);

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (Sheet is not null && SheetProvider is not null)
        {
            throw new InvalidOperationException(
                $"{nameof(MasaTable)} requires one of {nameof(Sheet)} or {nameof(SheetProvider)}, but both were provided.");
        }

        _internalSheet = await ResolveSheetRequestAsync();

        ResolveColumns();
    }

    private async ValueTask<Sheet> ResolveSheetRequestAsync()
    {
        if (SheetProvider is not null)
        {
            return await SheetProvider();
        }

        if (Sheet is not null)
        {
            return Sheet;
        }

        return Sheet.CreateDefault();
    }

    private void ResolveColumns()
    {
        if (_internalSheet is null)
        {
            return;
        }

        var firstItem = Items.FirstOrDefault();
        var firstColumnTemplate = ColumnTemplate.Invoke(firstItem);

        if (_internalSheet.Columns.Count == 0)
        {
            foreach (var columnTemplate in firstColumnTemplate)
            {
                columnTemplate.Column.Name = columnTemplate.Column.Type.ToString();
            }

            _activeViewColumns = firstColumnTemplate;
            _internalSheet.Columns = firstColumnTemplate.Select(u => u.Column).ToList();
            _internalSheet.ActiveView.Columns = firstColumnTemplate.Select(u => u.ViewColumn).ToList();
        }
        else
        {
            // var columnIds = _internalSheet.ActiveView.Columns.Where(u => u.Hidden == false).Select(u => u.Id).ToList();
            foreach (var template in firstColumnTemplate)
            {
                template.Column = _internalSheet.Columns.FirstOrDefault(u => u.Id == template.Column.Id) ??
                                  template.Column;
                // template.ViewColumn = _internalSheet.ActiveView.Columns.FirstOrDefault(u => u.Id == template.Column.Id) ?? template.ViewColumn;
            }

            _activeViewColumns = firstColumnTemplate;
        }

        _rowHeight = _internalSheet.ActiveView.RowHeight;
        _hiddenColumnIds =
            new HashSet<string>(_internalSheet.ActiveView.Columns.Where(u => u.Hidden).Select(u => u.ColumnId));
    }

    private async Task Release()
    {
        await OnSave.InvokeAsync(_internalSheet);
    }
}