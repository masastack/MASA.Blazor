using Masa.Blazor.Components.TemplateTable.FilterDialogs;

namespace Masa.Blazor.Components.TemplateTable;

public readonly struct SheetProviderRequest(string query)
{
    /// <summary>
    /// The built GraphQL query string to get the sheet.
    /// The root field should be `sheet`.
    /// </summary>
    public string Query { get; init; } = query;
}