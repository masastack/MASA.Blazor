namespace Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

public enum CubejsFilter
{
    Set = 0,
    Equals = 1,
    NotEquals = 2,
    In = 3,
    NotIn = 4,
    Contains = 5,
    NotContains = 6,
    StartsWith = 7,
    NotStartsWith = 8,
    EndsWith = 8,
    NotEndsWith = 9,

    Gt = 11,
    Gte = 12,
    Lt = 13,
    Lte = 14,

    // InDateRange = 21,
    // NotInDateRange = 22,
    BeforeDate = 23,
    BeforeOrOnDate = 24,
    AfterDate = 25,
    AfterOrOnDate = 26,
}