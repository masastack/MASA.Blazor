using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor;

public class StackChangedEventArgs : EventArgs
{
    public StackChangedEventArgs(string uri, object? state = null)
    {
        Uri = uri;
        State = state;
    }

    public string Uri { get; }

    public object? State { get; set; }
}

public class StackNavigationManager : IDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;

    public event EventHandler<StackChangedEventArgs> PagePushed;
    public event EventHandler<StackChangedEventArgs> PagePopped;
    public event EventHandler<StackChangedEventArgs> PageReplaced;
    public event EventHandler<LocationChangedEventArgs> LocationChanged;

    public StackNavigationManager(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
        _navigationManager.LocationChanged += LocationChanged;
    }

    public string Uri => _navigationManager.Uri;

    public void Push(string uri)
    {
        Console.Out.WriteLine("[StackNavigationManager] Push: " + uri);
        PagePushed?.Invoke(this, new StackChangedEventArgs(uri));
        _navigationManager.NavigateTo(uri);
    }

    public void NavigateTo(string uri) => NavigateTo(uri, false);

    public void NavigateTo(string uri, bool replace)
    {
        if (replace)
        {
            Replace(uri);
        }
        else
        {
            Push(uri);
        }
    }

    public void Pop()
    {
        Console.Out.WriteLine("[StackNavigationManager] Pop: " + _navigationManager.Uri);
        PagePopped?.Invoke(this, new StackChangedEventArgs(_navigationManager.Uri));
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryBack);
    }

    public void Back(object? state = null)
    {
        Console.Out.WriteLine("[StackNavigationManager] GoBack: " + _navigationManager.Uri);
        Go(-1, state);
    }

    public void Go(int delta, object? state = null)
    {
        Console.Out.WriteLine("[StackNavigationManager] Go: " + delta);
        PagePopped?.Invoke(this, new StackChangedEventArgs(_navigationManager.Uri, state));
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryGo, delta);
    }

    public void PopAndReplace(string uri)
    {
        Pop();
        _navigationManager.NavigateTo(uri, replace: true);
    }

    public void Replace(string uri)
    {
        Console.Out.WriteLine("[StackNavigationManager] Replace: " + uri);
        PageReplaced?.Invoke(this, new StackChangedEventArgs(uri));
        _navigationManager.NavigateTo(uri, replace: true);
    }

    public void Dispose()
    {
        _navigationManager.LocationChanged -= LocationChanged;
    }
}