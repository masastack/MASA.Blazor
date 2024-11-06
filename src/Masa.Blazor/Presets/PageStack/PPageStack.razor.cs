using Masa.Blazor.Presets.PageStack;
using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.AspNetCore.Components.Routing;

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
    /// Determines whether user action triggers the popstate event,
    /// different from the browser's back button.
    /// </summary>
    private bool _popstateByUserAction;

    private string? _lastVisitedTabPath;
    private PageType _targetPageType;
    private long _lastOnPreviousClickTimestamp;

    // just for knowing whether the tab has been changed
    private (Regex Pattern, string AbsolutePath) _lastVisitedTab;

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

            _lastVisitedTab = (tabbedPattern, targetPath);
        }
        else
        {
            _targetPageType = PageType.Stack;
            Push(NavigationManager.Uri);
        }

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;

        InternalPageStackNavManager = PageStackNavControllerFactory.Create(Name ?? string.Empty);
        InternalPageStackNavManager.StackPush += InternalStackStackNavManagerOnStackPush;
        InternalPageStackNavManager.StackPop += InternalPageStackNavManagerOnStackPop;
        InternalPageStackNavManager.StackReplace += InternalStackStackNavManagerOnStackReplace;
        InternalPageStackNavManager.StackClear += InternalStackStackNavManagerOnStackClear;
        InternalPageStackNavManager.StackGoBackTo += InternalPageStackNavManagerOnStackGoBackTo;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var currentPath = NavigationManager.GetAbsolutePath();
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(u => u.IsMatch(currentPath));

        if (tabbedPattern is not null && _lastVisitedTab.Pattern != tabbedPattern)
        {
            _lastVisitedTab = (tabbedPattern, currentPath);
            InternalPageStackNavManager?.NotifyTabChanged(currentPath, tabbedPattern);
        }
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
        InternalReplaceHandler(e.RelativeUri, e.State);
    }

    private void InternalReplaceHandler(string relativeUri, object? state)
    {
        Pages.UpdateTop(relativeUri, state);
        NavigationManager.Replace(relativeUri);
    }

    private async void InternalPageStackNavManagerOnStackPop(object? sender, PageStackPopEventArgs e)
    {
        _popstateByUserAction = true;

        CloseTopPages(e.Delta, e.State);
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -e.Delta);

        if (!string.IsNullOrWhiteSpace(e.ReplaceUri))
        {
            await Task.Delay(DelayForPageClosingAnimation);
            InternalReplaceHandler(e.ReplaceUri, e.State);
        }
    }

    private void InternalStackStackNavManagerOnStackPush(object? sender, PageStackPushEventArgs e)
    {
        Push(e.RelativeUri);
        NavigationManager.NavigateTo(e.RelativeUri);
    }

    private async void InternalStackStackNavManagerOnStackClear(object? sender, PageStackClearEventArgs e)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -Pages.Count);

        var backToLastVisitTab = string.IsNullOrWhiteSpace(e.RelativeUri) ||
                                 _lastVisitedTabPath == GetAbsolutePath(e.RelativeUri);

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

    private async void InternalPageStackNavManagerOnStackGoBackTo(object? sender, PageStackGoBackToPageEventArgs e)
    {
        var delta = Pages.GetDelta(e.AbsolutePath);
        if (delta == -1)
        {
            return;
        }

        _popstateByUserAction = true;

        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);

        CloseTopPages(delta, e.State);

        if (!string.IsNullOrWhiteSpace(e.ReplaceUri))
        {
            await Task.Delay(DelayForPageClosingAnimation);
            InternalReplaceHandler(e.ReplaceUri, e.State);
        }
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
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;

        if (InternalPageStackNavManager is not null)
        {
            InternalPageStackNavManager.StackPush -= InternalStackStackNavManagerOnStackPush;
            InternalPageStackNavManager.StackPop -= InternalPageStackNavManagerOnStackPop;
            InternalPageStackNavManager.StackReplace -= InternalStackStackNavManagerOnStackReplace;
            InternalPageStackNavManager.StackClear -= InternalStackStackNavManagerOnStackClear;
            InternalPageStackNavManager.StackGoBackTo -= InternalPageStackNavManagerOnStackGoBackTo;
        }

        if (_module is not null)
        {
            await _module.InvokeVoidAsync("detachListener", _dotnetObjectId);
            await _module.DisposeAsync();
        }
    }

    private enum PageType
    {
        Tab,
        Stack
    }
}