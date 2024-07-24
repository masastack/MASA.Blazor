namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackReplaceEventArgs(string relativeUri, object? state = null) : EventArgs
{
    public string RelativeUri { get; } = relativeUri;

    public object? State { get; } = state;
}