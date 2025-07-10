using System.Text.Json;

namespace Masa.Blazor.Components.TemplateTable.HotChocolate;

public record HotChocolateQueryResult
{
    public required CollectionSegment Result { get; init; }
}

public record CollectionSegment
{
    public required ICollection<IReadOnlyDictionary<string, JsonElement>>? Items { get; init; }

    public long Count { get; init; }
}