namespace Masa.Blazor.Components.TemplateTable;

public class ViewInfo
{
    public View Value { get; set; }

    public List<ViewColumnInfo> Columns { get; set; }

    public List<string> Order { get; set; } = [];

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

        var copyAllColumns = allColumns.ToList();

        foreach (var viewColumn in view.Columns)
        {
            var column = copyAllColumns.FirstOrDefault(c => c.Id == viewColumn.ColumnId);
            if (column is not null)
            {
                viewColumnInfos.Add(ViewColumnInfo.From(viewColumn, column));
                copyAllColumns.Remove(column);
            }
        }

        if (view.ShowSelect)
        {
            viewColumnInfos.Insert(0, Preset.CreateSelectViewColumn());
        }

        // Add the rest columns.
        viewColumnInfos.AddRange(copyAllColumns.Select(c => ViewColumnInfo.From(c.Id, true, c)));

        if (view.ShowActions)
        {
            viewColumnInfos.Add(Preset.CreateActionsViewColumn());
        }

        return new ViewInfo()
        {
            Value = view,
            Columns = viewColumnInfos,
            Order = viewColumnInfos.Select(u => u.ColumnId).ToList(),
            AccessRole = accessRole,
        };
    }

    public View ToView()
    {
        Value.Columns = Columns.Select(c => c as ViewColumn).ToList();
        Value.Columns.RemoveAll(c => c.ColumnId == Preset.ActionsColumnId || c.ColumnId == Preset.RowSelectColumnId);
        Value.Columns.Sort((a, b) =>
        {
            var aIndex = Order.IndexOf(a.ColumnId);
            var bIndex = Order.IndexOf(b.ColumnId);
            return aIndex.CompareTo(bIndex);
        });
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