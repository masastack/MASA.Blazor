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
            
            // Add the Actions column if it doesn't exist for rendering action column. 
            var view = Views.FirstOrDefault(v => v.Value.Id == value);
            if (view is not null && view.Columns.All(c => c.ColumnId != Preset.ActionsColumnId))
            {
                view.Columns.Add(Preset.CreateActionsViewColumn());
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

    [JsonIgnore] internal List<ViewColumnInfo> ActiveViewColumns => ActiveView?.Columns ?? [];

    [JsonIgnore]
    internal HashSet<string> ActiveViewHiddenColumnIds
        => ActiveViewColumns.Where(c => c.Hidden).Select(c => c.ColumnId).ToHashSet();

    [JsonIgnore] internal RowHeight ActiveViewRowHeight => ActiveView?.Value.RowHeight ?? RowHeight.Low;

    [JsonIgnore] internal bool ActiveViewHasActions => ActiveView?.Value.HasActions ?? false;

    internal ViewInfo? ActiveView => Views.FirstOrDefault(v => v.Value.Id == ActiveViewId);

    internal static SheetInfo From(Sheet sheet)
    {
        var columnInfos = sheet.Columns.Select(c => new ColumnInfo(c)).ToList();

        return new SheetInfo
        {
            Columns = columnInfos,
            Views = sheet.Views.Select(v => ViewInfo.From(v, columnInfos)).ToList(),
            Pagination = sheet.Pagination,
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

        ActiveView.Value.RowHeight = rowHeight;
    }

    internal void UpdateActiveViewItems(ICollection<IReadOnlyDictionary<string, JsonElement>> items,
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