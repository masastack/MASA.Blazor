namespace Masa.Blazor.Components.TemplateTable.Actions;

public class ColumnsContext
{
    public ColumnInfo Column { get; }

    public JsonElement? Value { get; }

    public IReadOnlyDictionary<string, JsonElement> RowValue { get; }

    public ColumnsContext(ColumnInfo column, IReadOnlyDictionary<string, JsonElement> rowValue, JsonElement? value, Func<ColumnsContext, RenderFragment> @default)
    {
        Column = column;
        RowValue = rowValue;
        Value = value;
        Default = @default.Invoke(this);
    }

    public RenderFragment Default { get; }
}