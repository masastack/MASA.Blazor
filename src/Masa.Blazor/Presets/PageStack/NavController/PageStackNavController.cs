using Masa.Blazor.Presets.PageStack.NavController;

namespace Masa.Blazor.Presets;

public class PageStackNavController(IJSRuntime jsRuntime, NavigationManager navigationManager)
{
    /// <summary>
    /// Records the timestamp of the last action, shared by all actions.
    /// </summary>
    private long _lastActionTimestamp;

    /// <summary>
    /// Occurs when a new page is pushed onto the page stack.
    /// </summary>
    public event EventHandler<PageStackPushEventArgs>? StackPush;

    /// <summary>
    /// Occurs when a page is popped from the page stack.
    /// </summary>
    public event EventHandler<PageStackPopEventArgs>? StackPop;

    /// <summary>
    /// Occurs when a page is replaced by a new page.
    /// </summary>
    public event EventHandler<PageStackReplaceEventArgs>? StackReplace;

    /// <summary>
    /// Occurs when the page stack is cleared.
    /// </summary>
    public event EventHandler<PageStackClearEventArgs>? StackClear;

    /// <summary>
    /// Occurs when the page is closed.
    /// </summary>
    public event EventHandler<PageStackPageClosedEventArgs>? PageClosed;

    /// <summary>
    /// Push a new page onto the page stack.
    /// </summary>
    /// <param name="relativeUri"></param>
    public void Push(string relativeUri)
    {
        ExecuteIfTimeElapsed(() =>
        {
            StackPush?.Invoke(this, new PageStackPushEventArgs(relativeUri));
            navigationManager.NavigateTo(relativeUri);
        });
    }

    /// <summary>
    /// Go back one step in the page stack.
    /// </summary>
    /// <param name="state"></param>
    public void Pop(object? state = null)
    {
        GoBack(1, state);
    }

    /// <summary>
    /// Go back the specified number of steps in the page stack.
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="state"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void GoBack(int delta = 1, object? state = null)
    {
        if (delta < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(delta), "The delta must be greater than or equal to 1.");
        }

        ExecuteIfTimeElapsed(() =>
        {
            StackPop?.Invoke(this, new PageStackPopEventArgs(delta, state));
            _ = jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);
        });
    }

    /// <summary>
    /// Replace the current page with the new page.
    /// </summary>
    /// <param name="relativeUri"></param>
    /// <param name="state"></param>
    public void Replace(string relativeUri, object? state = null)
    {
        ExecuteIfTimeElapsed(() =>
        {
            StackReplace?.Invoke(this, new PageStackReplaceEventArgs(relativeUri, state));
            navigationManager.NavigateTo(relativeUri, replace: true);
        });
    }

    /// <summary>
    /// Clear the page stack.
    /// </summary>
    public void Clear()
    {
        ExecuteIfTimeElapsed(() => { StackClear?.Invoke(this, new PageStackClearEventArgs()); });
    }

    /// <summary>
    /// Clear current page stack and navigate to the new tab.
    /// </summary>
    /// <param name="relativeUri"></param>
    public void GoToTab(string relativeUri)
    {
        ExecuteIfTimeElapsed(() => { StackClear?.Invoke(this, new PageStackClearEventArgs(relativeUri)); });
    }

    internal void NotifyPageClosed(string relativeUri)
    {
        PageClosed?.Invoke(this, new PageStackPageClosedEventArgs(relativeUri));
    }

    private void ExecuteIfTimeElapsed(Action action)
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (now - _lastActionTimestamp > 250)
        {
            action();
        }

        _lastActionTimestamp = now;
    }
}