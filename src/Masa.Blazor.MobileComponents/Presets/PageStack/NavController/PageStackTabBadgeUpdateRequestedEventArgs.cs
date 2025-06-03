namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabBadgeUpdateRequestedEventArgs : EventArgs
{
    public PageStackTabBadgeUpdateRequestedEventArgs(string targetHref, bool dot, bool overLap = false)
    {
        Value = true;
        TargetHref = targetHref;
        Dot = dot;
        OverLap = overLap;
    }

    public PageStackTabBadgeUpdateRequestedEventArgs(string targetHref, StringNumber content, bool overLap = false)
    {
        Value = true;
        TargetHref = targetHref;
        Content = content;
        OverLap = overLap;
    }

    private PageStackTabBadgeUpdateRequestedEventArgs(string targetHref, bool value)
    {
        TargetHref = targetHref;
        Value = value;
    }

    /// <summary>
    /// Creates an event args to clear the badge for a specific target href.
    /// </summary>
    /// <param name="targetHref"></param>
    /// <returns></returns>
    internal static PageStackTabBadgeUpdateRequestedEventArgs CreateClearEventArgs(string targetHref)
    {
        return new PageStackTabBadgeUpdateRequestedEventArgs(targetHref, false);
    }
    
    public string? TargetHref { get; set; }

    public bool Dot { get; set; }

    public StringNumber? Content { get; set; }

    public string? Color { get; set; } = "red";

    public bool OverLap { get; set; }

    internal bool Value { get; private set; }
}