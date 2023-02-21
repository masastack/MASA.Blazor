﻿using System.Text.Json;
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
                            case "group":
                                navItem.Group = reader.GetString();
                                break;
                            case "state":
                                var stateStr = reader.GetString();
                                navItem.State = stateStr switch
                                {
                                    "new" => NavItemState.New,
                                    "update" => NavItemState.Update,
                                    _ => NavItemState.None
                                };
                                break;
                            case "tiling":
                                var tilingStr = reader.GetString();
                                navItem.Tiling = tilingStr switch
                                {
                                    "some" => NavItemTiling.Some,
                                    "visible" => NavItemTiling.Visible,
                                    "invisible" => NavItemTiling.Invisible,
                                    _ => null
                                };
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
