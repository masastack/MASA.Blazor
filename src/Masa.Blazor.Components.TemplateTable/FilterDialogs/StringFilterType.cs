namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public static class FilterTypes
{
    public static readonly string[] SupportedStringFilters =
    [
        "eq", "neq", "contains", "ncontains", "startsWith", "nstartsWith", "endsWith", "nendsWith",
        "in", "nin"
    ];

    public static readonly string[] SupportedComparableFilters =
    [
        "Equals", "NotEquals", "GreaterThan", "NotGreaterThan",
        "GreaterThanOrEqual", "NotGreaterThanOrEqual", "LessThan", "NotLessThan",
        "LessThanOrEqual", "NotLessThanOrEqual", //"In", "NotIn"
    ];

    public static readonly string[] SupportedBooleanFilters =
    [
        "Equals", "NotEquals"
    ];

    public static readonly string[] SupportedEnumFilters =
    [
        "Equals", "NotEquals", "In", "NotIn"
    ];

    public static readonly string[] SupportedListFilters =
    [
        "All", "None", "Some", "Any"
    ];
}