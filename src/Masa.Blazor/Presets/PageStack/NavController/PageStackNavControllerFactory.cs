using System.Collections.Concurrent;

namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackNavControllerFactory : IPageStackNavControllerFactory
{
    private readonly IJSRuntime _jsRuntime;

    internal readonly ConcurrentDictionary<string, Lazy<PageStackNavController>> _managers = new();

    public PageStackNavControllerFactory(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public PageStackNavController Create(string name)
    {
        return _managers.GetOrAdd(name,
                _ => new Lazy<PageStackNavController>(() => new PageStackNavController(_jsRuntime)))
            .Value;
    }
}