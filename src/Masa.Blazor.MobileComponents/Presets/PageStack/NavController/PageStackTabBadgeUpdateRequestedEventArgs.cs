namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabBadgeUpdateRequestedEventArgs : EventArgs
{
    /// <summary>
    /// Creates an event args to update the badge with a dot for a specific tab.
    /// </summary>
    /// <param name="targetTab"></param>
    /// <param name="dot"></param>
    /// <param name="overLap"></param>
    /// <param name="color"></param>
    public PageStackTabBadgeUpdateRequestedEventArgs(TabRule targetTab, bool dot, bool overLap = false,
        string color = "red")
    {
        Value = true;
        TargetTab = targetTab;
        Dot = dot;
        OverLap = overLap;
        Color = color;
    }

    /// <summary>
    /// Creates an event args to update the badge with a text or number for a specific tab.
    /// </summary>
    /// <param name="targetTab"></param>
    /// <param name="badge"></param>
    /// <param name="overLap"></param>
    /// <param name="color"></param>
    public PageStackTabBadgeUpdateRequestedEventArgs(TabRule targetTab, StringNumber badge, bool overLap = false,
        string color = "red")
    {
        Value = true;
        TargetTab = targetTab;
        Badge = badge;
        OverLap = overLap;
        Color = color;
    }

    /// <summary>
    /// Creates an event args to clear the badge for a specific tab.
    /// </summary>
    /// <param name="targetTab"></param>
    private PageStackTabBadgeUpdateRequestedEventArgs(TabRule targetTab)
    {
        Value = false;
        TargetTab = targetTab;
    }

    /// <summary>
    /// Creates an event args to clear the badge for a specific tab.
    /// </summary>
    /// <param name="targetTab"></param>
    /// <returns></returns>
    internal static PageStackTabBadgeUpdateRequestedEventArgs CreateClearEventArgs(TabRule targetTab)
    {
        return new PageStackTabBadgeUpdateRequestedEventArgs(targetTab);
    }

    public TabRule TargetTab { get; }

    public bool Dot { get; set; }

    public StringNumber? Badge { get; set; }

    public string? Color { get; set; }

    public bool OverLap { get; set; }

    internal bool Value { get; private set; }
}