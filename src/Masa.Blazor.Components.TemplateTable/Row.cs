namespace Masa.Blazor.Components.TemplateTable;

public record Row(string? Key, IReadOnlyDictionary<string, JsonElement> Data);