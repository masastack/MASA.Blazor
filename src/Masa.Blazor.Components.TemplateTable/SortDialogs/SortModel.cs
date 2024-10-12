namespace Masa.Blazor.Components.TemplateTable.SortDialogs;

public class SortModel : SortOption
{
    public Column Column { get; init; }

    public SortModel(Column column, int index)
    {
        Column = column;
        ColumnId = column.Id;
        Index = index;
    }

    internal static SortModel From(SortOption option, Column column)
    {
        return new SortModel(column, option.Index)
        {
            OrderBy = option.OrderBy
        };
    }

    public SortOption ToSortOption()
    {
        return new SortOption
        {
            ColumnId = ColumnId,
            OrderBy = OrderBy,
            Index = Index
        };
    }
}