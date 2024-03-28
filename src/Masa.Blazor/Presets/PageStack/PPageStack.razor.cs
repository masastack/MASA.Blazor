using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageStack: PatternPathComponentBase
{
    enum PageType
    {
        Tab,
        Stack
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [EditorRequired] public IEnumerable<string> TabbedPatterns { get; set; } = Array.Empty<string>();

    internal readonly List<StackPatternPath> Pages = new();

    private bool _locationChangedByUserClick;

    private string? _previousPath;
    private PageType _pageTypeOfPreviousPath;

    private string? _latestTabPath;

    private HashSet<string> _prevTabbedPatterns = new();
    private HashSet<Regex> _cachedTabbedPatterns = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        UpdateRegexes();

        var patternPath = GetCurrentStackPatternPath();
        if (!IsTabbedPattern(patternPath.AbsolutePath))
        {
            Pages.Add(patternPath);
            DisableRootScrollbar(true);
        }
        else
        {
            _latestTabPath = patternPath.AbsolutePath;
        }

        _previousPath = patternPath.AbsolutePath;

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UpdateRegexes();
    }

    private void UpdateRegexes()
    {
        if (_prevTabbedPatterns.SetEquals(TabbedPatterns)) return;

        _prevTabbedPatterns = new HashSet<string>(TabbedPatterns);
        _cachedTabbedPatterns = TabbedPatterns
            .Select(p => new Regex(p, RegexOptions.IgnoreCase)).ToHashSet();
    }

    private bool IsTabbedPattern(string absolutePath)
    {
        return _cachedTabbedPatterns.Any(r => r.IsMatch(absolutePath));
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        // only two cases should be handled here:
        // 1. user click on the browser back/forward button
        // 2. user click on the anchor tag with href attribute

        if (_locationChangedByUserClick)
        {
            _locationChangedByUserClick = false;
            return;
        }

        var currentPath = NavigationManager.GetAbsolutePath();
        var isTabbedPath = IsTabbedPattern(currentPath);

        // if the current path is a tabbed path
        if (isTabbedPath)
        {
            if (_pageTypeOfPreviousPath == PageType.Tab)
            {
                _latestTabPath = currentPath;
            }
            else if (_pageTypeOfPreviousPath == PageType.Stack)
            {
                if (Pages.Count == 1)
                {
                    // has an animation
                    CloseTopPageOfStack();
                }
                else
                {
                    // no animation
                    ClearStack();
                }

                _previousPath = currentPath;
                _pageTypeOfPreviousPath = PageType.Tab;
                InvokeAsync(StateHasChanged);
            }

            return;
        }

        // if the current path is a stack path

        var currentPatternPath = GetCurrentStackPatternPath();

        if (!currentPatternPath.IsSelf && _pageTypeOfPreviousPath == PageType.Stack)
        {
            var previous2 = Pages.ElementAtOrDefault(Pages.Count - 2);
            if (previous2 is not null && currentPatternPath.Pattern == previous2.Pattern)
            {
                CloseTopPageOfStack();
                _previousPath = currentPath;
                _pageTypeOfPreviousPath = PageType.Stack;
                InvokeAsync(StateHasChanged);
                return;
            }
        }

        if (currentPatternPath.IsSelf)
        {
            var renderedPatternPath = Pages.FirstOrDefault(p => p.Pattern == currentPatternPath.Pattern);
            if (renderedPatternPath is not null)
            {
                renderedPatternPath.UpdatePath(currentPath);
                InvokeAsync(StateHasChanged);
            }
        }

        _previousPath = currentPath;
        _pageTypeOfPreviousPath = isTabbedPath ? PageType.Tab : PageType.Stack;
        Pages.Add(currentPatternPath);
        DisableRootScrollbar(true);

        InvokeAsync(StateHasChanged);
    }

    private void HandleOnPrevious()
    {
        _locationChangedByUserClick = true;

        Js.InvokeVoidAsync(JsInteropConstants.HistoryBack);

        CloseTopPageOfStack();
    }

    [MasaApiPublicMethod]
    public void GoBack()
    {
        HandleOnPrevious();
    }

    private void CloseTopPageOfStack()
    {
        if (Pages.Count == 0)
        {
            return;
        }

        var current = Pages.Last();
        current.Active = false;

        Task.Run(async () =>
        {
            await Task.Delay(300); // wait for the transition to complete
            Pages.RemoveAt(Pages.Count - 1);

            if (Pages.Count == 0)
            {
                DisableRootScrollbar(false);
            }

            _previousPath = Pages.LastOrDefault()?.AbsolutePath;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    private void ClearStack()
    {
        Pages.Clear();
        DisableRootScrollbar(false);
        _ = InvokeAsync(StateHasChanged);
    }

    private StackPatternPath GetCurrentStackPatternPath()
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        var selfPatternRegex = CachedSelfPatternRegexes.FirstOrDefault(r => r.IsMatch(absolutePath));
        return selfPatternRegex is null ? new StackPatternPath(absolutePath) : new StackPatternPath(selfPatternRegex.ToString(), absolutePath);
    }

    private void DisableRootScrollbar(bool disable)
    {
        _ = Js.InvokeVoidAsync(
            disable ? JsInteropConstants.AddCls : JsInteropConstants.RemoveCls,
            "html",
            "overflow-y-hidden");
    }

    protected override ValueTask DisposeAsyncCore()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;

        return base.DisposeAsyncCore();
    }

}