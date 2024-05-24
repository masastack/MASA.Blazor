namespace BlazorComponent;

public class HoverProps : ActivatorProps
{
    public HoverProps(bool hover, Dictionary<string, object> attrs) : base(attrs)
    {
        Hover = hover;
    }

    public bool Hover { get; internal set; }
}
