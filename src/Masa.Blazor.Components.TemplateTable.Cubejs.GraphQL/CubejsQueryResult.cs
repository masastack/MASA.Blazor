using System.Text.Json;

namespace Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

public record CubejsQueryResult
{
    private readonly List<CubeQueryResultItem> _cube = [];

    public required List<CubeQueryResultItem> Cube
    {
        get => _cube;
        init
        {
            _cube = value;
            Items = _cube.Select(item => item.Data).ToList();
        }
    }

    public ICollection<IReadOnlyDictionary<string, JsonElement>> Items { get; private set; } = [];
}