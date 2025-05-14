namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPageClosedEventArgs(string relativeUri) : EventArgs
{
    public string RelativeUri { get; } = relativeUri;
}