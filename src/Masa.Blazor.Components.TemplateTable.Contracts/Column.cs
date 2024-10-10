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

    public Column(string id, string name, ColumnType type, object? config) : this(id, name, type)
    {
        Config = System.Text.Json.JsonSerializer.Serialize(config);
    }

    public string Id { get; init; }

    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnType Type { get; set; }

    public string? Config { get; set; }

    internal object? ConfigObject { get; set; }
}