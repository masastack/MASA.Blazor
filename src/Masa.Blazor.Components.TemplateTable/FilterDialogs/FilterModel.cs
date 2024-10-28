namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public class FilterModel : FilterOption
{
    public ColumnInfo Column { get; private set; }

    public List<SelectOption>? SelectOptions { get; private set; }

    // TODO: 初始化
    public List<string> MultiSelect { get; internal set; } = [];

    public string[] FuncList { get; private set; }

    public FilterModel(ColumnInfo column)
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
        SelectOptions = null;

        switch (Column.Type)
        {
            case ColumnType.Text or ColumnType.Link or ColumnType.Email or ColumnType.Phone:
                FuncList = FilterTypes.SupportedStringFilters;
                Func = FuncList[0];
                Type = ExpectedType.String;
                break;
            case ColumnType.Number or ColumnType.Rating or ColumnType.Progress or ColumnType.Date:
                FuncList = FilterTypes.SupportedComparableFilters;
                Func = FuncList[0];
                Type = Column.Type is ColumnType.Date ? ExpectedType.String : ExpectedType.Number;
                break;
            case ColumnType.Checkbox:
                FuncList = FilterTypes.SupportedBooleanFilters;
                Func = FuncList[0];
                Type = ExpectedType.Boolean;
                break;
            case ColumnType.Select:
                FuncList = FilterTypes.SupportedSelectFilters;
                Func = FuncList[0];
                Type = ExpectedType.String;
                SelectOptions = (Column.ConfigObject as SelectConfig)?.Options;
                break;
            case ColumnType.MultiSelect:
                FuncList = FilterTypes.SupportedMultiSelectFilters;
                Func = FuncList[0];
                Type = ExpectedType.String;
                SelectOptions = (Column.ConfigObject as SelectConfig)?.Options;
                break;
            case ColumnType.Image:
                FuncList = FilterTypes.EmptyFilters;
                Func = FuncList[0];
                Type = ExpectedType.String;
                break;
        }
    }
}