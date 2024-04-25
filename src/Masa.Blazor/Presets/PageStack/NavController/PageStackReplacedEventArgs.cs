namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackReplacedEventArgs : EventArgs
{
    public PageStackReplacedEventArgs(string uri, object? state = null)
    {
        Uri = uri;
        State = state;
    }

    public string Uri { get; }

    public object? State { get; }
}