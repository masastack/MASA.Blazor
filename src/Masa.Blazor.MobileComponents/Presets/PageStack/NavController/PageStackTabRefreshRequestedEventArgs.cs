namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackTabRefreshRequestedEventArgs(string targetHref) : EventArgs
{
    public string? TargetHref { get; } = targetHref;
}
