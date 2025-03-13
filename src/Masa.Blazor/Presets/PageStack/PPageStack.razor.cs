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

    private PageStackNavController? InternalPageStackNavController { get; set; }

    private const int DelayForPageClosingAnimation = 300;
    private const string DefaultFallbackUri = "/";

    internal readonly StackPages Pages = new();

    /// <summary>
    /// Determines whether user action triggers the popstate event,
    /// different from the browser's back button.
    /// </summary>
    private bool _popstateByUserAction;

    /// <summary>
    /// Records for popstate event to invoke the stack pop event.
    /// </summary>
    private (int Delta, int Count)? _deltaAndStackCount;

    /// <summary>
    /// The flag to indicate whether to push a new page
    /// and remove top pages in the next popstate event.
    /// </summary>
    private (string relativeUri, int delta)? _callbackForPushAndRemove;

    /// <summary>
    /// The flag to indicate whether to replace the top page
    /// and clear the previous pages in the stack in the next popstate event.
    /// </summary>
    private (string relativeUri, object? state)? _uriForReplaceAndClearStack;

    /// <summary>
    /// The flag to indicate whether to go back to a specific page
    /// and then replace it with a new page.
    /// </summary>
    private (string relativeUri, object? state, int delta)? _uriForGoBackToAndReplace;

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

        InternalPageStackNavController = PageStackNavControllerFactory.Create(Name ?? string.Empty);
        InternalPageStackNavController.BindComponent(this);

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var currentPath = NavigationManager.GetAbsolutePath();
        var tabbedPattern = _cachedTabbedPatterns.FirstOrDefault(u => u.IsMatch(currentPath));

        if (tabbedPattern is not null && _lastVisitedTab.Pattern != tabbedPattern)
        {
            _lastVisitedTab = (tabbedPattern, currentPath);
            InternalPageStackNavController?.NotifyTabChanged(currentPath, tabbedPattern);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _module = await Js.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor/js/components/page-stack/index.js");
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
        if (Pages.Count == 0)
        {
            return;
        }

        // continue to process the push event that needs to clear the stack
        if (_callbackForPushAndRemove.HasValue)
        {
            var (relativeUri, delta) = _callbackForPushAndRemove.Value;

            var startIndex = Pages.Count - delta;
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(delta),
                    "The delta must be less than or equal to the number of pages in the stack.");
            }

            Push(relativeUri);
            NavigationManager.NavigateTo(relativeUri);
            _callbackForPushAndRemove = null;

            _ = Task.Run(async () =>
            {
                await Task.Delay(DelayForPageClosingAnimation); // wait for the transition to complete
                Pages.RemoveRange(startIndex, delta);
                await InvokeAsync(StateHasChanged);
            });

            return;
        }

        // continue to process the replace event that needs to clear the stack
        if (_uriForReplaceAndClearStack.HasValue)
        {
            var (relativeUri, state) = _uriForReplaceAndClearStack.Value;

            Pages.RemoveRange(0, Pages.Count - 1);
            InternalReplaceHandler(relativeUri, state);
            _uriForReplaceAndClearStack = null;
            return;
        }

        // continue to process the go back to event and replace with a new page
        if (_uriForGoBackToAndReplace.HasValue)
        {
            var (replaceUri, state, delta) = _uriForGoBackToAndReplace.Value;

            InternalPageStackNavController?.NotifyStackPopped(delta, replaceUri, state, Pages.Count == delta);

            NavigationManager.Replace(replaceUri);
            replaceUri = GetAbsolutePath(replaceUri);
            CloseTopPages(delta, state, replaceUri);
            _uriForGoBackToAndReplace = null;

            return;
        }

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
        
        if (_deltaAndStackCount.HasValue)
        {
            var (delta, count) = _deltaAndStackCount.Value;
            InternalPageStackNavController?.NotifyStackPopped(delta, clearing: delta >= count);
            _deltaAndStackCount = null;
        }
        else
        {
            InternalPageStackNavController?.NotifyStackPopped(1, clearing: Pages.Count == 1);
        }

        // ignore the popstate event triggered by the user action(back button)
        if (_popstateByUserAction)
        {
            _popstateByUserAction = false;
            return;
        }

        // pop the top page of the stack by the browser's back button
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

    internal void Replace(PageStackReplaceEventArgs e)
    {
        if (e.ClearStack)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -(Pages.Count - 1));
            _uriForReplaceAndClearStack = (e.RelativeUri, e.State);
            return;
        }

        InternalReplaceHandler(e.RelativeUri, e.State);
    }

    private void InternalReplaceHandler(string relativeUri, object? state)
    {
        Pages.UpdateTop(relativeUri, state);
        NavigationManager.Replace(relativeUri);
    }

    internal async Task StackPop(PageStackPopEventArgs e)
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

    internal void Push(PageStackPushEventArgs e, Action? afterPush = null)
    {
        if (e.CountOfTopPagesToRemove != 0)
        {
            // after calling history.go, a popstate event callback will be triggered,
            // where the logic of the stack page is processed (push new page, remove old pages)
            var delta = e.CountOfTopPagesToRemove == -1 ? Pages.Count : e.CountOfTopPagesToRemove;
            _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);
            _callbackForPushAndRemove = (e.RelativeUri, delta);
            return;
        }

        Push(e.RelativeUri);
        NavigationManager.NavigateTo(e.RelativeUri);
    }

    internal void Clear(PageStackClearEventArgs e)
    {
        _deltaAndStackCount = (Pages.Count, Pages.Count);
        
        _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -Pages.Count);

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

    internal void GoBackTo(PageStackGoBackToPageEventArgs e)
    {
        var delta = Pages.GetDelta(e.AbsolutePath);
        if (delta == -1)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(e.ReplaceUri))
        {
            _popstateByUserAction = true;

            _deltaAndStackCount = (delta, Pages.Count);

            CloseTopPages(delta, e.State);
        }
        else
        {
            _uriForGoBackToAndReplace = (e.ReplaceUri, e.State, delta);
        }

        _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -delta);
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

    private void CloseTopPages(int count, object? state = null, string? absolutePath = null)
    {
        if (!Pages.TryPeek(out var current)) return;

        // Set the stacked state to false, which directly determines whether the dialog is displayed
        // and will perform a transition animation
        current.Stacked = false;

        // When the count of pages to be deleted is greater than 1,
        // delete the pages between the top of the stack and the target page
        // For example, if the stack has 5 pages: [1,2,3,4,5], delete 3(count) pages,
        // then delete the page 3 and page 4
        if (count > 1)
        {
            Pages.RemoveRange(Pages.Count - count, count - 1);
        }

        // The stack is now [1,2,5], update the state of the 2nd page
        if (Pages.TryPeekSecondToLast(out var target))
        {
            target.UpdateState(state);

            if (!string.IsNullOrWhiteSpace(absolutePath))
            {
                target.UpdatePath(absolutePath);
            }

            target.Activate();
        }

        InvokeAsync(StateHasChanged);

        Task.Run(async () =>
        {
            // wait for a transition animation from left to right to complete,
            // known animation time is 300 ms
            await Task.Delay(DelayForPageClosingAnimation);

            // Remove page 5 and display page 2
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

        InternalPageStackNavController?.UnbindComponent();

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