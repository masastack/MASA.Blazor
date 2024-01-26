using Masa.Blazor.Components.ScrollToTarget;
using Masa.Blazor.ScrollToTarget;

namespace Masa.Blazor;

public class MScrollToTarget : ComponentBase, IAsyncDisposable
{
    [Inject] private ScrollToTargetJSModule ScrollToTargetJSModule { get; set; } = null!;

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] [EditorRequired] public RenderFragment<TargetContext> ChildContent { get; set; } = default!;

    [Parameter] public string? ActiveClass { get; set; }

    /// <summary>
    /// The top value of RootMargin for IntersectionObserver
    /// </summary>
    [Parameter] [MasaApiParameter("0px")] public string RootMarginTop { get; set; } = "0px;";

    /// <summary>
    /// The right value of RootMargin for IntersectionObserver
    /// </summary>
    [Parameter] [MasaApiParameter("0px")] public string RootMarginRight { get; set; } = "0px";

    /// <summary>
    /// THe bottom value of RootMargin for IntersectionObserver
    /// </summary>
    [Parameter] [MasaApiParameter("0px")] public string RootMarginBottom { get; set; } = "0px";

    /// <summary>
    /// The left value of RootMargin for IntersectionObserver
    /// </summary>
    [Parameter] [MasaApiParameter("0px")] public string RootMarginLeft { get; set; } = "0px";

    /// <summary>
    /// The margin value of RootMargin that will be automatically calculated.
    /// For example, if you set <see cref="RootMarginTop"/> to 64px,
    /// the value of RootMargin bottom will be `calc(64px - 100%)`.
    /// </summary>
    [Parameter] public AutoRootMargin AutoRootMargin { get; set; }

    [Parameter] public string? RootSelector { get; set; }

    [Parameter] public int Offset { get; set; }

    [Parameter] public double[] Threshold { get; set; } = { };

    [Parameter] public string? ScrollContainerSelector { get; set; }

    [Parameter] public bool DisableIntersectAfterTriggering { get; set; }

    private bool _task;

    private TargetContext _targetContext = new();
    private List<string> _activeStack = new();
    private bool _hasRendered;
    private List<Action>? _tasks;

    private DotNetObjectReference<ScrollToTargetJSInteropHandle>? _jsInteropHandle;
    private ScrollToTargetJSObjectReference? _scrollToTargetJSObjectReference;

    private bool _disableIntersecting;
    private CancellationTokenSource? _ctsForDisableIntersecting;

    private List<string> Targets { get; } = new();

    public string? ActiveTarget => _targetContext.ActiveTarget;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Targets.ThrowIfNull(nameof(MScrollToTarget));

        _jsInteropHandle = DotNetObjectReference.Create(new ScrollToTargetJSInteropHandle(this));
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RootMarginTop ??= "0px";
        RootMarginRight ??= "0px";
        RootMarginBottom ??= "0px";
        RootMarginLeft ??= "0px";

        _targetContext.ScrollToTarget ??= ScrollToTarget;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _scrollToTargetJSObjectReference = await ScrollToTargetJSModule.InitAsync(_jsInteropHandle!, new IntersectionObserverInit()
            {
                RootMarginTop = RootMarginTop,
                RootMarginRight = RootMarginRight,
                RootMarginBottom = RootMarginBottom,
                RootMarginLeft = RootMarginLeft,
                AutoRootMargin = AutoRootMargin,
                RootSelector = RootSelector,
                Threshold = Threshold
            });

            _hasRendered = true;

            if (_tasks is { Count: > 0 })
            {
                _tasks.ForEach(item => item.Invoke());
            }
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<MScrollToTarget>>(0);
        builder.AddAttribute(1, "IsFixed", true);
        builder.AddAttribute(2, "Value", this);
        builder.AddAttribute(3, "ChildContent", ChildContent.Invoke(_targetContext));
        builder.CloseElement();
    }

    internal void RegisterTarget(string target)
    {
        if (_hasRendered)
        {
            Observer();
        }
        else
        {
            _tasks ??= new();
            _tasks.Add(Observer);
        }

        return;

        void Observer() => _ = ObserverAsync(target);
    }

    internal void UnregisterTarget(string target)
    {
        Targets.Remove(target);

        _scrollToTargetJSObjectReference?.UnobserveAsync(target);
    }

    private async Task DisableIntersect()
    {
        _ctsForDisableIntersecting?.Cancel();
        _ctsForDisableIntersecting = new();

        try
        {
            _disableIntersecting = true;
            await Task.Delay(500, _ctsForDisableIntersecting.Token); // smooth scroll animation time is 200~500ms
            _disableIntersecting = false;
            if (lastTarget is not null)
            {
                _targetContext.ActiveTarget = lastTarget;
                lastTarget = null;
                StateHasChanged();
            }
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    private async ValueTask ObserverAsync(string target)
    {
        if (!Targets.Contains(target))
        {
            Targets.Add(target);
        }

        _scrollToTargetJSObjectReference?.ObserveAsync(target);
    }

    private string lastTarget;

    internal async ValueTask UpdateActiveTarget(string target)
    {
        lastTarget = target;

        if (_disableIntersecting)
        {
            return;
        }

        _targetContext.ActiveTarget = target;
        await InvokeAsync(StateHasChanged);
    }

    [MasaApiPublicMethod]
    public void ScrollToTarget(string target)
    {
        if (DisableIntersectAfterTriggering)
        {
            _ = DisableIntersect();
        }

        _ = JSRuntime.InvokeVoidAsync(
            JsInteropConstants.ScrollToTarget,
            $"#{target}",
            ScrollContainerSelector,
            Offset);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            foreach (var target in Targets)
            {
                _ = _scrollToTargetJSObjectReference?.UnobserveAsync(target);
                _ = _scrollToTargetJSObjectReference?.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }
}
