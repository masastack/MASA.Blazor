using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class Column
{
    public Column()
    {
    }

    public Column(string id, string name, ColumnType type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    public Column(string id, string name, ColumnType type, object? configObject) : this(id, name, type)
    {
        Config = System.Text.Json.JsonSerializer.Serialize(configObject);
    }

    /// <summary>
    /// The unique identifier for the column.
    /// Same as the property name in the data model.
    /// </summary>
    public string Id { get; init; }

    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnType Type { get; set; }

    public string? Config { get; set; }

    public bool Searchable { get; set; }

    public bool IsNested => Id.Contains('.');

    /// <summary>
    /// Get the expected data type based on the column type.
    /// For filtering and sorting purposes.
    /// </summary>
    /// <returns></returns>
    public ExpectedType GetExpectedType()
    {
        return Type switch
        {
            ColumnType.Number or ColumnType.Progress or ColumnType.Rating => ExpectedType.Float,
            ColumnType.Date => ExpectedType.DateTime,
            ColumnType.Switch or ColumnType.Checkbox => ExpectedType.Boolean,
            _ => ExpectedType.String,
        };
    }
}