namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabRefreshRequestedEventArgs(TabRule targetTab) : EventArgs
{
    public TabRule? TargetTab { get; } = targetTab;
}