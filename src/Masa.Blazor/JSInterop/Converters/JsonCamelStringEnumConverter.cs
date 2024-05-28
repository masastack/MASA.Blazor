using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorComponent.JSInterop;

public class JsonCamelStringEnumConverter : JsonStringEnumConverter
{
    public JsonCamelStringEnumConverter() : base(new ToLowerNamingPolicy())
    {
    }

    private class ToLowerNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name[..1].ToLower() + name[1..];
    }
}
