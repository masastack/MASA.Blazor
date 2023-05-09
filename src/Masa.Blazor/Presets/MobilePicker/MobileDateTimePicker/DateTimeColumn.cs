namespace Masa.Blazor.Presets;

public class DateTimeColumn : DateTimeColumn<DateTimePrecision>
{
    public DateTimeColumn(DateTimePrecision precision, int value, Func<DateTimePrecision, int, string> formatter) : base(precision, value, formatter)
    {
    }

    public List<DateTimeColumn> Children { get; set; } = new();
}

public class DateTimeColumn<TPrecision>
{
    public int Value { get; }

    public string Text => Formatter?.Invoke(Precision, Value) ?? Value.ToString("00");

    public TPrecision Precision { get; }

    public Func<TPrecision, int, string>? Formatter { get; }

    public DateTimeColumn(TPrecision precision, int value, Func<TPrecision, int, string>? formatter)
    {
        Value = value;
        Precision = precision;
        Formatter = formatter;
    }
}
