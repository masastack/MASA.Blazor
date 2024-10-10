using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

// TODO: rename this
public class SheetManager
{
    /// <summary>
    /// All columns without state.
    /// </summary>
    public List<Column> Columns { get; set; } = [];

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
    public List<View> Views { get; set; } = [];
    
    [JsonIgnore] internal List<ViewColumn> ActiveViewColumns => ActiveView?.Columns ?? [];

    [JsonIgnore] internal RowHeight ActiveViewRowHeight => ActiveView?.RowHeight ?? RowHeight.Low;

    [JsonIgnore] internal bool ActiveViewHasActions => ActiveView?.HasActions ?? false;

    internal View? ActiveView => Views.FirstOrDefault(v => v.Id == ActiveViewId);

    internal static SheetManager From(Sheet sheet)
    {
        return new SheetManager
        {
            Columns = sheet.Columns,
            Views = sheet.Views,
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
}