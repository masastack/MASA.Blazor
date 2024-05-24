namespace BlazorComponent;

public class SvgPath
{
    public string D { get; }

    public Dictionary<string, object> Attributes { get; }

    public SvgPath(string d, Dictionary<string, object>? attributes = null)
    {
        D = d;
        Attributes = attributes ?? new();
    }
}
