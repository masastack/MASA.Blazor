namespace Masa.Blazor.Presets;

public class ActivatorRefProps(Dictionary<string, object> attrs) : ActivatorProps(attrs)
{
    public ForwardRef Ref { get; set; } = new();
}
