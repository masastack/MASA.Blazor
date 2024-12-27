namespace Masa.Blazor.Components.TemplateTable.ColumnConfigs;

public class NumberConfig
{
    public NumberType Type { get; set; }
}

public enum NumberType
{
    Default,
    Number,
    Currency,
    Percentage
}