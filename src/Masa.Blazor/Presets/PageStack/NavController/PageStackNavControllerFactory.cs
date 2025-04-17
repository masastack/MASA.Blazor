using System.Collections.Concurrent;

namespace Masa.Blazor.Presets.PageStack.NavController;

public class PageStackNavControllerFactory : IPageStackNavControllerFactory
{
    private readonly ConcurrentDictionary<string, Lazy<PageStackNavController>> _controllers = new();

    private List<string> _activeControllers = [];

    public string? Current { get; private set; }

    public void Activate(string name)
    {
        if (_activeControllers.Contains(name))
        {
            return;
        }

        _activeControllers.Add(name);

        Current = name;
    }

    public PageStackNavController Create(string name)
        => _controllers.GetOrAdd(name, _ => new Lazy<PageStackNavController>(() => new PageStackNavController(name))).Value;

    public void Inactivate(string name)
    {
        if (_activeControllers.Contains(name))
        {
            _activeControllers.Remove(name);
        }

        Current = _activeControllers.LastOrDefault();
    }
}