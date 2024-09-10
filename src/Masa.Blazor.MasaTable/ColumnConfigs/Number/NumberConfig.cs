namespace Masa.Blazor.MasaTable.ColumnConfigs;

public class NumberConfig
{
    public NumberType Type { get; set; }
}

public enum NumberType
{
    Number,
    Currency,
    Percentage
}