namespace Masa.Blazor.Presets;

public class DateTimeColumn
{
    public int Value { get; }

    public string Text => Formatter?.Invoke(Type, Value) ?? Value.ToString("00");

    public DateTimePrecision Type { get; }

    public Func<DateTimePrecision, int, string> Formatter { get; }

    public List<DateTimeColumn> Children { get; set; } = new();

    public DateTimeColumn(DateTimePrecision type, int value, Func<DateTimePrecision, int, string> formatter)
    {
        Value = value;
        Type = type;
        Formatter = formatter;
    }
}