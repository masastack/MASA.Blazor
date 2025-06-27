namespace Masa.Blazor.Components.TemplateTable.Core;

public enum StandardFilter
{
    Set = 0,
    NotSet = 1,
    True = 2,
    False = 3,

    Equals = 10,
    NotEquals = 11,
    // In = 12,
    // NotIn = 13,
    Contains = 14,
    NotContains = 15,
    StartsWith = 16,
    NotStartsWith = 17,
    EndsWith = 18,
    NotEndsWith = 19,

    Gt = 30,
    Gte = 31,
    Lt = 32,
    Lte = 33,

    BeforeDate = 50,
    BeforeOrOnDate = 51,
    AfterDate = 52,
    AfterOrOnDate = 53,
}