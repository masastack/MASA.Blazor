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

    internal readonly StackPages Pages = new();

    private int _locationChangedByUserClick;

    private PageType _pageTypeOfPreviousPath;

    private string? _latestTabPath;

    private HashSet<string> _prevTabbedPatterns = new();
    private HashSet<Regex> _cachedTabbedPatterns = new();

    private List<TabbedPage> _tabbedPaths = new();
    private TabbedPage? _lastActiveTabbedPage;

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
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        Pages.Push(NavigationManager.ToAbsoluteUri(href).AbsolutePath);

        InvokeAsync(StateHasChanged);
    }

    private void InternalStackNavManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
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

        Pages.UpdateTop(e.Uri, e.State);
    }

    private void InternalStackNavManagerOnPagePopped(object? sender, StackChangedEventArgs e)
    {
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        CloseTopPageOfStack(e.State);
    }

    private void InternalStackNavManagerOnPagePushed(object? sender, StackChangedEventArgs e)
    {
        if (_cachedTabbedPatterns.Any(p => p.IsMatch(e.Uri)))
        {
            return;
        }

        Push(e.Uri);
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
        if (Pages.TryPeek(out var current))
        {
            current.Stacked = false;

            if (Pages.TryPeekSecondToLast(out var secondToLast))
            {
                secondToLast.UpdateState(state);
                secondToLast.Activate();
            }

            StateHasChanged();

            Task.Run(async () =>
            {
                await Task.Delay(300); // wait for the transition to complete
                Pages.Pop();

                if (Pages.Count == 0)
                {
                    DisableRootScrollbar(false);
                }

                _ = InvokeAsync(StateHasChanged);
            });
        }
    }

    private void ClearStack()
    {
        Pages.Clear();
        DisableRootScrollbar(false);
        _ = InvokeAsync(StateHasChanged);
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