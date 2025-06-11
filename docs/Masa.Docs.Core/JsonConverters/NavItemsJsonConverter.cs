using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Docs.Core.JsonConverters;

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
                if (title is null) continue;

                list.Add(new NavItem(title));
            }

            var navItem = new NavItem();
            if (reader.TokenType == JsonTokenType.StartObject)
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
                            case "exact":
                                navItem.Exact = reader.GetBoolean();
                                break;
                            case "group":
                                navItem.Group = reader.GetString();
                                break;
                            case "state":
                                navItem.State = reader.GetString();
                                break;
                            case "releasedOn":
                                navItem.ReleasedOn = reader.GetString();
                                break;
                            case "items":
                                navItem.Children = ReadSubItems(ref reader);
                                break;
                            case "type":
                                navItem.ComponentType = reader.GetString() switch
                                {
                                    "containment" => ComponentType.Containment,
                                    "navigation" => ComponentType.Navigation,
                                    "form" => ComponentType.Form,
                                    "data-display" => ComponentType.DataDisplay,
                                    "selection" => ComponentType.Selection,
                                    "feedback" => ComponentType.Feedback,
                                    "images-and-icons" => ComponentType.ImagesAndIcons,
                                    "pickers" => ComponentType.Pickers,
                                    "providers" => ComponentType.Providers,
                                    "services" => ComponentType.Services,
                                    "misc" => ComponentType.Misc,
                                    _ => ComponentType.Unknown
                                };
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
