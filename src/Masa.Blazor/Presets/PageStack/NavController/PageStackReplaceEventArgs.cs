namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackReplaceEventArgs(string relativeUri, object? state = null, bool clearStack = false) : EventArgs
{
    public string RelativeUri { get; } = relativeUri;

    public object? State { get; } = state;

    public bool ClearStack { get; } = clearStack;
}