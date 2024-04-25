using Masa.Blazor.Presets.PageStack;
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

    internal PageStackNavController InternalPageStackNavManager { get; private set; }

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

        InternalPageStackNavManager = PageStackNavControllerFactory.Create(Name ?? string.Empty);
        InternalPageStackNavManager.PagePushed += InternalPageStackNavManagerOnPagePushed;
        InternalPageStackNavManager.PagePopped += InternalPageStackNavManagerOnPagePopped;
        InternalPageStackNavManager.PageReplaced += InternalPageStackNavManagerOnPageReplaced;
        InternalPageStackNavManager.LocationChanged += InternalPageStackNavManagerOnLocationChanged;
        // NavigationManager.LocationChanged += InternalStackNavManagerOnLocationChanged;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    private IJSObjectReference? _module;
    private DotNetObjectReference<PPageStack>? _dotNetObjectReference;
    private int _dotnetObjectId;

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
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        Pages.Push(NavigationManager.ToAbsoluteUri(href).AbsolutePath);

        InvokeAsync(StateHasChanged);
    }

    private void InternalPageStackNavManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
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

    private void InternalPageStackNavManagerOnPageReplaced(object? sender, PageStackReplacedEventArgs e)
    {
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        Pages.UpdateTop(e.Uri, null);
    }

    private void InternalPageStackNavManagerOnPagePopped(object? sender, PageStackPoppedEventArgs e)
    {
        _locationChangedByUserClick++;
        _pageTypeOfPreviousPath = PageType.Stack;

        CloseTopPages(e.Delta, e.State);
    }

    private void InternalPageStackNavManagerOnPagePushed(object? sender, PageStackPushedEventArgs e)
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

    private void CloseTopPageOfStack(object? state = null) => CloseTopPages(1, state);

    private void CloseTopPages(int count, object? state = null)
    {
        if (Pages.TryPeek(out var current))
        {
            current.Stacked = false;

            // 需要先删顶部和目标页中间的页面

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

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("detachListener", _dotnetObjectId);
            await _module.DisposeAsync();
        }

        InternalPageStackNavManager.PagePushed -= InternalPageStackNavManagerOnPagePushed;
        InternalPageStackNavManager.PagePopped -= InternalPageStackNavManagerOnPagePopped;
        InternalPageStackNavManager.PageReplaced -= InternalPageStackNavManagerOnPageReplaced;
        // InternalStackNavManager.LocationChanged -= InternalStackNavManagerOnLocationChanged;
        NavigationManager.LocationChanged -= InternalPageStackNavManagerOnLocationChanged;
    }
}