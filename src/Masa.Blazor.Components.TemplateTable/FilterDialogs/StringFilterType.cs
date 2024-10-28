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
        BooleanFilterEqualTrue,
        BooleanFilterEqualFalse,
    ];

    public static readonly string[] SupportedSelectFilters =
    [
        SelectFilterEqual,
        SelectFilterNotEqual,
        Empty,
        NotEmpty
    ];

    public static readonly string[] SupportedMultiSelectFilters =
    [
        // MultiSelectEqual,
        // MultiSelectNotEqual,
        MultiSelectContainsOne,
        // MultiSelectContainsAll,
        MultiSelectNotContains,
        MultiSelectEmpty,
        MultiSelectNotEmpty
    ];

    public static readonly string[] EmptyFilters =
    [
        Empty,
        NotEmpty
    ];

    public const string BooleanFilterEqualTrue = "bool-eq-true";
    public const string BooleanFilterEqualFalse = "bool-eq-false";

    public const string SelectFilterEqual = "select-eq";
    public const string SelectFilterNotEqual = "select-neq";

    /*
     * The "is" and "is not" of multiple selections do not have a corresponding operator in HotChocolate.
     * Although "and+some+eq" can be used to achieve "is" and "and+all+neq" can be used to achieve "is not",
     * it is cumbersome to write the query statement by combining them,
     * so it is not supported for the time being.
     *
     * But the user can use "and" + "contains one" to achieve "is" and "and" + "not contains" to achieve "is not".
     */
    // public const string MultiSelectEqual = "multi-select-eq";
    // public const string MultiSelectNotEqual = "multi-select-neq";
    // public const string MultiSelectContainsAll = "multi-select-contains-all";

    public const string MultiSelectContainsOne = "multi-select-contains-one";
    public const string MultiSelectNotContains = "multi-select-ncontains";
    public const string MultiSelectEmpty = "multi-select-empty";
    public const string MultiSelectNotEmpty = "multi-select-not-empty";

    public const string Empty = "empty";
    public const string NotEmpty = "not-empty";
}