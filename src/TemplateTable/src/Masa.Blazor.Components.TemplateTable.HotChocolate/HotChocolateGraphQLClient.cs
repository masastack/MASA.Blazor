using System.Runtime.CompilerServices;
using System.Text;
using GraphQL;
using GraphQL.Client.Abstractions.Utilities;
using GraphQL.Client.Http;
using Masa.Blazor.Components.TemplateTable.Abstractions;
using Masa.Blazor.Components.TemplateTable.Contracts;

[assembly: InternalsVisibleTo("Masa.Blazor.Components.TemplateTable.Test")]

namespace Masa.Blazor.Components.TemplateTable.HotChocolate;

public class HotChocolateGraphQLClient(GraphQLHttpClient httpClient) : IGraphQLClient
{
    public async Task<QueryResult> QueryAsync(QueryRequest request)
    {
        var itemsQuery = GetQuery(request);

        var itemsResponse = await httpClient.SendQueryAsync<HotChocolateQueryResult>(new GraphQLRequest(itemsQuery));
        if (itemsResponse.Errors?.Length > 0)
        {
            return new QueryResult(itemsResponse.Errors);
        }

        return new QueryResult(itemsResponse.Data.Result.Items, itemsResponse.Data.Result.Count, null);
    }

    internal static string GetQuery(QueryRequest request)
    {
        var startIndex = request.QueryBody.IndexOf('{');
        var rootField = request.QueryBody[..startIndex].Trim();
        var queryBody = request.QueryBody[startIndex..];

        var arguments = new HashSet<string>();

        if (request.PageIndex > 0)
        {
            var skip = (request.PageIndex - 1) * request.PageSize;
            arguments.Add($"skip: {skip}");
        }

        if (request.PageSize != 0)
        {
            arguments.Add($"take: {request.PageSize}");
        }

        var filtering = GetFiltering(request.FilterRequest);
        if (filtering is not null)
        {
            arguments.Add(filtering);
        }

        var sorting = GetSorting(request.SortRequest);
        if (sorting is not null)
        {
            arguments.Add(sorting);
        }

        var args = string.Join(", ", arguments);

        return $$"""
                 query {
                   result: {{rootField}} ({{args}}) {
                     items {{queryBody}}
                     count: {{request.CountField}}
                   }
                 }
                 """;
    }

    internal static string? GetFiltering(Filter? filterRequest)
    {
        if (filterRequest is null || filterRequest.Options.Count == 0)
        {
            return null;
        }

        var filterItems = filterRequest.Options.Select(option =>
        {
            var (func, expected) = GetHotChocolateFilter(option);
            var filterValue = $"{{{func}: {expected}}}";

            var parts = option.ColumnId.Split('.');
            for (var i = parts.Length - 1; i >= 0; i--)
            {
                filterValue = $"{{{parts[i].ToCamelCase()}: {filterValue}}}";
            }

            return filterValue;
        });

        string filterContent;
        if (filterRequest.Options.Count > 1)
        {
            var op = filterRequest.Operator.ToString().ToLowerInvariant();
            filterContent = $"{{{op}: [{string.Join(", ", filterItems)}]}}";
        }
        else
        {
            filterContent = filterItems.First();
        }

        return $"where: {filterContent}";
    }

    internal static string? GetSorting(Sort? sortRequest)
    {
        if (sortRequest is null || sortRequest.Options.Count == 0)
        {
            return null;
        }

        var arguments = new List<string>();

        foreach (var option in sortRequest.Options)
        {
            var parts = option.ColumnId.Split('.');
            var sorting = $"{option.OrderBy.ToString().ToUpperInvariant()}";

            for (var i = parts.Length - 1; i >= 0; i--)
            {
                sorting = $"{{{parts[i].ToCamelCase()}: {sorting}}}";
            }

            arguments.Add(sorting.Substring(1, sorting.Length - 2));
        }

        return $"order: {{{string.Join(", ", arguments)}}}";
    }

    private static (string Filter, string? Expected) GetHotChocolateFilter(FilterOption option)
    {
        HotChocolateFilter filter;
        string? expected;
        ExpectedType type;
        if (option.Func == StandardFilter.NotSet)
        {
            filter = HotChocolateFilter.Eq;
            expected = "null";
            type = ExpectedType.Expression;
        }
        else if (option.Func == StandardFilter.Set)
        {
            filter = HotChocolateFilter.Neq;
            expected = "null";
            type = ExpectedType.Expression;
        }
        else if (option.Func == StandardFilter.True)
        {
            filter = HotChocolateFilter.Eq;
            expected = "true";
            type = ExpectedType.Boolean;
        }
        else if (option.Func == StandardFilter.False)
        {
            filter = HotChocolateFilter.Eq;
            expected = "false";
            type = ExpectedType.Boolean;
        }
        else
        {
            filter = option.Func switch
            {
                StandardFilter.Equals => HotChocolateFilter.Eq,
                StandardFilter.NotEquals => HotChocolateFilter.Neq,
                // StandardFilter.In => HotChocolateFilter.In,
                // StandardFilter.NotIn => HotChocolateFilter.Nin,
                StandardFilter.Contains => HotChocolateFilter.Contains,
                StandardFilter.NotContains => HotChocolateFilter.Ncontains,
                StandardFilter.StartsWith => HotChocolateFilter.StartsWith,
                StandardFilter.NotStartsWith => HotChocolateFilter.NstartsWith,
                StandardFilter.EndsWith => HotChocolateFilter.EndsWith,
                StandardFilter.NotEndsWith => HotChocolateFilter.NendsWith,
                StandardFilter.Gt => HotChocolateFilter.Gt,
                StandardFilter.Gte => HotChocolateFilter.Gte,
                StandardFilter.Lt => HotChocolateFilter.Lt,
                StandardFilter.Lte => HotChocolateFilter.Lte,
                StandardFilter.BeforeDate => HotChocolateFilter.Lt,
                StandardFilter.BeforeOrOnDate => HotChocolateFilter.Lte,
                StandardFilter.AfterDate => HotChocolateFilter.Gt,
                StandardFilter.AfterOrOnDate => HotChocolateFilter.Gte,
                _ => throw new ArgumentOutOfRangeException(nameof(option.Func), option.Func, null)
            };
            expected = option.Expected;
            type = option.Type;
        }

        return (filter.ToString().ToCamelCase(), FormatExpected(type, expected));
    }

    private static string? FormatExpected(ExpectedType expectedType, string? expected)
    {
        return expectedType is ExpectedType.String or ExpectedType.DateTime
            ? "\"" + expected + "\""
            : expected;
    }
}