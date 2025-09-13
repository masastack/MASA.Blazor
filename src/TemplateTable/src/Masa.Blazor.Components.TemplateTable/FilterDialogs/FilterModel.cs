namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public class FilterModel : FilterOption
{
    public ColumnInfo Column { get; private set; }

    public List<SelectOption>? SelectOptions { get; private set; }
    
    public List<string> MultiSelect { get; internal set; } = [];

    public StandardFilter[] FuncList { get; private set; }

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
            Type = Type,
            Persistent = Persistent
        };
    }

    private void UpdateOperator()
    {
        SelectOptions = null;

        switch (Column.Type)
        {
            case ColumnType.Text or ColumnType.Link or ColumnType.Email or ColumnType.Phone or ColumnType.Image:
                FuncList = SupportedFilter.SupportedStringFilters;
                Func = FuncList[0];
                Type = ExpectedType.String;
                break;
            case ColumnType.Number or ColumnType.Rating or ColumnType.Progress:
                FuncList = SupportedFilter.SupportedNumberFilters;
                Func = FuncList[0];
                Type = ExpectedType.Float;
                break;
            case ColumnType.Checkbox:
                FuncList = SupportedFilter.SupportedBooleanFilters;
                Func = FuncList[0];
                Type = ExpectedType.Boolean;
                break;
            case ColumnType.Select:
                FuncList = SupportedFilter.SupportedSelectFilters;
                Func = FuncList[0];
                Type = Func == StandardFilter.Contains || Func == StandardFilter.NotContains ? ExpectedType.Expression : ExpectedType.String;
                SelectOptions = (Column.ConfigObject as SelectConfig)?.Options;
                break;
            case ColumnType.Date:
                FuncList = SupportedFilter.SupportedDateTimeFilters;
                Func = FuncList[0];
                Type = ExpectedType.DateTime;
                break;
        }
    }
}