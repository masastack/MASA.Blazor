using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

// TODO: rename this
public class SheetInfo
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
    public Guid ActiveViewId { get; set; }

    /// <summary>
    /// All views of the sheet.
    /// </summary>
    public List<ViewInfo> Views { get; set; } = [];

    [JsonIgnore] internal List<ViewColumn> ActiveViewColumns => ActiveView?.Columns ?? [];

    [JsonIgnore] internal RowHeight ActiveViewRowHeight => ActiveView?.RowHeight ?? RowHeight.Low;

    [JsonIgnore] internal bool ActiveViewHasActions => ActiveView?.HasActions ?? false;

    internal ViewInfo? ActiveView => Views.FirstOrDefault(v => v.Id == ActiveViewId);

    internal static SheetInfo From(Sheet sheet)
    {
        return new SheetInfo
        {
            Columns = sheet.Columns.Select(c => new ColumnInfo(c)).ToList(),
            Views = sheet.Views.Select(v => new ViewInfo(v)).ToList(),
            ActiveViewId = sheet.ActiveViewId,
            DefaultViewId = sheet.DefaultViewId
        };
    }

    internal void UpdateActiveViewRowHeight(RowHeight rowHeight)
    {
        if (ActiveView is null)
        {
            return;
        }

        ActiveView.RowHeight = rowHeight;
    }

    internal void UpdateActiveViewHasActions(bool hasActions)
    {
        if (ActiveView is null)
        {
            return;
        }

        ActiveView.HasActions = hasActions;
    }

    internal void UpdateActiveViewItems(ICollection<IReadOnlyDictionary<string, JsonElement>> items, bool hasPreviousPage, bool hasNextPage)
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