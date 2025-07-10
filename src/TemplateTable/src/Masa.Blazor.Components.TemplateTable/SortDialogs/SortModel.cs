namespace Masa.Blazor.Components.TemplateTable.SortDialogs;

public class SortModel : SortOption
{
    public Column Column { get; init; }

    public SortModel(Column column, int index)
    {
        Column = column;
        ColumnId = column.Id;
        Index = index;

        UpdateOperator();
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
            Index = Index,
            Type = Type
        };
    }

    private void UpdateOperator()
    {
        switch (Column.Type)
        {
            case ColumnType.Text or ColumnType.Link or ColumnType.Email or ColumnType.Phone or ColumnType.Image:
                Type = ExpectedType.String;
                break;
            case ColumnType.Number or ColumnType.Rating or ColumnType.Progress:
                Type = ExpectedType.Float;
                break;
            case ColumnType.Checkbox:
                Type = ExpectedType.Boolean;
                break;
            case ColumnType.Select:
                Type = ExpectedType.String;
                break;
            case ColumnType.Date:
                Type = ExpectedType.DateTime;
                break;
        }
    }
}