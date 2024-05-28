namespace BlazorComponent;

public class Locale
{
    public string? Current { get; }

    public string? Fallback { get; }

    public string? UICurrent { get; set; }

    public string? UIFallback { get; set; }

    public Locale(string current, string fallback)
    {
        Current = current;
        Fallback = fallback;
    }

    public Locale(string current, string uiCurrent, string fallback, string uiFallback) : this(current, fallback: fallback)
    {
        UICurrent = uiCurrent;
        UIFallback = uiFallback;
    }
}
