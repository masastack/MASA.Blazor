using Masa.Blazor.Presets.PageStack;
using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageStack : PatternPathComponentBase
{
    [Inject] private IPageStackNavControllerFactory PageStackNavControllerFactory { get; set; } = null!;

    enum PageType
    {
        Tab,
        Stack
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Name { get; set; }

    [Parameter] [EditorRequired] public IEnumerable<string> TabbedPatterns { get; set; } = Array.Empty<string>();

    [Parameter]
    [MasaApiParameter(defaultValue: DefaultFallbackUri)]
    public string? FallbackUri { get; set; } = DefaultFallbackUri;

    private PageStackNavController? InternalPageStackNavManager { get; set; }

    private const int DelayForPageClosingAnimation = 300;
    private const string DefaultFallbackUri = "/";

    internal readonly StackPages Pages = new();

    private int _navCountByUserClick;
    private string? _lastVisitedTabPath;
    private bool _taskForBrowserClickBack;
    private PageType _targetPageType;
    private string? _latestTabPath;

    private HashSet<string> _prevTabbedPatterns = new();
    private HashSet<Regex> _cachedTabbedPatterns = new();

    private IJSObjectReference? _module;
    private DotNetObjectReference<PPageStack>? _dotNetObjectReference;
    private int _dotnetObjectId;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateRegexes();

        var targetPath = NavigationManager.GetAbsolutePath();
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(u => u.IsMatch(targetPath));
        if (tabbedPattern is not null)
        {
            _lastVisitedTabPath = targetPath;
            _targetPageType = PageType.Tab;
        }
        else
        {
            _targetPageType = PageType.Stack;
            Push(NavigationManager.Uri);
        }

        InternalPageStackNavManager = PageStackNavControllerFactory.Create(Name ?? string.Empty);
        InternalPageStackNavManager.PagePushed += InternalPageStackNavManagerOnPagePushed;
        InternalPageStackNavManager.PagePopped += InternalPageStackNavManagerOnPagePopped;
        InternalPageStackNavManager.PageReplaced += InternalPageStackNavManagerOnPageReplaced;
        InternalPageStackNavManager.PageCleared += InternalPageStackNavManagerOnPageCleared;
        InternalPageStackNavManager.LocationChanged += InternalPageStackNavManagerOnLocationChanged;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _module = await Js.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor/Presets/PageStack/PPageStack.razor.js");
            _dotnetObjectId = await _module.InvokeAsync<int>("attachListener", _dotNetObjectReference);
        }
    }

    [JSInvokable]
    public void Push(string href)
    {
        _navCountByUserClick++;

        Pages.Push(GetAbsolutePath(href));
        DisableRootScrollbar(true);

        InvokeAsync(StateHasChanged);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        // 不能放在 NavigationManager.LocationChanged 事件中，因为 a 标签的点击事件会比 <see cref="Push"/> 先触发
        if (_taskForBrowserClickBack)
        {
            _taskForBrowserClickBack = false;

            if (_navCountByUserClick > 0)
            {
                _navCountByUserClick = 0;
                return;
            }

            if (Pages.Count > 0)
            {
                CloseTopPageOfStack();
            }
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

    private void InternalPageStackNavManagerOnPageReplaced(object? sender, PageStackReplacedEventArgs e)
    {
        _navCountByUserClick++;

        Pages.UpdateTop(e.Uri, e.State);
    }

    private void InternalPageStackNavManagerOnPagePopped(object? sender, PageStackPoppedEventArgs e)
    {
        _navCountByUserClick++;

        CloseTopPages(e.Delta, e.State);
    }

    private void InternalPageStackNavManagerOnPagePushed(object? sender, PageStackPushedEventArgs e)
    {
        Push(e.Uri);
    }

    private async void InternalPageStackNavManagerOnPageCleared(object? sender, PageStackPushedEventArgs e)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -Pages.Count);

        var backToExistingPage = _lastVisitedTabPath == GetAbsolutePath(e.Uri);

        if (backToExistingPage)
        {
            CloseTopPages(Pages.Count);
        }
        else
        {
            Pages.Clear();
            DisableRootScrollbar(false);
            _ = InvokeAsync(StateHasChanged);
        }

        NextTick(async () =>
        {
            await Task.Delay(backToExistingPage ? DelayForPageClosingAnimation : 0);

            NavigationManager.Replace(e.Uri);
        });
    }

    private void InternalPageStackNavManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _taskForBrowserClickBack = true;

        var targetPath = new Uri(e.Location).AbsolutePath;
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(r => r.IsMatch(targetPath));

        if (tabbedPattern is not null)
        {
            _lastVisitedTabPath = targetPath;
            _targetPageType = PageType.Tab;
            DisableRootScrollbar(false);
            InvokeAsync(StateHasChanged);
        }
        else
        {
            _targetPageType = PageType.Stack;
        }
    }

    private string GetAbsolutePath(string relativeUri) => NavigationManager.ToAbsoluteUri(relativeUri).AbsolutePath;

    private void HandleOnPrevious()
    {
        _navCountByUserClick++;

        if (_lastVisitedTabPath is not null)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryBack);
        }
        else
        {
            NavigationManager.NavigateTo(FallbackUri ?? DefaultFallbackUri);
        }

        CloseTopPageOfStack();
    }

    [MasaApiPublicMethod]
    public void GoBack()
    {
        HandleOnPrevious();
    }

    private void CloseTopPageOfStack(object? state = null) => CloseTopPages(1, state);

    private void CloseTopPages(int count, object? state = null)
    {
        if (!Pages.TryPeek(out var current)) return;

        current.Stacked = false;

        if (count > 1)
        {
            Pages.RemoveRange(Pages.Count - count, count - 1);
        }

        if (Pages.TryPeekSecondToLast(out var target))
        {
            target.UpdateState(state);
            target.Activate();
        }

        StateHasChanged();

        Task.Run(async () =>
        {
            await Task.Delay(DelayForPageClosingAnimation); // wait for the transition to complete

            Pages.Pop();

            if (Pages.Count == 0)
            {
                DisableRootScrollbar(false);
            }

            _ = InvokeAsync(StateHasChanged);
        });
    }

    private void DisableRootScrollbar(bool disable)
    {
        _ = Js.InvokeVoidAsync(
            disable ? JsInteropConstants.AddCls : JsInteropConstants.RemoveCls,
            "html",
            "overflow-y-hidden");
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("detachListener", _dotnetObjectId);
            await _module.DisposeAsync();
        }

        if (InternalPageStackNavManager is not null)
        {
            InternalPageStackNavManager.PagePushed -= InternalPageStackNavManagerOnPagePushed;
            InternalPageStackNavManager.PagePopped -= InternalPageStackNavManagerOnPagePopped;
            InternalPageStackNavManager.PageReplaced -= InternalPageStackNavManagerOnPageReplaced;
            InternalPageStackNavManager.PageCleared -= InternalPageStackNavManagerOnPageCleared;
            InternalPageStackNavManager.LocationChanged -= InternalPageStackNavManagerOnLocationChanged;
        }
    }
}