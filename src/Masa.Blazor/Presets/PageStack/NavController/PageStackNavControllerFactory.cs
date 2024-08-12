using System.Collections.Concurrent;

namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackNavControllerFactory : IPageStackNavControllerFactory
{
    private readonly ConcurrentDictionary<string, Lazy<PageStackNavController>> _managers = new();

    public PageStackNavController Create(string name)
        => _managers.GetOrAdd(name, _ => new Lazy<PageStackNavController>(() => new PageStackNavController())).Value;
}