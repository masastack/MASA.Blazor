namespace Masa.Blazor.Components.TemplateTable;

public class Sheet
{
    /// <summary>
    /// All columns without state.
    /// </summary>
    public IEnumerable<Column> Columns { get; set; } = [];
    
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
    public IEnumerable<View> Views { get; set; } = [];
}