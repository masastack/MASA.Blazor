namespace Masa.Blazor.Presets;

public class PageStackTabBadgeProps
{
    public PageStackTabBadgeProps(bool dot, bool overLap = false, string color = "red")
    {
        Dot = dot;
        OverLap = overLap;
        Color = color;
    }

    public PageStackTabBadgeProps(StringNumber badge, bool overLap = false, string color = "red")
    {
        Badge = badge;
        OverLap = overLap;
        Color = color;
    }

    public bool Dot { get; set; }

    public StringNumber? Badge { get; set; }

    public bool OverLap { get; set; }

    public string? Color { get; set; }
}