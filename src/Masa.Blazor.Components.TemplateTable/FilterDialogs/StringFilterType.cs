namespace Masa.Blazor.Components.TemplateTable.FilterDialogs;

public static class FilterTypes
{
    public const string Equal = "eq";
    public const string NotEqual = "neq";
    public const string GreaterThan = "gt";
    public const string NotGreaterThan = "ngt";
    public const string GreaterThanOrEqual = "gte";
    public const string NotGreaterThanOrEqual = "ngte";
    public const string LessThan = "lt";
    public const string NotLessThan = "nlt";
    public const string LessThanOrEqual = "lte";
    public const string NotLessThanOrEqual = "nlte";
    public const string Contains = "contains";
    public const string NotContains = "ncontains";
    public const string StartsWith = "startsWith";
    public const string NotStartsWith = "nstartsWith";
    public const string EndsWith = "endsWith";
    public const string NotEndsWith = "nendsWith";
    
    // --------------------------
    // The following filters are not supported by HotChocolate.
    // They are 'logical' filters.
    // --------------------------
    
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
    
    public static readonly string[] SupportedStringFilters =
    [
        Equal, NotEqual, Contains, NotContains, StartsWith, NotStartsWith, EndsWith, NotEndsWith, // "in", "nin"
    ];

    public static readonly string[] SupportedComparableFilters =
    [
        Equal, NotEqual, GreaterThan, NotGreaterThan, GreaterThanOrEqual, NotGreaterThanOrEqual, LessThan, NotLessThan, LessThanOrEqual, NotLessThanOrEqual, // "in", "nin"
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
}