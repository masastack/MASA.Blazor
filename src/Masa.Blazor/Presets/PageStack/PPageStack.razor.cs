using Masa.Blazor.Presets.PageStack;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageStack : PatternPathComponentBase
{
    [Inject] private IStackNavigationManagerFactory StackNavigationManagerFactory { get; set; } = null!;

    enum PageType
    {
        Tab,
        Stack
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Name { get; set; }

    [Parameter] [EditorRequired] public IEnumerable<string> TabbedPatterns { get; set; } = Array.Empty<string>();

    internal StackNavigationManager InternalStackNavManager { get; private set; }

    internal readonly List<StackPageData> Pages = new();

    private bool _locationChangedByUserClick;

    private string? _previousPath;
    private StackPageData? _lastPage;
    private PageType _pageTypeOfPreviousPath;

    private string? _latestTabPath;

    private HashSet<string> _prevTabbedPatterns = new();
    private HashSet<Regex> _cachedTabbedPatterns = new();

    private List<TabbedPage> _tabbedPaths = new();
    private TabbedPage _lastActiveTabbedPage;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateRegexes();

        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(u => u.IsMatch(NavigationManager.GetAbsolutePath()));
        if (tabbedPattern is not null)
        {
            TabbedPage tabbedPage = new() { Pattern = tabbedPattern.ToString(), CreatedAt = DateTime.Now };
            _tabbedPaths.Add(tabbedPage);
            _lastActiveTabbedPage = tabbedPage;
            _pageTypeOfPreviousPath = PageType.Stack;
        }

        InternalStackNavManager = StackNavigationManagerFactory.Create(Name ?? string.Empty);
        InternalStackNavManager.PagePushed += InternalStackNavManagerOnPagePushed;
        InternalStackNavManager.PagePopped += InternalStackNavManagerOnPagePopped;
        InternalStackNavManager.PageReplaced += InternalStackNavManagerOnPageReplaced;
        InternalStackNavManager.LocationChanged += InternalStackNavManagerOnLocationChanged;
    }

    private void InternalStackNavManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        Console.Out.WriteLine("[PPageStack] InternalStackNavManagerOnLocationChanged: " + e.Location);

        var targetPath = new Uri(e.Location).AbsolutePath;
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(r => r.IsMatch(targetPath));

        if (tabbedPattern is not null)
        {
            if (_pageTypeOfPreviousPath == PageType.Tab)
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
            }

            TabbedPage tabbedPage = new() { Pattern = tabbedPattern.ToString(), CreatedAt = DateTime.Now };
            _tabbedPaths.Add(tabbedPage);
            _lastActiveTabbedPage = tabbedPage;
            _pageTypeOfPreviousPath = PageType.Stack;
            InvokeAsync(StateHasChanged);
        }
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

    private readonly Block _block = new("p-page-container");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "p-page-container";
    }

    private void InternalStackNavManagerOnPageReplaced(object? sender, StackChangedEventArgs e)
    {
        _locationChangedByUserClick = true;
        _pageTypeOfPreviousPath = PageType.Stack;

        // TODO
    }

    private void InternalStackNavManagerOnPagePopped(object? sender, StackChangedEventArgs e)
    {
        _locationChangedByUserClick = true;
        _pageTypeOfPreviousPath = PageType.Stack;
        
        CloseTopPageOfStack();
    }

    private void InternalStackNavManagerOnPagePushed(object? sender, StackChangedEventArgs e)
    {
        _locationChangedByUserClick = true;
        _pageTypeOfPreviousPath = PageType.Stack;

        Console.Out.WriteLine($"[PPageStack] InternalStackNavManagerOnPagePushed: {e.Uri} {NavigationManager.Uri}");
        Pages.Add(new StackPageData(e.Uri));
        InvokeAsync(StateHasChanged);
    }

    private bool IsTabbedPattern(string absolutePath)
    {
        return _cachedTabbedPatterns.Any(r => r.IsMatch(absolutePath));
    }

    // private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    // {
    //     // only two cases should be handled here:
    //     // 1. user click on the browser back/forward button
    //     // 2. user click on the anchor tag with href attribute
    //
    //     if (_locationChangedByUserClick)
    //     {
    //         _locationChangedByUserClick = false;
    //         return;
    //     }
    //
    //     var currentPath = NavigationManager.GetAbsolutePath();
    //     var isTabbedPath = IsTabbedPattern(currentPath);
    //
    //     // if the current path is a tabbed path
    //     if (isTabbedPath)
    //     {
    //         if (_pageTypeOfPreviousPath == PageType.Tab)
    //         {
    //             _latestTabPath = currentPath;
    //         }
    //         else if (_pageTypeOfPreviousPath == PageType.Stack)
    //         {
    //             if (Pages.Count == 1)
    //             {
    //                 // has an animation
    //                 CloseTopPageOfStack();
    //             }
    //             else
    //             {
    //                 // no animation
    //                 ClearStack();
    //             }
    //
    //             _previousPath = currentPath;
    //             _pageTypeOfPreviousPath = PageType.Tab;
    //             InvokeAsync(StateHasChanged);
    //         }
    //
    //         return;
    //     }
    //
    //     // if the current path is a stack path
    //     var targetPage = GetCurrentPageData();
    //
    //     // maybe self page
    //     if (_lastPage?.Pattern == targetPage.Pattern)
    //     {
    //         if (targetPage.IsSelf)
    //         {
    //             var result = Pages.FirstOrDefault(u => u.Pattern == targetPage.Pattern);
    //             result?.UpdatePath(currentPath);
    //             // InvokeAsync(StateHasChanged);
    //             // return;
    //         }
    //     }
    //     else
    //     {
    //         var isGoBack = false;
    //
    //         if (Pages.Count > 1)
    //         {
    //             var secondToLastPage = Pages.ElementAt(Pages.Count - 2);
    //             if (secondToLastPage.Pattern == targetPage.Pattern)
    //             {
    //                 if (secondToLastPage.AbsolutePath == targetPage.AbsolutePath)
    //                 {
    //                     isGoBack = true;
    //                 }
    //             }
    //         }
    //
    //         if (isGoBack)
    //         {
    //             CloseTopPageOfStack();
    //         }
    //         else
    //         {
    //             Pages.Add(targetPage);
    //             if (Pages.Count == 1)
    //             {
    //                 DisableRootScrollbar(true);
    //             }
    //         }
    //     }
    //
    //     _previousPath = currentPath;
    //     _lastPage = targetPage;
    //     _pageTypeOfPreviousPath = isTabbedPath ? PageType.Tab : PageType.Stack;
    //
    //     InvokeAsync(StateHasChanged);
    // }

    private void HandleOnPrevious()
    {
        _locationChangedByUserClick = true;

        _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryBack);

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
        current.Stacked = false;

        Task.Run(async () =>
        {
            await Task.Delay(300); // wait for the transition to complete
            Pages.RemoveAt(Pages.Count - 1);

            if (Pages.Count == 0)
            {
                DisableRootScrollbar(false);
            }

            _lastPage = Pages.LastOrDefault();
            _previousPath = _lastPage?.AbsolutePath;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    private void ClearStack()
    {
        Pages.Clear();
        DisableRootScrollbar(false);
        _ = InvokeAsync(StateHasChanged);
    }

    private StackPageData GetCurrentPageData()
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        var selfPatternRegex = CachedSelfPatternRegexes.FirstOrDefault(r => r.IsMatch(absolutePath));
        return selfPatternRegex is null
            ? new StackPageData(absolutePath)
            : new StackPageData(selfPatternRegex.ToString(), absolutePath);
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
        // NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;

        return base.DisposeAsyncCore();
    }
}