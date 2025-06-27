namespace Masa.Blazor.Components.TemplateTable.Core;

public interface IGraphQLClient
{
    Task<Result> QueryAsync(QueryRequest request);
}