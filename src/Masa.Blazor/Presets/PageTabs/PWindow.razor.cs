#nullable enable
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PWindow : IAsyncDisposable
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [CascadingParameter]
    private PPageTabs? ContainerPageTabs { get; set; }

    [Parameter]
    public PPageTabs? PageTabs { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    // TODO: rename: self patterns
    [Parameter]
    public IEnumerable<string>? SelfPaths { get; set; }

    private readonly List<PathPattern> _absolutePaths = new();

    private string? _currentAbsolutePath;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        var (_, path) = TryGetMatchedPattern();
        _currentAbsolutePath = path;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var (matched, pattern) = TryGetMatchedPattern();
        var pathPattern = new PathPattern(pattern, matched);
        if (!_absolutePaths.Contains(pathPattern))
        {
            _absolutePaths.Add(pathPattern);
        }

        if (ContainerPageTabs != null)
        {
            ContainerPageTabs.TabClosed += PageTabsOnTabClosed;
            ContainerPageTabs.TabReload += PageTabsOnTabReload;
            ContainerPageTabs.TabsUpdated += PageTabsOnTabsUpdated;
        }

        NavigationManager!.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (PageTabs != null)
            {
                PageTabs.TabClosed += PageTabsOnTabClosed;
                PageTabs.TabReload += PageTabsOnTabReload;
                PageTabs.TabsUpdated += PageTabsOnTabsUpdated;
            }
        }
    }

    private void PageTabsOnTabClosed(object? sender, string e)
    {
        InvokeAsync(() =>
        {
            _absolutePaths.Remove(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabReload(object? sender, string e)
    {
        InvokeAsync(() =>
        {
            _absolutePaths.Remove(e);
            StateHasChanged();
            _absolutePaths.Add(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabsUpdated(object? sender, string[] paths)
    {
        InvokeAsync(() =>
        {
            _absolutePaths.RemoveAll(p => !paths.Contains(p));
            StateHasChanged();
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (_absolutePaths.Contains(_currentAbsolutePath!))
        {
            return;
        }

        var regexSelfPaths = FormatSelfPaths();
        var selfPath = regexSelfPaths!.FirstOrDefault(r => r.IsMatch(_currentAbsolutePath!));
        if (selfPath is not null)
        {
            var renderedAbsolutePath = _absolutePaths.FirstOrDefault(p => selfPath.IsMatch(p));
            if (renderedAbsolutePath is not null)
            {
                var index = _absolutePaths.IndexOf(renderedAbsolutePath);
                _absolutePaths[index] = selfPath.ToString();
                InvokeAsync(StateHasChanged);
                return;
            }
            else
            {
                _absolutePaths.Add(selfPath.ToString());
                InvokeAsync(StateHasChanged);
                return;
            }
        }

        InvokeAsync(() =>
        {
            _absolutePaths.Add(_currentAbsolutePath!);
            StateHasChanged();
        });
    }

    private List<Regex> FormatSelfPaths()
    {
        return SelfPaths is null
            ? Enumerable.Empty<Regex>().ToList()
            : SelfPaths.Select(p => new Regex(p)).ToList();
    }

    private(bool matched, string value) TryGetMatchedPattern()
    {
        var absolutePath = NavigationManager!.GetAbsolutePath();
        var regexSelfPaths = FormatSelfPaths();
        var regexSelfPath = regexSelfPaths.FirstOrDefault(r => r.IsMatch(absolutePath));
        return regexSelfPath is null ? (false, absolutePath) : (true, regexSelfPath.ToString());
    }

    public async ValueTask DisposeAsync()
    {
        if (PageTabs != null)
        {
            PageTabs.TabClosed -= PageTabsOnTabClosed;
            PageTabs.TabReload -= PageTabsOnTabReload;
            PageTabs.TabsUpdated -= PageTabsOnTabsUpdated;
        }

        if (ContainerPageTabs != null)
        {
            ContainerPageTabs.TabClosed -= PageTabsOnTabClosed;
            ContainerPageTabs.TabReload -= PageTabsOnTabReload;
            ContainerPageTabs.TabsUpdated -= PageTabsOnTabsUpdated;
        }

        NavigationManager!.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
