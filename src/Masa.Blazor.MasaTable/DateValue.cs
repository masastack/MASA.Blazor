namespace Masa.Blazor.MasaTable;

public class DateValue
{
    public long Timestamp { get; set; }

    public DateTimeOffset Value { get; set; }

    public string? Text { get; set; }
}