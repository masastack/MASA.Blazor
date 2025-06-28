namespace Masa.Blazor.Components.TemplateTable.Contracts;

public enum ColumnType
{
    Text = 0,
    Email = 1,
    Link = 2,
    Phone = 3,
    Image = 6,
    Select = 11,
    Date = 20,
    Number = 30,
    Progress = 31,
    Rating = 32,
    Checkbox = 40,
    Switch = 41,
    Actions = 90,
    RowSelect = 91,
    Custom = 100
}