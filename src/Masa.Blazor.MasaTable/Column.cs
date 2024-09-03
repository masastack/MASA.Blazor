using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Masa.Blazor.MasaTable.ColumnConfigs;

namespace Masa.Blazor.MasaTable;

public class Column
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ColumnType Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateCacheConfig();
        }
    }

    [JsonInclude]
    internal string? Config
    {
        get => _config;
        set
        {
            _config = value;
            UpdateCacheConfig();
        }
    }

    private ColumnType _type;
    private string? _config;

    internal object? ConfigObject { get; private set; }

    private void UpdateCacheConfig()
    {
        if (string.IsNullOrWhiteSpace(Config))
        {
            ConfigObject = null;
            return;
        }

        ConfigObject = Type switch
        {
            ColumnType.Date => JsonSerializer.Deserialize<DateConfig>(Config),
            ColumnType.MultiSelect => JsonSerializer.Deserialize<MultiSelectValue>(Config),
            _ => null
        };
    }
}