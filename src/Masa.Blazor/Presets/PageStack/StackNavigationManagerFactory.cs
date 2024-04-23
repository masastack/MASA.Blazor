using System.Collections.Concurrent;

namespace Masa.Blazor;

public class StackNavigationManagerFactory : IStackNavigationManagerFactory
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;

    internal readonly ConcurrentDictionary<string, Lazy<StackNavigationManager>> _managers = new();

    public StackNavigationManagerFactory(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    public StackNavigationManager Create(string name)
    {
        return _managers.GetOrAdd(name,
                _ => new Lazy<StackNavigationManager>(() => new StackNavigationManager(_jsRuntime, _navigationManager)))
            .Value;
    }
}