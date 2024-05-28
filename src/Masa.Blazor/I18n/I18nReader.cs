using System.Text;
using System.Text.Json;

namespace BlazorComponent.I18n;

public static class I18nReader
{
    static JsonReaderOptions _jsonReaderOptions = new JsonReaderOptions()
    {
        CommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public static Dictionary<string, string> Read(string jsonData, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var reader = new Utf8JsonReader(encoding.GetBytes(jsonData), _jsonReaderOptions);
        var map = new Dictionary<string, string>();
        BuildMap(ref reader, map);
        return map;
    }

    static void BuildMap(ref Utf8JsonReader reader, Dictionary<string, string> map, string? baseKey = null)
    {
        while (reader.Read())
        {
            if (reader.TokenType is JsonTokenType.StartArray) reader.Skip();
            else if (reader.TokenType is JsonTokenType.EndObject) break;
            else if (reader.TokenType is JsonTokenType.PropertyName)
            {
                var key = reader.GetString()!;
                var newkey = baseKey is null ? key : $"{baseKey}.{key}";
                reader.Read();
                if (reader.TokenType is JsonTokenType.StartArray) reader.Skip();
                else if (reader.TokenType is JsonTokenType.StartObject) BuildMap(ref reader, map, newkey);
                else map[newkey] = ReadString(reader);
            }
        }
    }

    static string ReadString(Utf8JsonReader reader)
    {
        return reader.TokenType switch
        {
            JsonTokenType.False or JsonTokenType.True => reader.GetBoolean().ToString(),
            JsonTokenType.Number => reader.GetInt32().ToString(),
            JsonTokenType.String => reader.GetString() ?? "",
            _ => "",
        };
    }
}

