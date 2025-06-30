using System.Text.Json;

namespace Masa.Blazor.JSInterop;

public class JsonCamelStringEnumConverter() : JsonStringEnumConverter(JsonNamingPolicy.CamelCase);