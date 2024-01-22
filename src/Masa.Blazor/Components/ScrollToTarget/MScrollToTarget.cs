using Masa.Blazor.ScrollToTarget;

namespace Masa.Blazor;

public class MScrollToTarget : ComponentBase, IAsyncDisposable
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

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

    [Parameter] public string? ScrollContainer { get; set; }

    private bool _task;
    private TargetContext _targetContext = new();
    private List<string> _activeStack = new();
    private bool _hasRendered;
    private List<Action>? _tasks;

    private List<string> Targets { get; } = new();

    public string? ActiveTarget => _targetContext.ActiveTarget;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Targets.ThrowIfNull(nameof(MScrollToTarget));
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RootMarginTop ??= "0px";
        RootMarginRight ??= "0px";
        RootMarginBottom ??= "0px";
        RootMarginLeft ??= "0px";

        _targetContext.ActiveClass = ActiveClass;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
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
        _ = IntersectJSModule.UnobserveAsync($"#{target}");
    }

    private async ValueTask ObserverAsync(string target)
    {
        if (!Targets.Contains(target))
        {
            Targets.Add(target);
        }

        await IntersectJSModule.ObserverAsync($"#{target}", e => OnIntersectAsync(e, target),
            new IntersectionObserverInit()
            {
                RootMarginTop = RootMarginTop,
                RootMarginRight = RootMarginRight,
                RootMarginBottom = RootMarginBottom,
                RootMarginLeft = RootMarginLeft,
                AutoRootMargin = AutoRootMargin,
                RootSelector = RootSelector,
                Threshold = Threshold
            });
    }

    private CancellationTokenSource? _cts;

    private async Task OnIntersectAsync(IntersectEventArgs e, string target)
    {
        if (e.IsIntersecting)
        {
            _activeStack.Add(target);
        }
        else
        {
            _activeStack.Remove(target);
        }

        _cts?.Cancel();
        _cts = new();

        try
        {
            await Task.Delay(32, _cts.Token);
            if (_activeStack.Count > 0)
            {
                var lastActive = _activeStack.Last();
                _targetContext.ActiveTarget = lastActive;
                _ = InvokeAsync(StateHasChanged);
            }
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    private async Task ScrollToTargetAsync(string target, string? container = null)
    {
        await JSRuntime.InvokeVoidAsync(JsInteropConstants.ScrollToTarget, target, container, Offset);
    }

    [MasaApiPublicMethod]
    public void TryObserverTargets()
    {
        foreach (var target in Targets.ToList())
        {
            _ = ObserverAsync(target);
        }
    }

    [MasaApiPublicMethod]
    public void ScrollToTarget(string target)
    {
        if (Targets.Contains(target))
        {
            _ = ScrollToTargetAsync($"#{target}", ScrollContainer);
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            foreach (var target in Targets)
            {
                _ = IntersectJSModule.UnobserveAsync($"#{target}");
            }
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }
}
