namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackPushEventArgs : EventArgs
{
    public PageStackPushEventArgs(string relativeUri)
    {
        RelativeUri = relativeUri;
    }

    public PageStackPushEventArgs(string relativeUri, int countOfTopPagesToRemove, bool isFirstPage)
        : this(relativeUri)
    {
        CountOfTopPagesToRemove = countOfTopPagesToRemove;
        ClearStack = countOfTopPagesToRemove == -1;
        IsFirstPage = isFirstPage;
    }

    /// <summary>
    /// The relative URI of the page to push.
    /// </summary>
    public string RelativeUri { get; init; }

    /// <summary>
    /// Determines whether to clear the stack before pushing.
    /// </summary>
    public bool ClearStack { get; init; }

    /// <summary>
    /// Determines whether the page to push is the first page in the stack.
    /// </summary>
    public bool IsFirstPage { get; init; }

    /// <summary>
    /// The page to remove from the stack.
    /// If value is -1, it means to remove all pages from the stack.
    /// </summary>
    public int CountOfTopPagesToRemove { get; init; }
}