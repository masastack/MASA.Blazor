namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushEventArgs : EventArgs
{
    public PageStackPushEventArgs(string uri)
    {
        Uri = uri;
    }

    public string Uri { get; }
}