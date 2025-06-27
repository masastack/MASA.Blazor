using System.Text.Json;

namespace Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

public record CubeQueryResultItem
{
    public required IReadOnlyDictionary<string, JsonElement> Data { get; init; }
}