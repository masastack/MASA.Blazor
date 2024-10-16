namespace Masa.Blazor.Components.TemplateTable;

public class ViewInfo
{
    public View? Value { get; set; }

    public List<ViewColumnInfo> Columns { get; set; }

    public ICollection<IReadOnlyDictionary<string, JsonElement>>? Rows { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public static ViewInfo From(View view, IEnumerable<ColumnInfo> columns)
    {
        return new ViewInfo()
        {
            Value = view,
            Columns = view.Columns.Select(vc =>
            {
                var c = columns.FirstOrDefault(c => c.Id == vc.ColumnId);
                if (c is null)
                {
                    throw new InvalidOperationException($"Column {vc.ColumnId} not found.");
                }

                return ViewColumnInfo.From(vc, c);
            }).ToList()
        };
    }
}

// public class ViewInfo : View
// {
//     public ViewInfo()
//     {
//     }
//
//     public ViewInfo(View view)
//     {
//         Id = view.Id;
//         Name = view.Name;
//         Columns = view.Columns;
//         Type = view.Type;
//         RowHeight = view.RowHeight;
//         HasActions = view.HasActions;
//         Filter = view.Filter;
//         Sort = view.Sort;
//     }
//
//     public ICollection<IReadOnlyDictionary<string, JsonElement>>? Rows { get; set; }
//
//     public int PageIndex { get; set; }
//
//     public int PageSize { get; set; }
//
//     public bool HasPreviousPage { get; set; }
//
//     public bool HasNextPage { get; set; }
//
//     internal static ViewInfo From(View view)
//     {
//         return new ViewInfo(view);
//     }
// }