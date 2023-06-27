using Microsoft.AspNetCore.Components.Routing;

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
    [Parameter]
    public IEnumerable<string>? ExcludedPatterns { get; set; }

    private string? _previousPath;
    private PatternPath? _currentPatternPath;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var patternPath = GetCurrentPatternPath();
        _currentPatternPath = patternPath;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var patternPath = GetCurrentPatternPath();
        if (!PatternPaths.Contains(patternPath))
        {
            PatternPaths.Add(patternPath);
        }

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
            PatternPaths.Remove(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabReload(object? sender, PatternPath e)
    {
        InvokeAsync(() =>
        {
            PatternPaths.Remove(e);
            StateHasChanged();
            PatternPaths.Add(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabsUpdated(object? sender, PatternPath[] paths)
    {
        InvokeAsync(() =>
        {
            if (paths.Length == 0)
            {
                PatternPaths.Clear();
            }
            else
            {
                PatternPaths.RemoveAll(p => !paths.Contains(p));
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

        // if the previous path is excluded, remove it from the PatternPaths
        if (_previousPath is not null && ExcludedPatterns is not null)
        {
            if (ExcludedPatterns.Any(pattern => new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(_previousPath)))
            {
                var previousPatternPath = PatternPaths.FirstOrDefault(p => p.AbsolutePath == _previousPath);
                if (previousPatternPath is not null)
                {
                    PatternPaths.Remove(previousPatternPath);
                    InvokeAsync(StateHasChanged);
                }
            }
        }

        _previousPath = currentPath;

        if (PatternPaths.Contains(_currentPatternPath!))
        {
            return;
        }

        if (currentPatternPath.IsSelf)
        {
            var renderedAbsolutePath = PatternPaths.FirstOrDefault(p => currentPatternPath == p);
            if (renderedAbsolutePath is not null)
            {
                InvokeAsync(StateHasChanged);
                return;
            }
            else
            {
                PatternPaths.Add(currentPatternPath);
                InvokeAsync(StateHasChanged);
                return;
            }
        }

        InvokeAsync(() =>
        {
            PatternPaths.Add(_currentPatternPath!);
            StateHasChanged();
        });
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
