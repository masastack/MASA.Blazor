namespace Masa.Blazor.Components.TemplateTable.Abstractions;

public interface IGraphQLClient
{
    Task<QueryResult> QueryAsync(QueryRequest request);
}