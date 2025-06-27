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
    public IList<View> Views { get; set; } = [];

    /// <summary>
    /// The options for pagination.
    /// </summary>
    public Pagination Pagination { get; set; } = new();

    /// <summary>
    /// The identifier of the row item.
    /// </summary>
    public string? ItemKeyName { get; set; }

    /// <summary>
    /// The query body of the GraphQL query.
    /// It should be the minimal query body without any parameters.
    /// </summary>
    /// <example>
    /// users {
    ///   id
    ///   name
    ///   email
    /// }
    /// </example>
    public required string QueryBody { get; set; }
    
    /// <summary>
    /// The field name for counting the total number of items.
    /// </summary>
    public required string CountField { get; set; }

    // TODO: 支持建列自动隐藏（添加新的属性，如HideItemKeyColumn）
}

public class Pagination
{
    public List<int> PageSizeOptions { get; set; } = [5, 10, 20, 50];
}