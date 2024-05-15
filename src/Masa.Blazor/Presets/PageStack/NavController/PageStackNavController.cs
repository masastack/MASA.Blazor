using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public class PageStackNavController : IDisposable
{
    private readonly IJSRuntime _jsRuntime;

    private readonly NavigationManager _navigationManager;

    public event EventHandler<PageStackPushEventArgs>? StackPush;

    public event EventHandler<PageStackPopEventArgs>? StackPop;

    public event EventHandler<PageStackReplaceEventArgs>? StackReplace;

    public event EventHandler<PageStackClearEventArgs>? StackClear;

    internal event EventHandler<LocationChangedEventArgs>? LocationChanged;

    public PageStackNavController(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
        _navigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        LocationChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Push a new page onto the page stack.
    /// </summary>
    /// <param name="uri"></param>
    public void Push(string uri)
    {
        StackPush?.Invoke(this, new PageStackPushEventArgs(uri));
        _navigationManager.NavigateTo(uri);
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

        StackPop?.Invoke(this, new PageStackPopEventArgs(delta, state));
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);
    }

    /// <summary>
    /// Replace the current page with the new page.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="state"></param>
    public void Replace(string uri, object? state = null)
    {
        StackReplace?.Invoke(this, new PageStackReplaceEventArgs(uri, state));
        _navigationManager.NavigateTo(uri, replace: true);
    }

    /// <summary>
    /// Clear the page stack.
    /// </summary>
    public void Clear()
    {
        StackClear?.Invoke(this, new PageStackClearEventArgs());
    }

    /// <summary>
    /// Clear current page stack and navigate to the new tab.
    /// </summary>
    /// <param name="uri"></param>
    public void GoToTab(string uri)
    {
        StackClear?.Invoke(this, new PageStackClearEventArgs(uri));
    }

    public void Dispose()
    {
        _navigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}