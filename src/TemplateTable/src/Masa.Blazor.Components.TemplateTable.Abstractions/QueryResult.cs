namespace Masa.Blazor.Components.TemplateTable.Abstractions;

public record QueryResult(ICollection<IReadOnlyDictionary<string, JsonElement>>? Items, long Total, GraphQLError[]? Errors)
{
    public QueryResult(GraphQLError[] errors) : this(null, 0, errors)
    {
    }

    [MemberNotNullWhen(true, nameof(Errors))]
    [MemberNotNullWhen(false, nameof(Items))]
    public bool HasErrors => Errors is not null && Errors.Length > 0;
}