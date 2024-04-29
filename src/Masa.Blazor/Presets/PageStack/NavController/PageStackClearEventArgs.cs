namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackClearEventArgs : EventArgs
{
    public PageStackClearEventArgs(string uri)
    {
        Uri = uri;
    }

    public PageStackClearEventArgs()
    {
    }

    public string? Uri { get; }
}