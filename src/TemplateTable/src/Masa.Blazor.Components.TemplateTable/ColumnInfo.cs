namespace Masa.Blazor.Components.TemplateTable;

public class ColumnInfo : Column
{
    public ColumnInfo()
    {
    }
    
    public ColumnInfo(Column column)
    {
        Id = column.Id;
        Name = column.Name;
        Type = column.Type;
        Config = column.Config;
        Searchable = column.Searchable;
        ConfigObject = UpdateConfigObject(column.Type, column.Config);
    }

    internal object? ConfigObject { get; set; }

    private static object? UpdateConfigObject(ColumnType type, string? config)
    {
        if (string.IsNullOrWhiteSpace(config))
        {
            return null;
        }

        return type switch
        {
            ColumnType.Date => JsonSerializer.Deserialize<DateConfig>(config),
            ColumnType.Select => JsonSerializer.Deserialize<SelectConfig>(config),
            ColumnType.Number => JsonSerializer.Deserialize<NumberConfig>(config),
            ColumnType.Progress => JsonSerializer.Deserialize<ProgressConfig>(config),
            ColumnType.Link => JsonSerializer.Deserialize<LinkConfig>(config),
            ColumnType.Email => null,
            ColumnType.Image => null,
            ColumnType.Phone => null,
            ColumnType.Checkbox => null,
            ColumnType.Rating => null,
            ColumnType.Text => null,
            _ => throw new NotSupportedException($"Column type '{type}' is not supported.")
        };
    }
}