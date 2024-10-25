namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public class FilterModel : FilterOption
{
    public Column Column { get; private set; }

    public string[] FuncList { get; private set; }

    public FilterModel(Column column)
    {
        Column = column;
        ColumnId = column.Id;

        UpdateOperator();
    }

    public void OnSelect((ColumnInfo Column, bool Selected) item)
    {
        Column = item.Column;
        ColumnId = item.Column.Id;
        UpdateOperator();
    }

    public FilterOption ToFilterOption()
    {
        return new FilterOption
        {
            ColumnId = ColumnId,
            Func = Func,
            Expected = Expected,
            Type = Type
        };
    }

    private void UpdateOperator()
    {
        if (Column.Type is ColumnType.Text or ColumnType.Link or ColumnType.Email or ColumnType.Phone)
        {
            FuncList = FilterTypes.SupportedStringFilters;
            Func = FuncList[0];
            Type = ExpectedType.String;
        }
        else if (Column.Type is ColumnType.Number or ColumnType.Rating or ColumnType.Progress or ColumnType.Date)
        {
            FuncList = FilterTypes.SupportedComparableFilters;
            Func = FuncList[0];
            Type = Column.Type is ColumnType.Date ? ExpectedType.String : ExpectedType.Number;
        }
        else if (Column.Type == ColumnType.Checkbox)
        {
            FuncList = FilterTypes.SupportedBooleanFilters;
            Func = FuncList[0];
            Type = ExpectedType.Boolean;
        }
        else if (Column.Type == ColumnType.Select)
        {
            FuncList = ["Equal", "NotEqual", "Empty", "NotEmpty"];
            Func = FuncList[0];
            Type = ExpectedType.String;
        }
        else if (Column.Type == ColumnType.MultiSelect)
        {
            FuncList = ["Equal", "NotEqual", "ContainsAny", "ContainsAll", "NotContains", "Empty", "NotEmpty"];
            Func = FuncList[0];
            Type = ExpectedType.String;
        }
        else if (Column.Type == ColumnType.Image)
        {
            FuncList = ["Empty", "NotEmpty"];
            Func = FuncList[0];
            Type = ExpectedType.String;
        }
    }
}