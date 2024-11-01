using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable;

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

    public string Id { get; init; }

    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnType Type { get; set; }

    public string? Config { get; set; }

    public bool Searchable { get; set; }
}