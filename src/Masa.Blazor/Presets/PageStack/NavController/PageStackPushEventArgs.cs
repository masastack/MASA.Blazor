namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushEventArgs(string relativeUri, bool clearStack) : EventArgs
{
    public string RelativeUri { get; } = relativeUri;

    public bool ClearStack { get; } = clearStack;
}