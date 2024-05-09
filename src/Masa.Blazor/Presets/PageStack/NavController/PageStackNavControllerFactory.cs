using System.Collections.Concurrent;

namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackNavControllerFactory : IPageStackNavControllerFactory
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;

    internal readonly ConcurrentDictionary<string, Lazy<PageStackNavController>> _managers = new();

    public PageStackNavControllerFactory(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    public PageStackNavController Create(string name)
    {
        return _managers.GetOrAdd(name,
                _ => new Lazy<PageStackNavController>(() => new PageStackNavController(_jsRuntime, _navigationManager)))
            .Value;
    }
}