namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackClearEventArgs : EventArgs
{
    public PageStackClearEventArgs(string relativeUri, bool disableTransition = false)
    {
        RelativeUri = relativeUri;
        DisableTransition = disableTransition;
    }

    public PageStackClearEventArgs(bool disableTransition = false)
    {
        DisableTransition = disableTransition;
    }

    public string? RelativeUri { get; }
    
    internal bool DisableTransition { get; }
}