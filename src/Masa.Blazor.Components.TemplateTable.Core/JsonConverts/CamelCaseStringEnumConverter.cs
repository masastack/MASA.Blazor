using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class CamelCaseStringEnumConverter() : JsonStringEnumConverter(JsonNamingPolicy.CamelCase);