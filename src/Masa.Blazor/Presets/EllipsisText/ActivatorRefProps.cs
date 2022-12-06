namespace Masa.Blazor.Presets;

public class ActivatorRefProps : ActivatorProps
{
    public ActivatorRefProps(Dictionary<string, object> attrs) : base(attrs)
    {
    }

    public ForwardRef Ref { get; set; } = new();
}
