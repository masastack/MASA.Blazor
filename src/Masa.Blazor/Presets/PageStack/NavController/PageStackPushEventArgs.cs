namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushEventArgs(string relativeUri, bool clearStack, bool isFirstPage) : EventArgs
{
    /// <summary>
    /// The relative URI of the page to push.
    /// </summary>
    public string RelativeUri { get; } = relativeUri;

    /// <summary>
    /// Determines whether to clear the stack before pushing.
    /// </summary>
    public bool ClearStack { get; } = clearStack;

    /// <summary>
    /// Determines whether the page to push is the first page in the stack.
    /// </summary>
    public bool IsFirstPage { get; } = isFirstPage;
}