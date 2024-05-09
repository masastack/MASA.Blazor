namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackReplaceEventArgs : EventArgs
{
    public PageStackReplaceEventArgs(string uri, object? state = null)
    {
        Uri = uri;
        State = state;
    }

    public string Uri { get; }

    public object? State { get; }
}