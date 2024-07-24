using Masa.Blazor.Presets.PageStack;
using Masa.Blazor.Presets.PageStack.NavController;

namespace Masa.Blazor.Presets;

public partial class PPageStack : PatternPathComponentBase
{
    [Inject] private IPageStackNavControllerFactory PageStackNavControllerFactory { get; set; } = null!;

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

    /// <summary>
    /// Determines whether the popstate event is triggered by user action,
    /// different from the browser's back button.
    /// </summary>
    private bool _popstateByUserAction;

    private string? _lastVisitedTabPath;
    private PageType _targetPageType;
    private string? _latestTabPath;
    private long _lastOnPreviousClickTimestamp;

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
        InternalPageStackNavManager.StackPush += InternalStackStackNavManagerOnStackPush;
        InternalPageStackNavManager.StackPop += InternalPageStackNavManagerOnStackPop;
        InternalPageStackNavManager.StackReplace += InternalStackStackNavManagerOnStackReplace;
        InternalPageStackNavManager.StackClear += InternalStackStackNavManagerOnStackClear;

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
        Pages.Push(GetAbsolutePath(href));
        DisableRootScrollbar(true);
        InvokeAsync(StateHasChanged);
    }
    
    [JSInvokable]
    public void Popstate(string absolutePath)
    {
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(r => r.IsMatch(absolutePath));
        
        if (tabbedPattern is not null)
        {
            _lastVisitedTabPath = absolutePath;
            _targetPageType = PageType.Tab;
            DisableRootScrollbar(false);
            StateHasChanged();
        }
        else
        {
            _targetPageType = PageType.Stack;
        }

        if (_popstateByUserAction)
        {
            _popstateByUserAction = false;
            return;
        }

        if (Pages.Count > 0)
        {
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

    private void InternalStackStackNavManagerOnStackReplace(object? sender, PageStackReplaceEventArgs e)
    {
        Pages.UpdateTop(e.RelativeUri, e.State);
    }

    private void InternalPageStackNavManagerOnStackPop(object? sender, PageStackPopEventArgs e)
    {
        _popstateByUserAction = true;

        CloseTopPages(e.Delta, e.State);
    }

    private void InternalStackStackNavManagerOnStackPush(object? sender, PageStackPushEventArgs e)
    {
        Push(e.RelativeUri);
    }

    private async void InternalStackStackNavManagerOnStackClear(object? sender, PageStackClearEventArgs e)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -Pages.Count);

        var backToLastVisitTab = string.IsNullOrWhiteSpace(e.RelativeUri) || _lastVisitedTabPath == GetAbsolutePath(e.RelativeUri);

        if (backToLastVisitTab)
        {
            CloseTopPages(Pages.Count);
            return;
        }

        Pages.Clear();
        DisableRootScrollbar(false);
        _ = InvokeAsync(StateHasChanged);

        NextTick(() => NavigationManager.Replace(e.RelativeUri!));
    }

    private string GetAbsolutePath(string relativeUri) => NavigationManager.ToAbsoluteUri(relativeUri).AbsolutePath;

    private void HandleOnPrevious()
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        if (now - _lastOnPreviousClickTimestamp > 250)
        {
            _popstateByUserAction = true;

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

        _lastOnPreviousClickTimestamp = now;
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

        InvokeAsync(StateHasChanged);

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
            InternalPageStackNavManager.StackPush -= InternalStackStackNavManagerOnStackPush;
            InternalPageStackNavManager.StackPop -= InternalPageStackNavManagerOnStackPop;
            InternalPageStackNavManager.StackReplace -= InternalStackStackNavManagerOnStackReplace;
            InternalPageStackNavManager.StackClear -= InternalStackStackNavManagerOnStackClear;
        }
    }

    private enum PageType
    {
        Tab,
        Stack
    }
}