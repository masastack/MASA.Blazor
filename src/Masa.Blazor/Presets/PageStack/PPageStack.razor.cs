using Masa.Blazor.Presets.PageStack;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageStack : PatternPathComponentBase
{
    [Inject] private IStackNavigationManagerFactory StackNavigationManagerFactory { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

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

    private int _locationChangedByUserClick;

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
            _pageTypeOfPreviousPath = PageType.Tab;
        }

        InternalStackNavManager = StackNavigationManagerFactory.Create(Name ?? string.Empty);
        InternalStackNavManager.PagePushed += InternalStackNavManagerOnPagePushed;
        InternalStackNavManager.PagePopped += InternalStackNavManagerOnPagePopped;
        InternalStackNavManager.PageReplaced += InternalStackNavManagerOnPageReplaced;
        // InternalStackNavManager.LocationChanged += InternalStackNavManagerOnLocationChanged;
        NavigationManager.LocationChanged += InternalStackNavManagerOnLocationChanged;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    private IJSObjectReference? module;
    private DotNetObjectReference<PPageStack>? _dotNetObjectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            module = await Js.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor/Presets/PageStack/PPageStack.razor.js");
            await module.InvokeVoidAsync("attachListener", _dotNetObjectReference);
        }
    }

    [JSInvokable]
    public void Push(string href)
    {
        Console.Out.WriteLine(
            $"[PPageStack] JSInvokable Push: href={href} NavigationManager.uri={NavigationManager.Uri}");

        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        Pages.Add(new StackPageData(href));

        InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public async Task Pop()
    {
        Console.Out.WriteLine("[PPageStack] JSInvokable Pop: ");
    }

    [JSInvokable]
    public async Task Replace(string href)
    {
        // Pages.RemoveAt(Pages.Count - 1);
        // Pages.Add(new StackPageData(href));

        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        var topPage = Pages.LastOrDefault();
        if (topPage is null)
        {
            throw new InvalidOperationException("The page stack is empty.");
        }

        topPage.UpdatePath(href);

        await Js.InvokeVoidAsync(JsInteropConstants.HistoryReplace, href);
        InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public async Task PopAndReplace(string href)
    {
        // var targetPage = Pages.ElementAtOrDefault(Pages.Count - 2);
        // targetPage?.UpdatePath(href);
        // CloseTopPageOfStack();

        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryBack);

        await InvokeAsync(StateHasChanged);

        var targetPage = Pages.ElementAtOrDefault(Pages.Count - 2);
        targetPage?.UpdatePath(href);

        CloseTopPageOfStack();

        // NavigationManager.Replace(href);
        Console.Out.WriteLine("[PPageStack] JSInvokable PopAndReplace: " + href);

        await Task.Yield();
        // Console.Out.WriteLine("[PPageStack] JSInvokable PopAndReplace2: " + NavigationManager.Uri);
        NextTick(async () =>
        {
            await Task.Delay(16);
            InvokeAsync(() =>
            {
                _locationChangedByUserClick++;
                NavigationManager.Replace(href);
                Console.Out.WriteLine("[PPageStack] !!!!!!!!!!!!! Replace href: " + href);
            });
        });

        await InvokeAsync(StateHasChanged);
    }

    private void InternalStackNavManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        Console.Out.WriteLine("[PPageStack] InternalStackNavManagerOnLocationChanged: " + e.Location);

        var targetPath = new Uri(e.Location).AbsolutePath;
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(r => r.IsMatch(targetPath));

        if (tabbedPattern is not null)
        {
            if (_pageTypeOfPreviousPath == PageType.Stack)
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

            var tabbedPage = _tabbedPaths.FirstOrDefault(u => u.Pattern == tabbedPattern.ToString());
            if (tabbedPage is null)
            {
                tabbedPage = new() { Pattern = tabbedPattern.ToString(), CreatedAt = DateTime.Now };
                _tabbedPaths.Add(tabbedPage);
            }

            _lastActiveTabbedPage = tabbedPage;
            _pageTypeOfPreviousPath = PageType.Tab;
            InvokeAsync(StateHasChanged);
            return;
        }

        if (_pageTypeOfPreviousPath == PageType.Stack)
        {
            needCheck = true;
        }
    }

    private bool needCheck;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (needCheck)
        {
            needCheck = false;
            
            if (_locationChangedByUserClick-- > 0)
            {
                _locationChangedByUserClick = 0;
                return;
            }

            CloseTopPageOfStack();
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
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        // TODO
    }

    private void InternalStackNavManagerOnPagePopped(object? sender, StackChangedEventArgs e)
    {
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        CloseTopPageOfStack(e.State);
    }

    private void InternalStackNavManagerOnPagePushed(object? sender, StackChangedEventArgs e)
    {
        Push(e.Uri);
    }

    private bool IsTabbedPattern(string absolutePath)
    {
        return _cachedTabbedPatterns.Any(r => r.IsMatch(absolutePath));
    }

    private void HandleOnPrevious()
    {
        _locationChangedByUserClick++;

        _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryBack);

        CloseTopPageOfStack();
    }

    [MasaApiPublicMethod]
    public void GoBack()
    {
        HandleOnPrevious();
    }

    private void CloseTopPageOfStack(object? state = null)
    {
        if (Pages.Count == 0)
        {
            return;
        }

        var current = Pages.Last();
        current.Stacked = false;
        StateHasChanged();

        Task.Run(async () =>
        {
            await Task.Delay(300); // wait for the transition to complete
            Pages.RemoveAt(Pages.Count - 1);

            if (Pages.Count == 0)
            {
                DisableRootScrollbar(false);
            }

            _lastPage = Pages.LastOrDefault();
            _lastPage?.UpdateState(state);
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
        NavigationManager.LocationChanged -= InternalStackNavManagerOnLocationChanged;

        return base.DisposeAsyncCore();
    }
}