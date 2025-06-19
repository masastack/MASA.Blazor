namespace Masa.Blazor;

public class HoverProps(bool hover, Dictionary<string, object> attrs) : ActivatorProps(attrs)
{
    public bool Hover { get; internal set; } = hover;
}