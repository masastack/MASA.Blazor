namespace Masa.Blazor.MasaTable;

public class Filter
{
    public string ColumnId { get; set; }

    public string Func { get; set; }

    public string? Expected { get; set; }

    public Column Column { get; private set; }

    public string[] FuncList { get; private set; }

    public Filter()
    {
    }

    public Filter(Column column)
    {
        Column = column;
        ColumnId = column.Id;

        UpdateOperator();
    }

    public void OnSelect((Column Column, bool Selected) item)
    {
        Column = item.Column;
        ColumnId = item.Column.Id;

        UpdateOperator();
    }

    private void UpdateOperator()
    {
        if (Column.Type is ColumnType.Text or ColumnType.Link or ColumnType.Email or ColumnType.Phone)
        {
            FuncList =
            [
                "StartsWith",
                "NotStartsWith",
                "EndsWith",
                "NotEndsWith",
                "Contains",
                "NotContains",
                "Empty",
                "NotEmpty"
            ];

            Func = FuncList[0];
        }
        else if (Column.Type is ColumnType.Number or ColumnType.Rating or ColumnType.Progress)
        {
            FuncList =
            [
                "Equal",
                "NotEqual",
                "GreaterThan",
                "GreaterThanOrEqual",
                "LessThan",
                "LessThanOrEqual",
                "Empty",
                "NotEmpty"
            ];
            Func = FuncList[0];
        }
        else if (Column.Type == ColumnType.Date)
        {
            FuncList =
            [
                "Specific",
                "Today",
                "Tomorrow",
                "Yesterday",
                "ThisWeek",
                "NextWeek",
                "LastWeek",
                "ThisMonth",
                "NextMonth",
                "LastMonth",
                "Last7Days",
                "Next7Days",
                "Last30Days",
                "Next30Days",
            ];
            Func = FuncList[0];
        }
        else if (Column.Type == ColumnType.Checkbox)
        {
            FuncList = ["True", "False"];
            Func = FuncList[0];
        }
        else if (Column.Type == ColumnType.Select)
        {
            FuncList = ["Equal", "NotEqual", "Empty", "NotEmpty"];
            Func = FuncList[0];
        }
        else if (Column.Type == ColumnType.MultiSelect)
        {
            FuncList = ["Equal", "NotEqual", "ContainsAny", "ContainsAll", "NotContains", "Empty", "NotEmpty"];
            Func = FuncList[0];
        }
        else if (Column.Type == ColumnType.Image)
        {
            FuncList = ["Empty", "NotEmpty"];
            Func = FuncList[0];
        }
    }
}