using System.Text.RegularExpressions;
using Masa.Blazor.JSInterop;
using Masa.Blazor.Presets.PageStack;
using Masa.Blazor.Presets.PageStack.NavController;
using Masa.Blazor.SourceGenerated;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageStack : MasaComponentBase
{
    [Inject] private IPageStackNavControllerFactory PageStackNavControllerFactory { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public RenderFragment? LoaderContent { get; set; }

    [Parameter] public string? Name { get; set; }

    /// <summary>
    /// Tab rules to match and manage.
    /// </summary>
    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.9.0")]
    public HashSet<TabRule> TabRules { get; set; } = [];

    [Parameter]
    [MasaApiParameter(defaultValue: DefaultFallbackUri)]
    public string? FallbackUri { get; set; } = DefaultFallbackUri;

    /// <summary>
    /// Each page in the stack will have an app bar by default. If set to false,
    /// only the page that include the <see cref="PStackPageBar"/> component will have an app bar.
    /// </summary>
    [Parameter]
    [MasaApiParameter(true, "v1.10.0")]
    public bool AppBarAlwaysVisible { get; set; } = true;

    /// <summary>
    /// If set to true, the container and stack pages
    /// will not slide with a transition animation
    /// </summary>
    [MasaApiParameter(ReleasedIn = "v1.10.0")]
    [Parameter] public bool DisableUnderlaySlide { get; set; }

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
    private (string relativeUri, object? state, int delta, bool disableTransition)? _uriForGoBackToAndReplace;

    private string? _lastVisitedTabPath;
    private long _lastOnPreviousClickTimestamp;

    private TabRecord? _activeTab;

    /// <summary>
    /// Stores the scroll positions of each persistent tab.
    /// </summary>
    private readonly Dictionary<int, double> _scrollPositions = new();

    // just for knowing whether the tab has been changed
    private (Regex Pattern, string AbsolutePath) _lastVisitedTab;

    private IJSObjectReference? _module;
    private DotNetObjectReference<PPageStack>? _dotNetObjectReference;
    private int _dotnetObjectId;

    /// <summary>
    /// Indicates whether to emit the underlay slide effect.
    /// </summary>
    private bool _emitUnderlaySlide;
    
    private RenderFragment? _defaultLoaderContent;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _defaultLoaderContent = RenderDefaultLoader();

        var targetPath = NavigationManager.GetAbsolutePath();
        var tabbedPattern = TabRules.FirstOrDefault(u => u.Regex.IsMatch(targetPath));
        if (tabbedPattern is not null)
        {
            _lastVisitedTabPath = targetPath;

            _lastVisitedTab = (tabbedPattern.Regex, targetPath);
        }
        else
        {
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
        var tabbedPattern = TabRules.FirstOrDefault(u => u.Regex.IsMatch(currentPath));

        if (tabbedPattern is not null && _lastVisitedTab.Pattern != tabbedPattern.Regex)
        {
            _lastVisitedTabPath = currentPath;
            _lastVisitedTab = (tabbedPattern.Regex, currentPath);
            InternalPageStackNavController?.NotifyTabChanged(currentPath, tabbedPattern.Regex);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _module = await Js.InvokeAsync<IJSObjectReference>("import",
                $"./_content/Masa.Blazor/js/{JSManifest.PageStackIndexJs}");
            _dotnetObjectId = await _module.InvokeAsync<int>("attachListener", _dotNetObjectReference);
        }
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return "m-page-stack";

        if (Pages.Count > 0 && Pages.ElementAt(0).Stacked && _emitUnderlaySlide)
        {
            yield return "m-page-stack--has-pages";
        }

        if (DisableUnderlaySlide)
        {
            yield return "m-page-stack--disable-underlay-slide";
        }
    }

    [JSInvokable]
    public void Push(string href)
    {
        if (Pages.Count == 0)
        {
            BlockScroll();
        }

        Pages.Push(GetAbsolutePath(href));
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

        // continue to process the replacement event that needs to clear the stack
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
            var (replaceUri, state, delta, disableTransition) = _uriForGoBackToAndReplace.Value;

            InternalPageStackNavController?.NotifyStackPopped(delta, replaceUri, state, Pages.Count == delta);

            NavigationManager.Replace(replaceUri);
            replaceUri = GetAbsolutePath(replaceUri);
            CloseTopPages(delta, disableTransition, state, replaceUri);
            _uriForGoBackToAndReplace = null;

            return;
        }

        var tabbedPattern = TabRules.FirstOrDefault(r => r.Regex.IsMatch(absolutePath));
        if (tabbedPattern is not null)
        {
            _lastVisitedTabPath = absolutePath;
            UnblockScroll();
            StateHasChanged();
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

    [JSInvokable("Scroll")]
    public void OnWindowScroll(double pageYOffset)
    {
        if (_activeTab is null || !_activeTab.Rule.Persistent)
        {
            return;
        }

        _scrollPositions[_activeTab.Id] = pageYOffset;
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

        CloseTopPages(e.Delta, e.DisableTransition, e.State);
        await Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -e.Delta);

        if (!string.IsNullOrWhiteSpace(e.ReplaceUri))
        {
            if (!e.DisableTransition)
            {
                await Task.Delay(DelayForPageClosingAnimation);
            }

            InternalReplaceHandler(e.ReplaceUri, e.State);
        }
    }

    internal void Push(PageStackPushEventArgs e)
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

        if (_lastVisitedTabPath is not null)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.HistoryGo, -Pages.Count);
        }
        else
        {
            NavigationManager.NavigateTo(FallbackUri ?? DefaultFallbackUri);
        }

        var backToLastVisitTab = string.IsNullOrWhiteSpace(e.RelativeUri) ||
                                 _lastVisitedTabPath == GetAbsolutePath(e.RelativeUri);

        if (backToLastVisitTab)
        {
            CloseTopPages(Pages.Count, e.DisableTransition);
            return;
        }

        Pages.Clear();
        UnblockScroll();
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

            CloseTopPages(delta, e.DisableTransition, e.State);
        }
        else
        {
            _uriForGoBackToAndReplace = (e.ReplaceUri, e.State, delta, e.DisableTransition);
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

    private void OnActiveTabUpdate(TabRecord activeTabId)
    {
        _activeTab = activeTabId;

        if (_scrollPositions.TryGetValue(_activeTab.Id, out var pageYOffset))
        {
            _ = Js.ScrollTo(pageYOffset, 0, ScrollBehavior.Auto);
        }
    }

    private void CloseTopPageOfStack(object? state = null) => CloseTopPages(1, false, state);

    private void CloseTopPages(int count, bool disableTransition, object? state = null, string? absolutePath = null)
    {
        if (!Pages.TryPeek(out var current)) return;

        // Set the stacked state to false, which directly determines whether the dialog is displayed
        // and will perform a transition animation
        current.Stacked = false;

        current.DisableTransition = disableTransition;

        // When the count of pages to be deleted is greater than 1,
        // delete the pages between the top of the stack and the target page,
        // For example, if the stack has 5 pages: [1,2,3,4,5], delete 3(count) pages,
        // then delete page 3 and page 4
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
            if (!current.DisableTransition)
            {
                // wait for a transition animation from left to right to complete,
                // known animation time is 300 ms
                await Task.Delay(DelayForPageClosingAnimation);
            }

            // Remove page 5 and display page 2
            Pages.Pop();

            if (Pages.Count == 0)
            {
                UnblockScroll();
            }

            _ = InvokeAsync(StateHasChanged);
        });
    }

    private void BlockScroll()
    {
        Task.Run(async () =>
        {
            try
            {
                // The rendering of the stack page has a cost,
                // which may cause the tab page to move first and the stack page to move later.
                // Here we delay 48 milliseconds to ensure that the stack page is rendered
                // before triggering the sliding animation.
                // 48 milliseconds is an empirical value,
                // as long as it is within 300 milliseconds of the stack page rendering.
                await Task.Delay(48);

                if (!IsDisposed)
                {
                    _emitUnderlaySlide = true;
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.Error.WriteLine($"Error in BlockScroll: {ex}");
            }
        });

        _module?.InvokeVoidAsync("blockScroll").ConfigureAwait(false);
    }

    private void UnblockScroll()
    {
        _emitUnderlaySlide = false;

        _module?.InvokeVoidAsync("unblockScroll").ConfigureAwait(false);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;

        InternalPageStackNavController?.UnbindComponent();

        if (_module is not null)
        {
            await _module.InvokeVoidAsync("detachListener", _dotnetObjectId);
            await _module.DisposeAsync();
            _module = null;
        }
    }
}