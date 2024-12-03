using System.Text.Json.Serialization;
using HotChocolate;

namespace Masa.Blazor.Components.TemplateTable;

public enum ColumnType
{
    Text = 0,
    Email = 1,
    Link = 2,
    Phone = 3,
    Image = 6,
    Select = 11,
    [GraphQLName("MultiSelect")] MultiSelect = 12,
    Date = 20,
    Number = 30,
    Progress = 31,
    Rating = 32,
    Checkbox = 33,
    Actions = 40,
    RowSelect = 41,
    Custom = 100
}