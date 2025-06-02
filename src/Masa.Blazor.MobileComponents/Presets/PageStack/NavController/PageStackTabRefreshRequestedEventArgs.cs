namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabRefreshRequestedEventArgs(string targetHref) : EventArgs
{
    public string? TargetHref { get; } = targetHref;
}

public class PageStackTabBadgeUpdateEventArgs : EventArgs
{
    public PageStackTabBadgeUpdateEventArgs(string targetHref, bool dot, bool overLap = false)
    {
        TargetHref = targetHref;
        Dot = dot;
        OverLap = overLap;
    }

    public PageStackTabBadgeUpdateEventArgs(string targetHref, StringNumber content, bool overLap = false)
    {
        TargetHref = targetHref;
        Content = content;
        OverLap = overLap;
    }

    internal PageStackTabBadgeUpdateEventArgs(string targetHref, bool value)
    {
        TargetHref = targetHref;
        Value = value;
    }
    
    public string? TargetHref { get; set; }

    public bool Dot { get; set; }

    public StringNumber? Content { get; set; }

    public string? Color { get; set; } = "red";

    public bool OverLap { get; set; }

    internal bool Value { get; set; }
}