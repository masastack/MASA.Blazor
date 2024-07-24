namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackClearEventArgs : EventArgs
{
    public PageStackClearEventArgs(string relativeUri)
    {
        RelativeUri = relativeUri;
    }

    public PageStackClearEventArgs()
    {
    }

    public string? RelativeUri { get; }
}