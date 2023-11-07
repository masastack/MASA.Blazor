using Microsoft.AspNetCore.Components.Routing;
using Masa.Blazor.Presets.PageContainer;

namespace Masa.Blazor.Presets;

public partial class PPageContainer : PatternPathComponentBase
{
    [CascadingParameter]
    private PPageTabs? ContainerPageTabs { get; set; }

    [Parameter]
    public PPageTabs? PageTabs { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// A list of regular expression patterns to match.
    /// The content of the matched path would not be cached in the DOM.
    /// </summary>
    [Parameter] [Obsolete("Use ExcludePatterns instead")]
    public IEnumerable<string>? ExcludedPatterns { get; set; }

    /// <summary>
    /// A list of regular expression patterns to match.
    /// The content of the matched path would not be cached in the DOM.
    /// </summary>
    [Parameter]
    public IEnumerable<string>? ExcludePatterns { get; set; }

    /// <summary>
    /// A list of regular expression patterns to match.
    /// The content of the matched path would be cached in the DOM.
    /// </summary>
    [Parameter]
    [MassApiParameter(ReleasedOn = "v1.0.2")]
    public IEnumerable<string>? IncludePatterns { get; set; }

    /// <summary>
    /// Max number of cached pages.
    /// </summary>
    [Parameter] [MassApiParameter(10, "v1.0.2")]
    public int Max { get; set; } = 10;

    private readonly LRUCache<string, PatternPath> _patternPaths = new(10);

    private string? _previousPath;
    private PatternPath? _currentPatternPath;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ExcludePatterns ??= ExcludedPatterns;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var patternPath = GetCurrentPatternPath();
        _currentPatternPath = patternPath;
        _patternPaths.Put(patternPath.Pattern, patternPath);

        if (ContainerPageTabs != null)
        {
            ContainerPageTabs.TabClosed += PageTabsOnTabClosed;
            ContainerPageTabs.TabReload += PageTabsOnTabReload;
            ContainerPageTabs.TabsUpdated += PageTabsOnTabsUpdated;
        }

        _previousPath = patternPath.AbsolutePath;

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(css => { css.Add("p-page-container"); }, style => { style.Add("height: 100%"); });
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (PageTabs == null) return;

            PageTabs.TabClosed += PageTabsOnTabClosed;
            PageTabs.TabReload += PageTabsOnTabReload;
            PageTabs.TabsUpdated += PageTabsOnTabsUpdated;
        }
    }

    private void PageTabsOnTabClosed(object? sender, PatternPath e)
    {
        InvokeAsync(() =>
        {
            _patternPaths.Remove(e.Pattern);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabReload(object? sender, PatternPath e)
    {
        InvokeAsync(() =>
        {
            _patternPaths.Remove(e.Pattern);
            StateHasChanged();
            _patternPaths.Put(e.Pattern, e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabsUpdated(object? sender, PatternPath[] paths)
    {
        InvokeAsync(() =>
        {
            if (paths.Length == 0)
            {
                _patternPaths.Clear();
            }
            else
            {
                _patternPaths.RemoveAll(_patternPaths.Keys.Except(paths.Select(p => p.Pattern)));
            }

            StateHasChanged();
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var currentPatternPath = GetCurrentPatternPath();
        var currentPath = currentPatternPath.AbsolutePath;

        // only the path is changed, not the query string
        if (_previousPath == currentPath)
        {
            return;
        }

        if (currentPatternPath.IsSelf)
        {
            if (_patternPaths.TryGetValue(currentPath, out var renderedPathPattern))
            {
                renderedPathPattern.UpdatePath(NavigationManager.GetAbsolutePath());
                InvokeAsync(StateHasChanged);
            }
        }

        // if the previous path is excluded or not included, remove it from the PatternPaths
        if (_previousPath is not null)
        {
            if ((ExcludePatterns?.Any() is true &&
                 ExcludePatterns.Any(pattern => new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(_previousPath))) ||
                (IncludePatterns?.Any() is true &&
                 !IncludePatterns.Any(pattern => new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(_previousPath))))
            {
                var previousPatternPath = _patternPaths.FirstOrDefault(p => p.AbsolutePath == _previousPath);
                if (previousPatternPath is not null)
                {
                    _patternPaths.Remove(previousPatternPath.Pattern);
                    InvokeAsync(StateHasChanged);
                }
            }
        }

        _previousPath = currentPath;

        _currentPatternPath = currentPatternPath;

        _patternPaths.PutOrGet(currentPatternPath.Pattern, currentPatternPath);

        InvokeAsync(StateHasChanged);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

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

        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
