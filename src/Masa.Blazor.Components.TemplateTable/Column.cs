namespace Masa.Blazor.Components.TemplateTable;

public static class ColumnExtensions
{
    internal static void UpdateConfigObject(this Column column)
    {
        if (string.IsNullOrWhiteSpace(column.Config))
        {
            column.ConfigObject = null;
            return;
        }

        column.ConfigObject = column.Type switch
        {
            ColumnType.Date => JsonSerializer.Deserialize<DateConfig>(column.Config),
            ColumnType.MultiSelect or ColumnType.Select => JsonSerializer.Deserialize<SelectConfig>(column.Config),
            ColumnType.Number => JsonSerializer.Deserialize<NumberConfig>(column.Config),
            ColumnType.Progress => JsonSerializer.Deserialize<ProgressConfig>(column.Config),
            ColumnType.Email => null,
            ColumnType.Image => null,
            ColumnType.Link => null,
            ColumnType.Phone => null,
            ColumnType.Checkbox => null,
            ColumnType.Rating => null,
            ColumnType.Text => null,
            ColumnType.Actions => JsonSerializer.Deserialize<ActionsConfig>(column.Config),
            _ => throw new NotSupportedException($"Column type '{column.Type}' is not supported.")
        };
    }
}