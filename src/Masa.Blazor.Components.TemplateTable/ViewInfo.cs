namespace Masa.Blazor.Components.TemplateTable;

public class ViewInfo
{
    public View Value { get; set; }

    public List<ViewColumnInfo> Columns { get; set; }

    public ICollection<IReadOnlyDictionary<string, JsonElement>>? Rows { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public static ViewInfo From(View view, IEnumerable<ColumnInfo> columns)
    {
        List<ViewColumnInfo> viewColumnInfos = [];

        foreach (var column in columns)
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
            Columns = viewColumnInfos
        };
    }

    public View ToView()
    {
        Value.Columns = Columns.Select(c => c as ViewColumn).ToList();
        return Value;
    }

    public List<ViewColumnInfo> GetSearchableColumn()
    {
        return Columns.Where(c => c.Column.Searchable).ToList();
    }
}