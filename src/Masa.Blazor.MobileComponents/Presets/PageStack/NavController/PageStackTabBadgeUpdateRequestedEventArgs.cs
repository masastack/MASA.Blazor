namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabBadgeUpdateRequestedEventArgs : EventArgs
{
    /// <summary>
    /// Creates an event args to update the badge with a dot for a specific target href.
    /// </summary>
    /// <param name="targetHref"></param>
    /// <param name="dot"></param>
    /// <param name="overLap"></param>
    /// <param name="color"></param>
    public PageStackTabBadgeUpdateRequestedEventArgs(string targetHref, bool dot, bool overLap = false,
        string color = "red")
    {
        Value = true;
        TargetHref = targetHref;
        Dot = dot;
        OverLap = overLap;
        Color = color;
    }

    /// <summary>
    /// Creates an event args to update the badge with a text or number for a specific target href.
    /// </summary>
    /// <param name="targetHref"></param>
    /// <param name="badge"></param>
    /// <param name="overLap"></param>
    /// <param name="color"></param>
    public PageStackTabBadgeUpdateRequestedEventArgs(string targetHref, StringNumber badge, bool overLap = false,
        string color = "red")
    {
        Value = true;
        TargetHref = targetHref;
        Badge = badge;
        OverLap = overLap;
        Color = color;
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

    public StringNumber? Badge { get; set; }

    public string? Color { get; set; }

    public bool OverLap { get; set; }

    internal bool Value { get; private set; }
}