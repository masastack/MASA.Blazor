using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

// TODO: rename this
internal class SheetInfo
{
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
    public Guid ActiveViewId { get; private set; }

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
    public required string ItemKeyName { get; set; }

    public required string QueryBody { get; set; }

    public required string CountField { get; set; }

    [JsonIgnore] internal List<ViewColumnInfo> ActiveViewColumns => ActiveView.Columns;

    [JsonIgnore]
    internal HashSet<string> ActiveViewHiddenColumnIds
        => ActiveViewColumns.Where(c => c.Hidden).Select(c => c.ColumnId).ToHashSet();

    [JsonIgnore] internal RowHeight ActiveViewRowHeight => ActiveView?.Value.RowHeight ?? RowHeight.Low;

    public required ViewInfo ActiveView { get; set; }

    internal static SheetInfo From(Sheet sheet)
    {
        if (sheet.Views.Count == 0)
        {
            throw new InvalidOperationException("The sheet must have at least one view.");
        }

        if (string.IsNullOrWhiteSpace(sheet.ItemKeyName))
        {
            throw new InvalidOperationException("The 'ItemKeyName' is required.");
        }

        var columnInfos = sheet.Columns.Select(c => new ColumnInfo(c)).ToList();
        var views = sheet.Views.Select(v => ViewInfo.From(v, columnInfos, Role.Manager)).ToList();

        var defaultView = views.FirstOrDefault(v => v.Value.Id == sheet.DefaultViewId);
        if (defaultView is null)
        {
            throw new InvalidOperationException($"The default view with id {sheet.DefaultViewId} is not found.");
        }

        defaultView.IsDefaultView = true;

        var activeView = views.FirstOrDefault(v => v.Value.Id == sheet.ActiveViewId) ?? defaultView;

        return new SheetInfo
        {
            QueryBody = sheet.QueryBody,
            CountField = sheet.CountField,
            Columns = columnInfos,
            Views = views,
            Pagination = sheet.Pagination,
            DefaultViewId = sheet.DefaultViewId,
            ItemKeyName = sheet.ItemKeyName,
            ActiveView = activeView,
            ActiveViewId = activeView.Value.Id
        };
    }

    internal void SetActiveView(Guid viewId)
    {
        var view = Views.First(v => v.Value.Id == viewId);

        #region Add the Actions column if it doesn't exist.

        var columnIds = view.Columns.Select(c => c.ColumnId).ToArray();
        if (!columnIds.Contains(Preset.ActionsColumnId))
        {
            view.Columns.Add(Preset.CreateActionsViewColumn());
        }

        if (!columnIds.Contains(Preset.RowSelectColumnId))
        {
            view.Columns.Insert(0, Preset.CreateSelectViewColumn());
        }

        #endregion

        ActiveViewId = viewId;
        ActiveView = view;
    }

    internal void UpdateActiveViewRowHeight(RowHeight rowHeight)
    {
        ActiveView.Value.RowHeight = rowHeight;
    }

    internal void UpdateActiveViewItems(ICollection<Row> items)
    {
        ActiveView.Rows = items;
    }
}