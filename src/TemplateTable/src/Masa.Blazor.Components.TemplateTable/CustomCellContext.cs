namespace Masa.Blazor.Components.TemplateTable;

public record CustomCellContext(string ColumnId, JsonElement Value, IReadOnlyDictionary<string, JsonElement> Data, RenderFragment @Default);