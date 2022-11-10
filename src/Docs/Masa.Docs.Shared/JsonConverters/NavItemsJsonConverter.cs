using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Docs.Shared.JsonConverters;

public class NavItemsJsonConverter : JsonConverter<List<NavItem>>
{
    public override List<NavItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ReadSubItems(ref reader);
    }

    public override void Write(Utf8JsonWriter writer, List<NavItem> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    private List<NavItem> ReadSubItems(ref Utf8JsonReader reader)
    {
        var list = new List<NavItem>();

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return list;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var title = reader.GetString();
                list.Add(new NavItem(title));
            }

            var navItem = new NavItem();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                list.Add(navItem);
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var propertyName = reader.GetString();
                        reader.Read();
                        switch (propertyName)
                        {
                            case "title":
                                navItem.Title = reader.GetString();
                                break;
                            case "icon":
                                navItem.Icon = reader.GetString();
                                break;
                            case "group":
                                navItem.Group = reader.GetString();
                                break;
                            case "items":
                                navItem.Children = ReadSubItems(ref reader);
                                break;
                        }
                    }
                    else if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        list.Add(navItem);
                        break;
                    }
                }
            }
        }

        return list;
    }
}
