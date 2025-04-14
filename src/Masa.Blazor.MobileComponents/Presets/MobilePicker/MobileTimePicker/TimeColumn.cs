namespace Masa.Blazor.Presets;

public class TimeColumn : DateTimeColumn<TimePrecision>
{
    public TimeColumn(TimePrecision precision, int value, Func<TimePrecision, int, string>? formatter) : base(precision, value, formatter)
    {
    }

    public List<TimeColumn> Children { get; set; } = new();
}
