namespace Masa.Blazor.Components.Rating;

public class RatingItem : RatingItemContext
{
    public RatingItem(int index, Func<ExMouseEventArgs, Task> click) : base(index, click)
    {
        ForwardRef = new();
    }

    public ForwardRef ForwardRef { get; internal set; }
}