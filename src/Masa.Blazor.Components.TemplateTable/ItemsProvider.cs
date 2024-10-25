using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using GraphQL.Client.Abstractions.Utilities;
using Masa.Blazor.Components.TemplateTable.FilterDialogs;

[assembly: InternalsVisibleTo("Masa.Blazor.Test")]

namespace Masa.Blazor.Components.TemplateTable;

public delegate ValueTask<ItemsProviderResult> ItemsProvider(ItemsProviderRequest request);

public readonly struct ItemsProviderRequest
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public Filter? FilterRequest { get; init; }

    public Sort? SortRequest { get; init; }

    //TODO: Sort

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public string FormatQuery(string query)
    {
        var regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*\s*\{[\s\d\w]+\}$");
        if (!regex.IsMatch(query))
        {
            throw new ArgumentException("Invalid query");
        }

        var startIndex = query.IndexOf('{');
        var rootField = query[..startIndex].Trim();
        var queryBody = query[startIndex..];

        var arguments = new HashSet<string>();

        if (PageIndex > 0)
        {
            var skip = (PageIndex - 1) * PageSize;
            arguments.Add($"skip: {skip}");
        }

        if (PageSize != 0)
        {
            arguments.Add($"take: {PageSize}");
        }

        var filtering = GetFiltering(FilterRequest);
        if (filtering is not null)
        {
            arguments.Add(filtering);
        }

        var sorting = GetSorting(SortRequest);
        if (sorting is not null)
        {
            arguments.Add(sorting);
        }

        var args = string.Join(", ", arguments);

        return $$"""
                 query {
                   result: {{rootField}} ({{args}}) {
                     items {{queryBody}}
                     pageInfo {
                       hasNextPage
                       hasPreviousPage
                     }
                     totalCount
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

        var argumentsBuilder = new StringBuilder();
        var prefix = "where: ";
        var suffix = "";

        if (filterRequest.Options.Count == 1)
        {
            var option = filterRequest.Options.First();
            argumentsBuilder.Append($"{{{option.ColumnId.ToCamelCase()}: {{{option.Func}: {FormatExpected(option)}}}}}");
        }
        else
        {
            foreach (var option in filterRequest.Options)
            {
                argumentsBuilder.Append(
                    $"{{{option.ColumnId.ToCamelCase()}: {{{option.Func}: {FormatExpected(option)}}}}}, ");
            }

            argumentsBuilder.Remove(argumentsBuilder.Length - 2, 2);
        }

        if (filterRequest.Options.Count > 1)
        {
            prefix += $"{{{filterRequest.Operator.ToString().ToLowerInvariant()}: [";
            suffix = "]}" + suffix;
        }

        return prefix + argumentsBuilder + suffix;
    }

    internal static string? GetSorting(Sort? sortRequest)
    {
        if (sortRequest is null || sortRequest.Options.Count == 0)
        {
            return null;
        }

        List<string> arguments = new();

        foreach (var option in sortRequest.Options)
        {
            arguments.Add($"{option.ColumnId.ToCamelCase()}: {option.OrderBy.ToString().ToUpperInvariant()}");
        }

        return $"order: {{{string.Join(", ", arguments)}}}";
    }

    private static string FormatExpected(FilterOption option)
    {
        return option.Type == ExpectedType.String ? "\"" + option.Expected + "\"" : option.Expected;
    }
}

public readonly struct ItemsProviderResult
{
    public required CollectionSegment Result { get; init; }
}

public readonly struct CollectionSegment
{
    public required ICollection<IReadOnlyDictionary<string, JsonElement>>? Items { get; init; }

    public required CollectionSegmentInfo PageInfo { get; init; }

    public long TotalCount { get; init; }
}

public readonly struct CollectionSegmentInfo
{
    public bool HasNextPage { get; init; }

    public bool HasPreviousPage { get; init; }
}