namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public static class SupportedFilter
{
    public static readonly StandardFilter[] SupportedStringFilters =
    [
        StandardFilter.Equals,
        StandardFilter.NotEquals,
        StandardFilter.Contains,
        StandardFilter.NotContains,
        StandardFilter.StartsWith,
        StandardFilter.NotStartsWith,
        StandardFilter.EndsWith,
        StandardFilter.NotEndsWith,
        StandardFilter.Set,
        StandardFilter.NotSet
    ];

    public static readonly StandardFilter[] SupportedNumberFilters =
    [
        StandardFilter.Equals,
        StandardFilter.NotEquals,
        StandardFilter.Gt,
        StandardFilter.Gte,
        StandardFilter.Lt,
        StandardFilter.Lte,
        StandardFilter.Set,
        StandardFilter.NotSet
    ];

    public static readonly StandardFilter[] SupportedDateTimeFilters =
    [
        StandardFilter.Equals,
        StandardFilter.NotEquals,
        StandardFilter.BeforeDate,
        StandardFilter.BeforeOrOnDate,
        StandardFilter.AfterDate,
        StandardFilter.AfterOrOnDate,
        StandardFilter.Set,
        StandardFilter.NotSet
    ];

    public static readonly StandardFilter[] SupportedSelectFilters =
    [
        StandardFilter.Equals,
        StandardFilter.NotEquals,
        StandardFilter.Set,
        StandardFilter.NotSet
    ];

    public static readonly StandardFilter[] SupportedBooleanFilters =
    [
        StandardFilter.True,
        StandardFilter.False,
        StandardFilter.Set,
        StandardFilter.NotSet
    ];
}