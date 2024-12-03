using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

// TODO: rename this
public class SheetInfo
{
    private Guid _activeViewId;

    /// <summary>
    /// All columns without state.
    /// </summary>
    public List<ColumnInfo> Columns { get; set; } = [];

    /// <summary>
    /// The identifier of the default view, which is the first view and cannot be deleted.
    /// </summary>
    public Guid DefaultViewId { get; set; }

    /// <summary>
    /// The identifier of the active view.
    /// </summary>
    public Guid ActiveViewId
    {
        get => _activeViewId;
        set
        {
            _activeViewId = value;

            // Add the Actions column if it doesn't exist. 
            var view = Views.FirstOrDefault(v => v.Value.Id == value);
            if (view is not null)
            {
                var columnIds = view.Columns.Select(c => c.ColumnId).ToArray();
                if (!columnIds.Contains(Preset.ActionsColumnId))
                {
                    view.Columns.Add(Preset.CreateActionsViewColumn());
                }

                if (!columnIds.Contains(Preset.RowSelectColumnId))
                {
                    view.Columns.Insert(0, Preset.CreateSelectViewColumn());
                }
            }
        }
    }

    /// <summary>
    /// All views of the sheet.
    /// </summary>
    public List<ViewInfo> Views { get; set; } = [];

    /// <summary>
    /// The options for pagination.
    /// </summary>
    public Pagination Pagination { get; set; } = new();

    /// <summary>
    /// The identifier of the row item.
    /// </summary>
    public string? ItemKeyName { get; set; }

    [JsonIgnore] internal List<ViewColumnInfo> ActiveViewColumns => ActiveView?.Columns ?? [];

    [JsonIgnore]
    internal HashSet<string> ActiveViewHiddenColumnIds
        => ActiveViewColumns.Where(c => c.Hidden).Select(c => c.ColumnId).ToHashSet();

    [JsonIgnore] internal RowHeight ActiveViewRowHeight => ActiveView?.Value.RowHeight ?? RowHeight.Low;

    [JsonIgnore] internal bool ActiveViewHasActions => ActiveView?.Value.HasActions ?? false;

    [JsonIgnore] internal bool ActiveViewShowSelect => ActiveView?.Value.ShowSelect ?? false;

    internal ViewInfo ActiveView { get; private set; } = default!;

    internal static SheetInfo From(Sheet sheet)
    {
        if (sheet.Views.Count == 0)
        {
            throw new InvalidOperationException("The sheet must have at least one view.");
        }

        var columnInfos = sheet.Columns.Select(c => new ColumnInfo(c)).ToList();
        var views = sheet.Views.Select(v => ViewInfo.From(v, columnInfos)).ToList();
        var activeView = views.FirstOrDefault(v => v.Value.Id == sheet.ActiveViewId)
                         ?? views.FirstOrDefault(v => v.Value.Id == sheet.DefaultViewId)
                         ?? views.First();

        var sheetInfo = new SheetInfo
        {
            Columns = columnInfos,
            Views = views,
            Pagination = sheet.Pagination,
            ActiveViewId = sheet.ActiveViewId,
            DefaultViewId = sheet.DefaultViewId,
            ItemKeyName = sheet.ItemKeyName
        };

        sheetInfo.ActiveViewId = activeView.Value.Id;
        sheetInfo.ActiveView = activeView;

        return sheetInfo;
    }

    internal void SetActiveView(Guid viewId)
    {
        var view = Views.First(v => v.Value.Id == viewId);
        ActiveViewId = viewId;
        ActiveView = view;
    }

    internal void UpdateActiveViewRowHeight(RowHeight rowHeight)
    {
        if (ActiveView is null)
        {
            return;
        }

        ActiveView.Value.RowHeight = rowHeight;
    }

    internal void UpdateActiveViewItems(ICollection<Row> items,
        bool hasPreviousPage, bool hasNextPage)
    {
        if (ActiveView is null)
        {
            return;
        }

        ActiveView.Rows = items;
        ActiveView.HasPreviousPage = hasPreviousPage;
        ActiveView.HasNextPage = hasNextPage;
    }
}