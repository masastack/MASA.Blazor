namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public static class FilterTypes
{
    public static readonly string[] SupportedStringFilters =
    [
        "eq", "neq", "contains", "ncontains", "startsWith", "nstartsWith", "endsWith", "nendsWith", //"in", "nin"
    ];

    public static readonly string[] SupportedComparableFilters =
    [
        "eq", "neq", "gt", "ngt", "gte", "ngte", "lt", "nlt", "lte", "nlte", //"in", "nin"
    ];

    public static readonly string[] SupportedBooleanFilters =
    [
        BooleanFilterEqTrue,
        BooleanFilterEqFalse,
    ];

    public const string BooleanFilterEqTrue = "bool-eq-true";
    public const string BooleanFilterEqFalse = "bool-eq-false";
    
    public static readonly string[] SupportedListFilters =
    [
        "All", "None", "Some", "Any"
    ];
}