namespace Masa.Blazor.Docs;

public class SliderParameter
{
    public double Min { get; set; }

    public double Max { get; set; }

    public double Step { get; set; } = 1;

    public double Value { get; set; }

    public bool RequireGt0 { get; set; } = true;

    public bool Condition => RequireGt0 ? Value > 0 : true;

    public SliderParameter(double value)
    {
        Value = value;
    }

    public SliderParameter(double value, double min, double max) : this(value)
    {
        Min = min;
        Max = max;
    }

    public SliderParameter(double value, double min, double max, bool requireGt0) : this(value)
    {
        Min = min;
        Max = max;
        RequireGt0 = requireGt0;
    }

    public SliderParameter(double value, double min, double max, double step) : this(value, min, max)
    {
        Step = step;
    }

    public StringNumber ToStringNumber(double value)
    {
        return value;
    }
}
