namespace Masa.Blazor.Components.TemplateTable;

public class ViewInfo : View
{
    public ViewInfo()
    {
    }

    public ViewInfo(View view)
    {
        Id = view.Id;
        Name = view.Name;
        Columns = view.Columns;
        Type = view.Type;
        RowHeight = view.RowHeight;
        HasActions = view.HasActions;
        Filter = view.Filter;
        Sort = view.Sort;
    }

    public ICollection<IReadOnlyDictionary<string, JsonElement>>? Rows { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    internal static ViewInfo From(View view)
    {
        return new ViewInfo(view);
    }
}