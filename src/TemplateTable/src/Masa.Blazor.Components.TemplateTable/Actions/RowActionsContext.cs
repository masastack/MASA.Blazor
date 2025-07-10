namespace Masa.Blazor.Components.TemplateTable.Actions;

public record RowActionsContext(Action ShowDetail, IReadOnlyDictionary<string, JsonElement> RowData);