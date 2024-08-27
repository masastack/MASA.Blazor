using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Masa.Blazor.MasaTable;

public class Column
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnType Type { get; set; }

    internal string? AdditionalValue { get; set; }
}