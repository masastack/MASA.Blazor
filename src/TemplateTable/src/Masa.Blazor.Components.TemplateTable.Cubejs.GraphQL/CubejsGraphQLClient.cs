using System.Runtime.CompilerServices;
using GraphQL;
using GraphQL.Client.Abstractions.Utilities;
using GraphQL.Client.Http;
using Masa.Blazor.Components.TemplateTable.Abstractions;
using Masa.Blazor.Components.TemplateTable.Contracts;

[assembly: InternalsVisibleTo("Masa.Blazor.Components.TemplateTable.Test")]

namespace Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

public class CubejsGraphQLClient(GraphQLHttpClient httpClient) : IGraphQLClient
{
    public async Task<QueryResult> QueryAsync(QueryRequest request)
    {
        var (itemsQuery, countQuery) = GetQuery(request);

        var itemsResponse = await httpClient.SendQueryAsync<CubejsQueryResult>(new GraphQLRequest(itemsQuery));
        if (itemsResponse.Errors?.Length > 0)
        {
            return new QueryResult(itemsResponse.Errors);
        }

        var countResponse = await httpClient.SendQueryAsync<CubejsQueryResult>(new GraphQLRequest(countQuery));
        if (countResponse.Errors?.Length > 0)
        {
            return new QueryResult(countResponse.Errors);
        }

        long total = 0;
        var countItem = countResponse.Data.Items.FirstOrDefault();
        if (countItem is not null)
        {
            total = countItem[request.CountField].GetInt64();
        }

        return new QueryResult(itemsResponse.Data.Items, total, null);
    }

    internal static (string itemsQuery, string countQuery) GetQuery(QueryRequest request)
    {
        var startIndex = request.QueryBody.IndexOf('{');
        var cubeName = request.QueryBody[..startIndex].Trim();
        var queryBody = request.QueryBody[(startIndex + 1)..].TrimEnd('}').Trim();

        var cubeQueryArgs = new List<string>();

        if (request.PageIndex > 0)
        {
            var offset = (request.PageIndex - 1) * request.PageSize;
            cubeQueryArgs.Add($"offset: {offset}");
        }

        if (request.PageSize > 0)
        {
            cubeQueryArgs.Add($"limit: {request.PageSize}");
        }

        var cubeArgs = new List<string>();
        var filtering = GetFiltering(request.FilterRequest);
        if (filtering is not null)
        {
            cubeArgs.Add(filtering);
        }

        var sorting = GetSorting(request.SortRequest);
        if (sorting is not null)
        {
            cubeArgs.Add(sorting);
        }

        var cubeQueryArgsString = cubeQueryArgs.Count != 0 ? $"({string.Join(", ", cubeQueryArgs)}) " : "";
        var cubeArgsString = cubeArgs.Count != 0 ? $"({string.Join(", ", cubeArgs)}) " : "";

        var itemsQuery = $$"""
                           query Cube {
                             cube {{cubeQueryArgsString}}{
                               data: {{cubeName}} {{cubeArgsString}}{
                                 {{queryBody}}
                               }
                             }
                           }
                           """;

        var countQuery = $$"""
                           query Cube {
                             cube {
                               data: {{cubeName}} {{cubeArgsString}}{
                                 {{request.CountField}}
                               }
                             }
                           }
                           """;

        return (itemsQuery, countQuery);
    }

    internal static string? GetFiltering(Filter? filterRequest)
    {
        if (filterRequest is null || filterRequest.Options.Count == 0)
        {
            return null;
        }

        var prefix = "where: ";
        var suffix = "";

        var filterStrings = new List<string>();
        foreach (var option in filterRequest.Options)
        {
            var parts = option.ColumnId.Split('.');
            var (fuc, expected) = GetCubejsFilter(option);

            var filterClause = $"{{{fuc}: {expected}}}";

            for (var i = parts.Length - 1; i >= 0; i--)
            {
                var part = parts[i].ToCamelCase();
                if (option.Type == ExpectedType.DateTime && part == "value")
                {
                    i--;
                    part = parts[i].ToCamelCase();
                }

                filterClause = $"{{{part}: {filterClause}}}";
            }

            filterStrings.Add(filterClause);
        }

        var arguments = string.Join(", ", filterStrings);

        if (filterRequest.Options.Count > 1)
        {
            prefix += $"{{{filterRequest.Operator.ToString().ToUpperInvariant()}: [";
            suffix = "]}" + suffix;
        }

        return prefix + arguments + suffix;
    }

    internal static string? GetSorting(Sort? sortRequest)
    {
        if (sortRequest is null || sortRequest.Options.Count == 0)
        {
            return null;
        }

        var sortObjects = new List<string>();

        foreach (var option in sortRequest.Options)
        {
            var parts = option.ColumnId.Split('.');

            var lastPart = parts[^1].ToCamelCase();
            var start = parts.Length - 2;

            if (option.Type == ExpectedType.DateTime && lastPart == "value")
            {
                lastPart = parts[^2].ToCamelCase();
                start--;
            }

            var sortString = $"{{{lastPart}: {option.OrderBy.ToString().ToLowerInvariant()}}}";

            for (var i = start; i >= 0; i--)
            {
                var currentPart = parts[i].ToCamelCase();
                sortString = $"{{{currentPart}: {sortString}}}";
            }

            sortObjects.Add(sortString.Substring(1, sortString.Length - 2));
        }

        return $"orderBy: {{{string.Join(", ", sortObjects)}}}";
    }

    private static (string Filter, string? Expected) GetCubejsFilter(FilterOption option)
    {
        CubejsFilter filter;
        string? expected;
        ExpectedType type;
        switch (option.Func)
        {
            case StandardFilter.NotSet:
                filter = CubejsFilter.Set;
                expected = "false";
                type = ExpectedType.Boolean;
                break;
            case StandardFilter.True:
                filter = CubejsFilter.Equals;
                expected = "true";
                type = ExpectedType.String;
                break;
            case StandardFilter.False:
                filter = CubejsFilter.Equals;
                expected = "false";
                type = ExpectedType.String;
                break;
            default:
                filter = option.Func switch
                {
                    StandardFilter.Set => CubejsFilter.Set,
                    StandardFilter.Equals => CubejsFilter.Equals,
                    StandardFilter.NotEquals => CubejsFilter.NotEquals,
                    // StandardFilter.In => CubejsFilter.In,
                    // StandardFilter.NotIn => CubejsFilter.NotIn,
                    StandardFilter.Contains => CubejsFilter.Contains,
                    StandardFilter.NotContains => CubejsFilter.NotContains,
                    StandardFilter.StartsWith => CubejsFilter.StartsWith,
                    StandardFilter.NotStartsWith => CubejsFilter.NotStartsWith,
                    StandardFilter.EndsWith => CubejsFilter.EndsWith,
                    StandardFilter.NotEndsWith => CubejsFilter.NotEndsWith,
                    StandardFilter.Gt => CubejsFilter.Gt,
                    StandardFilter.Gte => CubejsFilter.Gte,
                    StandardFilter.Lt => CubejsFilter.Lt,
                    StandardFilter.Lte => CubejsFilter.Lte,
                    StandardFilter.BeforeDate => CubejsFilter.BeforeDate,
                    StandardFilter.BeforeOrOnDate => CubejsFilter.BeforeOrOnDate,
                    StandardFilter.AfterDate => CubejsFilter.AfterDate,
                    StandardFilter.AfterOrOnDate => CubejsFilter.AfterOrOnDate,
                    _ => throw new ArgumentOutOfRangeException(nameof(option.Func), option.Func, null)
                };
                expected = option.Expected;
                type = option.Type;
                break;
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