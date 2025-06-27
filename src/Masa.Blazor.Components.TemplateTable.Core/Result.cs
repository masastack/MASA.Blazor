using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using GraphQL;

namespace Masa.Blazor.Components.TemplateTable.Core;

public record Result(ICollection<IReadOnlyDictionary<string, JsonElement>>? Items, long Total, GraphQLError[]? Errors)
{
    public Result(GraphQLError[] errors) : this(null, 0, errors)
    {
    }

    [MemberNotNullWhen(true, nameof(Errors))]
    [MemberNotNullWhen(false, nameof(Items))]
    public bool HasErrors => Errors is not null && Errors.Length > 0;
}