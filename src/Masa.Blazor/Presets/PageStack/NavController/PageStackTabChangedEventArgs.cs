namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabChangedEventArgs(string currentTab, Func<string, bool> isMatch) : EventArgs
{
    /// <summary>
    /// The absolute path of the current tab.
    /// </summary>
    public string CurrentTab { get; init; } = currentTab;

    /// <summary>
    /// Checks whether the given path matches the current tab.
    /// </summary>
    /// <returns>
    /// True if the path matches the current tab; otherwise, false.
    /// </returns>
    public Func<string, bool> IsMatch { get; init; } = isMatch;
}