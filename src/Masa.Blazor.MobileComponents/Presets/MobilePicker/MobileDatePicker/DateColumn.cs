namespace Masa.Blazor.Presets;

public class DateColumn : DateTimeColumn<DatePrecision>
{
    public DateColumn(DatePrecision precision, int value, Func<DatePrecision, int, string>? formatter) : base(precision, value, formatter)
    {
    }

    public List<DateColumn> Children { get; set; } = new();
}
