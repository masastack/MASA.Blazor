namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushedEventArgs : EventArgs
{
    public PageStackPushedEventArgs(string uri)
    {
        Uri = uri;
    }

    public string Uri { get; }
}