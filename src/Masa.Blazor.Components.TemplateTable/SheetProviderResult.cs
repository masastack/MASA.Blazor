namespace Masa.Blazor.Components.TemplateTable;

public readonly struct SheetProviderResult
{
    public required SheetManager Sheet { get; init; }

    public static SheetProviderResult From(Sheet sheet, ICollection<IReadOnlyDictionary<string, JsonElement>> items,
        int totalItemCount)
    {
        return new SheetProviderResult
        {
            Sheet = SheetManager.From(sheet),
            // Items = items,
            // TotalItemCount = totalItemCount
        };
    }
}