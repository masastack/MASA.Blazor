using System.Diagnostics.CodeAnalysis;
using Masa.Blazor.Presets.PageStack.NavController;

namespace Masa.Blazor.Presets;

public class PageStackNavController(string name)
{
    /// <summary>
    /// Records the timestamp of the last action, shared by all actions.
    /// </summary>
    private long _lastActionTimestamp;

    /// <summary>
    /// The bound <see cref="PPageStack"/> component.
    /// </summary>
    private PPageStack? _boundComponent;

    /// <summary>
    /// Occurs when a new page is pushed onto the page stack.
    /// </summary>
    public event EventHandler<PageStackPushEventArgs>? StackPush;

    /// <summary>
    /// Occurs when a page is popped from the page stack.
    /// </summary>
    public event EventHandler<PageStackPopEventArgs>? StackPop;

    /// <summary>
    /// Occurs when a new page replaces a page.
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
    /// Occurs when user invoked the <see cref="GoBackToPage(string)"/> method.
    /// </summary>
    internal event EventHandler<PageStackGoBackToPageEventArgs>? StackGoBackTo;

    /// <summary>
    /// Occurs when the active tab is changed.
    /// </summary>
    public event EventHandler<PageStackTabChangedEventArgs>? TabChanged;

    public string Name { get; init; } = name;

    internal void BindComponent(PPageStack component) => _boundComponent = component;

    internal void UnbindComponent() => _boundComponent = null;

    [MemberNotNull(nameof(_boundComponent))]
    private void AssertComponentBound()
    {
        if (_boundComponent is null)
        {
            throw new InvalidOperationException("The PageStackNavController is not bound to any PageStack component.");
        }
    }

    /// <summary>
    /// Push a new page onto the page stack.
    /// </summary>
    /// <param name="relativeUri">The relative URI of the new page</param>
    /// <param name="clearStack">Determine whether to push new page and remove all old pages</param>
    public void Push(string relativeUri, bool clearStack = false)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var countOfTopPagesToRemove = clearStack ? -1 : 0;
            var eventArgs = new PageStackPushEventArgs(
                relativeUri, countOfTopPagesToRemove, _boundComponent.Pages.Count == 0);

            StackPush?.Invoke(this, eventArgs);

            _boundComponent.Push(eventArgs);
        });
    }

    /// <summary>
    /// Push a new page onto the page stack and remove the specified number of top pages excluding the new page.
    /// </summary>
    /// <param name="relativeUri">The relative URI of the new page</param>
    /// <param name="countOfTopPagesToRemove">The number of top pages to remove</param>
    public void Push(string relativeUri, int countOfTopPagesToRemove)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackPushEventArgs(
                relativeUri, countOfTopPagesToRemove, _boundComponent.Pages.Count == 0);

            StackPush?.Invoke(this, eventArgs);

            _boundComponent.Push(eventArgs);
        });
    }

    /// <summary>
    /// Push a new page onto the page stack and remove all pages until the specified page.
    /// If the <see cref="removeUntilPage"/> is not found, no page will be removed.
    /// </summary>
    /// <param name="relativeUri">The relative URI of the new page</param>
    /// <param name="removeUntilPage">The page to remove until, accepts the absolute path of the page</param>
    /// <example>
    /// <code>
    /// // known pages:  page1 -> page2 -> page3
    /// Push("/page4", "/page1");
    /// // result: page1 -> page4
    /// </code>
    /// </example>
    public void Push(string relativeUri, string removeUntilPage)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var delta = _boundComponent.Pages.GetDelta(removeUntilPage);
            if (delta == -1)
            {
                delta = 0;
            }

            var eventArgs = new PageStackPushEventArgs(
                relativeUri, delta, _boundComponent.Pages.Count == 0);

            StackPush?.Invoke(this, eventArgs);

            _boundComponent.Push(eventArgs);
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
        GoBackAndReplace(delta, null, state);
    }

    /// <summary>
    /// Go back to specified number of steps in the page stack,
    /// and then invoke the <see cref="Replace(string, object?)"/> method.
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="replaceUri"></param>
    /// <param name="state"></param>
    public void GoBackAndReplace(int delta, string? replaceUri, object? state = null)
    {
        if (delta < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(delta), "The delta must be greater than or equal to 1.");
        }

        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackPopEventArgs(delta, replaceUri, state, delta == _boundComponent.Pages.Count);

            StackPop?.Invoke(this, eventArgs);

            _ = _boundComponent.StackPop(eventArgs);
        });
    }

    internal void NotifyStackPopped(int delta, string? replaceUri = null, object? state = null, bool clearing = false)
        => StackPop?.Invoke(this, new PageStackPopEventArgs(delta, replaceUri, state, clearing));

    /// <summary>
    /// Go back to the specified page in the page stack.
    /// If the page is not found, do nothing.
    /// </summary>
    /// <param name="absolutePath"></param>
    /// <param name="state"></param>
    public void GoBackToPage(string absolutePath, object? state = null)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackGoBackToPageEventArgs(absolutePath, state);
            StackGoBackTo?.Invoke(this, eventArgs);

            _boundComponent.GoBackTo(eventArgs);
        });
    }

    /// <summary>
    /// Go back to the specified page in the page stack 
    /// and then invoke the <see cref="Replace(string, object?)"/> method.
    /// </summary>
    /// <param name="absolutePath"></param>
    /// <param name="replaceUri"></param>
    /// <param name="state"></param>
    public void GoBackToPageAndReplace(string absolutePath, string replaceUri, object? state = null)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackGoBackToPageEventArgs(absolutePath, state, replaceUri);

            StackGoBackTo?.Invoke(this, eventArgs);

            _boundComponent.GoBackTo(eventArgs);
        });
    }

    /// <summary>
    /// Replace the current page with the new page.
    /// </summary>
    /// <param name="relativeUri"></param>
    /// <param name="state"></param>
    /// <param name="clearStack">determine whether to replace the current page and remove all previous pages</param>
    public void Replace(string relativeUri, object? state = null, bool clearStack = false)
    {
        AssertComponentBound();

        var eventArgs = new PageStackReplaceEventArgs(relativeUri, state, clearStack);
        StackReplace?.Invoke(this, eventArgs);

        _boundComponent.Replace(eventArgs);
    }

    /// <summary>
    /// Clear the page stack.
    /// </summary>
    public void Clear()
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackClearEventArgs();
            StackClear?.Invoke(this, eventArgs);

            _boundComponent.Clear(eventArgs);
        });
    }

    /// <summary>
    /// Clear the current page stack and navigate to a tab.
    /// </summary>
    /// <param name="relativeUri"></param>
    [Obsolete("Use GoBackToTab instead.")]
    public void GoToTab(string relativeUri)
    {
        GoBackToTab(relativeUri);
    }

    /// <summary>
    /// Clear the current page stack and navigate to a tab.
    /// </summary>
    /// <param name="uri">The tab URI to navigate to.</param>
    public void GoBackToTab(string uri)
    {
        ExecuteIfTimeElapsed(() =>
        {
            AssertComponentBound();

            var eventArgs = new PageStackClearEventArgs(uri);
            StackClear?.Invoke(this, eventArgs);

            _boundComponent.Clear(eventArgs);
        });
    }

    internal void NotifyPageClosed(string relativeUri)
    {
        PageClosed?.Invoke(this, new PageStackPageClosedEventArgs(relativeUri));
    }

    internal void NotifyTabChanged(string currentTabPath, Regex currentTabPattern)
    {
        TabChanged?.Invoke(this, new PageStackTabChangedEventArgs(currentTabPath, currentTabPattern.IsMatch));
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