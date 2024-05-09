namespace Masa.Blazor.Components.Rating;

public class RatingItemContext
{
    public RatingItemContext(int index, Func<ExMouseEventArgs, Task> click)
    {
        Index = index;
        Click = click;
    }

    public int Index { get; init; }

    public double Value { get; internal set; }

    public bool IsFilled { get; internal set; }

    public bool? IsHalfFilled { get; internal set; }

    public bool IsHovered { get; internal set; }

    public bool? IsHalfHovered { get; internal set; }

    public Func<ExMouseEventArgs, Task> Click { get; init; }
}