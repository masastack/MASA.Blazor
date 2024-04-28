using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public class PageStackNavController : IDisposable
{
    private readonly IJSRuntime _jsRuntime;

    private readonly NavigationManager _navigationManager;

    internal event EventHandler<PageStackPushedEventArgs>? PagePushed;

    internal event EventHandler<PageStackPoppedEventArgs>? PagePopped;

    internal event EventHandler<PageStackReplacedEventArgs>? PageReplaced;
    
    internal event EventHandler<PageStackPushedEventArgs>? PageCleared;

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

    public void Push(string uri)
    {
        PagePushed?.Invoke(this, new PageStackPushedEventArgs(uri));
        _navigationManager.NavigateTo(uri);
    }

    public void Pop(object? state = null)
    {
        GoBack(1, state);
    }

    public void GoBack(int delta = 1, object? state = null)
    {
        if (delta < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(delta), "The delta must be greater than or equal to 1.");
        }

        PagePopped?.Invoke(this, new PageStackPoppedEventArgs(delta, state));
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);
    }

    public void Replace(string uri, object? state = null)
    {
        PageReplaced?.Invoke(this, new PageStackReplacedEventArgs(uri, state));
        _navigationManager.NavigateTo(uri, replace: true);
    }

    public void Tab(string uri)
    {
        PageCleared?.Invoke(this, new PageStackPushedEventArgs(uri));
    }

    public void Dispose()
    {
        _navigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}