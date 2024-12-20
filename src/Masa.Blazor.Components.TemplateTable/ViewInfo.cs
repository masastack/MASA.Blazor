namespace Masa.Blazor.Components.TemplateTable;

public class ViewInfo
{
    public View Value { get; set; }

    public List<ViewColumnInfo> Columns { get; set; }

    public ICollection<Row>? Rows { get; set; }

    public Dictionary<Row, bool> Selection { get; set; } = [];

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public bool IsDefaultView { get; internal set; }

    public Role AccessRole { get; set; }

    public bool IsUserView => AccessRole == Role.User;

    public ViewState State { get; set; }

    public static ViewInfo From(View view, IEnumerable<ColumnInfo> allColumns, Role accessRole)
    {
        List<ViewColumnInfo> viewColumnInfos = [];

        foreach (var column in allColumns)
        {
            var viewColumn = view.Columns.FirstOrDefault(vc => vc.ColumnId == column.Id);
            if (viewColumn is null)
            {
                viewColumnInfos.Add(new ViewColumnInfo()
                {
                    Column = column,
                    ColumnId = column.Id,
                    Hidden = true
                });
            }
            else
            {
                viewColumnInfos.Add(ViewColumnInfo.From(viewColumn, column));
            }
        }

        return new ViewInfo()
        {
            Value = view,
            Columns = viewColumnInfos,
            AccessRole = accessRole,
        };
    }

    public View ToView()
    {
        Value.Columns = Columns.Select(c => c as ViewColumn).ToList();
        Value.Columns.RemoveAll(c => c.ColumnId == Preset.ActionsColumnId || c.ColumnId == Preset.RowSelectColumnId);
        return Value;
    }

    public List<ViewColumnInfo> GetSearchableColumn()
    {
        return Columns.Where(c => c.Column.Searchable).ToList();
    }
}

public enum ViewState
{
    /// <summary>
    /// Have saved to the server and not modified.
    /// </summary>
    Unmodified,
    /// <summary>
    /// Have not saved to the server.
    /// </summary>
    Unsaved,
    /// <summary>
    /// Have saved to the server and modified.
    /// </summary>
    Modified,
}