using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets.PageStack;

public partial class PPageStackContainer : MasaComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public HashSet<TabRule> TabRules { get; set; } = [];

    [Parameter] public EventCallback<TabRecord> OnActiveTabUpdate { get; set; }

    private static readonly Block _block = new("p-page-container");

    private readonly HashSet<TabRecord> _tabs = [];
    private string? _lastVisitedPage;
    private TabRecord? _lastVisitedTab;
    private int _lastTabId = 1;
    private string? currentPath;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_lastVisitedPage is null)
        {
            currentPath = NavigationManager.GetAbsolutePath();

            var matched = TabRules.FirstOrDefault(r => r.Regex.IsMatch(currentPath));
            if (matched is not null)
            {
                _lastVisitedTab = new TabRecord(matched, currentPath)
                {
                    Id = _lastTabId++
                };
                _tabs.Add(_lastVisitedTab);
                _lastVisitedPage = _lastVisitedTab.AbsolutePath;
                _ = OnActiveTabUpdate.InvokeAsync(_lastVisitedTab).ConfigureAwait(false);
            }
        }
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _block.Name;
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentPath = NavigationManager.GetAbsolutePath();

        // avoid multiple clicks on the same tab
        if (_lastVisitedPage == currentPath)
        {
            return;
        }

        var matched = TabRules.FirstOrDefault(r => r.Regex.IsMatch(currentPath));
        // do nothing if not a tab page
        if (matched is null)
        {
            return;
        }

        // if the current page is not persistent, remove it from the persistent tabs
        if (_lastVisitedTab?.Rule.Persistent is false)
        {
            _tabs.Remove(_lastVisitedTab);
        }

        _lastVisitedPage = currentPath;

        var exists = false;

        if (matched.Self)
        {
            var p = _tabs.FirstOrDefault(u => u.Rule.Regex == matched.Regex);
            if (p is not null)
            {
                exists = true;

                _lastVisitedTab = p with { AbsolutePath = currentPath };
                _tabs.Remove(p);
                _tabs.Add(_lastVisitedTab);
                OnActiveTabUpdate.InvokeAsync(_lastVisitedTab);
            }
        }

        if (!exists)
        {
            _lastVisitedTab = _tabs.FirstOrDefault(u => u.Rule == matched)
                              ?? new TabRecord(matched, currentPath)
                              {
                                  Id = _lastTabId++
                              };

            _tabs.Add(_lastVisitedTab);
            OnActiveTabUpdate.InvokeAsync(_lastVisitedTab);
        }

        InvokeAsync(StateHasChanged);
    }

    protected override ValueTask DisposeAsyncCore()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
        return base.DisposeAsyncCore();
    }
}