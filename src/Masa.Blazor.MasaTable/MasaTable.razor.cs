using System.Text.Json;
using BemIt;
using Masa.Blazor.MasaTable.ColumnConfigs;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.MasaTable;

public partial class MasaTable<TItem>
{
    [Parameter] public Sheet? Sheet { get; set; }

    [Parameter] public Func<ValueTask<Sheet>>? SheetProvider { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public IEnumerable<TItem>? Items { get; set; }

    [Parameter] public Func<TItem, List<ColumnTemplate<TItem, object>>> ColumnTemplate { get; set; }

    [Parameter] public EventCallback<TItem> OnUpdate { get; set; }

    [Parameter] public EventCallback<TItem> OnDelete { get; set; }

    [Parameter] public EventCallback<TItem> OnAction1 { get; set; }

    [Parameter] public EventCallback<TItem> OnAction2 { get; set; }

    // ReSharper disable once StaticMemberInGenericType
    private static string[] _modes = ["view", "edit"];
    
    private static Block _block = new Block("m-gen-table");

    private string? _selectedMode = "edit";
    private Sheet? _internalSheet;
    private bool _defaultSheetFlag = false;

    private List<ColumnTemplate<TItem, object>> _activeViewTemplateColumns = [];
    private List<string> _columnOrder = [];
    private HashSet<string> _hiddenColumnIds = [];

    private View? ActiveView => _internalSheet?.Views.FirstOrDefault(v => v.Id == _internalSheet.ActiveViewId);

    private bool IsEditMode => _selectedMode == "edit";

    private bool HasActions => OnUpdate.HasDelegate || OnDelete.HasDelegate ||
                               OnAction1.HasDelegate || OnAction2.HasDelegate;

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (Sheet is not null && SheetProvider is not null)
        {
            throw new InvalidOperationException(
                $"{nameof(MasaTable)} requires one of {nameof(Sheet)} or {nameof(SheetProvider)}, but both were provided.");
        }

        var defaultSheet = Sheet is null && SheetProvider is null;
        if (_defaultSheetFlag != defaultSheet)
        {
            _defaultSheetFlag = defaultSheet;

            if (_defaultSheetFlag)
            {
                _internalSheet = Sheet.CreateDefault();
            }
            else
            {
                _internalSheet = (SheetProvider is not null) ? await SheetProvider() : Sheet;
            }

            ResolveColumns();
        }
    }

    private void ResolveColumns()
    {
        if (_internalSheet is null)
        {
            return;
        }

        _columnOrder.Clear();

        var firstItem = Items.FirstOrDefault();
        _activeViewTemplateColumns = ColumnTemplate.Invoke(firstItem);

        if (_internalSheet.Columns.Count == 0)
        {
            foreach (var columnTemplate in _activeViewTemplateColumns)
            {
                columnTemplate.Column.Name = columnTemplate.Column.Id;
            }

            if (HasActions)
            {
                var actionsTemplate = new ColumnTemplate<TItem, object>(Preset.ActionsColumnId, ColumnType.Actions);
                actionsTemplate.Column.Name = "Actions";
                _activeViewTemplateColumns.Add(actionsTemplate);
            }

            _internalSheet.Columns = _activeViewTemplateColumns.Select(u => u.Column).ToList();
            _internalSheet.ActiveView.Columns = _activeViewTemplateColumns.Select(u => u.ViewColumn).ToList();
        }
        else
        {
            AlignColumns(firstTime: true);
        }

        UpdateState();
    }

    private void UpdateState()
    {
        _rowHeight = _internalSheet.ActiveView.RowHeight;
        _hiddenColumnIds.Clear();
        _columnOrder.Clear();
        foreach (var viewColumn in _internalSheet.ActiveView.Columns)
        {
            if (viewColumn.Hidden)
            {
                _hiddenColumnIds.Add(viewColumn.ColumnId);
            }

            _columnOrder.Add(viewColumn.ColumnId);
        }
    }

    private void AlignColumns(bool firstTime = false)
    {
        var templateColumnIds = _activeViewTemplateColumns.Select(u => u.Column.Id).ToList();
        var sheetColumnIds = _internalSheet.Columns.Select(u => u.Id).ToList();

        // columns in the template but not in the sheet
        var addColumns = templateColumnIds.Except(sheetColumnIds).ToList();
        if (addColumns.Count > 0)
        {
            foreach (var columnTemplate in _activeViewTemplateColumns)
            {
                if (addColumns.Contains(columnTemplate.Column.Id))
                {
                    columnTemplate.Column.Name = columnTemplate.Column.Id;
                    columnTemplate.ViewColumn.Hidden = true;
                    if (firstTime)
                    {
                        _internalSheet.Columns.Add(columnTemplate.Column);
                    }

                    _internalSheet.ActiveView.Columns.Add(columnTemplate.ViewColumn);
                }
            }
        }

        // columns in the sheet but not in the template
        var removeColumns = sheetColumnIds.Except(templateColumnIds).ToList();
        if (removeColumns.Count > 0)
        {
            foreach (var columnId in removeColumns)
            {
                if (columnId == Preset.ActionsColumnId)
                {
                    continue;
                }

                if (firstTime)
                {
                    var column = _internalSheet.Columns.FirstOrDefault(u => u.Id == columnId);
                    if (column is not null)
                    {
                        _internalSheet.Columns.Remove(column);
                    }
                }

                var viewColumn = _internalSheet.ActiveView.Columns.FirstOrDefault(u => u.ColumnId == columnId);
                if (viewColumn is not null)
                {
                    _internalSheet.ActiveView.Columns.Remove(viewColumn);
                }
            }
        }

        if (firstTime)
        {
            // if actions column is not in the sheet but required
            if (HasActions)
            {
                if (_activeViewTemplateColumns.All(u => u.Column.Id != Preset.ActionsColumnId))
                {
                    var actionsTemplate = new ColumnTemplate<TItem, object>(Preset.ActionsColumnId, ColumnType.Actions);
                    actionsTemplate.Column.Name = "Actions";
                    _activeViewTemplateColumns.Add(actionsTemplate);
                }
            }

            foreach (var columnTemplate in _activeViewTemplateColumns)
            {
                var inputColumn = _internalSheet.Columns.FirstOrDefault(u => u.Id == columnTemplate.Column.Id);
                if (inputColumn is not null)
                {
                    columnTemplate.Column.Type = inputColumn.Type;
                    columnTemplate.Column.Name = inputColumn.Name;
                    columnTemplate.Column.Config = inputColumn.Config;
                }
            }
        }
    }

    private async Task Release()
    {
        await OnSave.InvokeAsync(_internalSheet);
    }
}