namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushEventArgs(string relativeUri) : EventArgs
{
    public string RelativeUri { get; } = relativeUri;
}