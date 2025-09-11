namespace Masa.Blazor.Components.TemplateTable.Contracts;

public static class TemplateTableEnumTypeConvert
{
    public static ExpectedType ConvertToExpectedType(this ColumnType columnType)
    {
        return columnType switch
        {
            ColumnType.Date => ExpectedType.DateTime,
            ColumnType.Progress => ExpectedType.Float,
            ColumnType.Number => ExpectedType.Float,
            ColumnType.Rating => ExpectedType.Float,
            ColumnType.Switch => ExpectedType.Boolean,
            ColumnType.Checkbox => ExpectedType.Boolean,
            ColumnType.RowSelect => ExpectedType.Boolean,
            _ => ExpectedType.String,
        };
    }
}
