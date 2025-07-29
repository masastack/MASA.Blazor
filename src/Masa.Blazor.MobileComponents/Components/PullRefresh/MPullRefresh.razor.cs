using Masa.Blazor.PullToRefresh;
using Masa.Blazor.Utils;

namespace Masa.Blazor;

public partial class MPullRefresh
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] [EditorRequired] public EventCallback OnRefresh { get; set; }

    [Parameter] public EventCallback<Exception> OnError { get; set; }

    [Parameter] [MasaApiParameter(50)] public int HeadHeight { get; set; } = 50;

    [Parameter] [MasaApiParameter(500)] public int SuccessDuration { get; set; } = 500;

    [Parameter] [MasaApiParameter(50)] public int Threshold { get; set; } = 50;

    [Parameter] public string? PullingText { get; set; }

    [Parameter] public string? CanReleaseText { get; set; }

    [Parameter] public string? SuccessText { get; set; }

    [Parameter] public RenderFragment? DefaultContent { get; set; }

    [Parameter] public RenderFragment<double>? LoadingContent { get; set; }

    [Parameter] public RenderFragment<double>? CanReleaseContent { get; set; }

    [Parameter] public RenderFragment<double>? PullingContent { get; set; }

    [Parameter] public RenderFragment? SuccessContent { get; set; }
    
    private static Block _block = new("m-pull-refresh");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    private readonly Toucher _toucher = new();
    private readonly ThrottleTask _throttleTask = new(16);

    private IJSObjectReference? _scrollParentJSRef;

    private ElementReference _trackRef;
    private PullRefreshStatus _pullRefreshStatus;
    private int _duration;
    private bool _reachTop;
    private double _distance;

    private long _touchstartEventId;
    private long _touchmoveEventId;

    /// <summary>
    /// Only render <see cref="ChildContent"/> when the status is <see cref="PullRefreshStatus.Default"/>.
    /// We don't want to render it when loading, pulling, or releasing.
    /// </summary>
    private bool IsRenderStatus => _pullRefreshStatus == PullRefreshStatus.Default;

    private bool IsTouchable => _pullRefreshStatus != PullRefreshStatus.Loading && _pullRefreshStatus != PullRefreshStatus.Success && !Disabled;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PullingText ??= I18n.T("$masaBlazor.pullRefresh.pullingText");
        CanReleaseText ??= I18n.T("$masaBlazor.pullRefresh.canReleaseText");
        SuccessText ??= I18n.T("$masaBlazor.pullRefresh.successText");
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(_pullRefreshStatus, "status").AddTheme(ComputedTheme).Build();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _scrollParentJSRef = await Js.InvokeAsync<IJSObjectReference>(JsInteropConstants.GetScrollParent, Ref);

            var trackSelector = _trackRef.GetSelector()!;

            _ = Task.Run(async () =>
            {
                _touchstartEventId = await Js.AddHtmlElementEventListener<TouchEventArgs>(trackSelector, "touchstart",
                    OnTouchStart, new EventListenerOptions(passive: true));

                _touchmoveEventId = await Js.AddHtmlElementEventListener<TouchEventArgs>(trackSelector, "touchmove",
                    OnTouchMove, new EventListenerOptions(passive: false),
                    new EventListenerExtras
                    {
                        PreventDefault = false,
                        Throttle = 16
                    });
            });
        }
    }
    
    /// <summary>
    /// Simulates a pull-to-refresh action programmatically.
    /// </summary>
    [MasaApiPublicMethod]
    public async Task SimulateRefreshAsync()
    {
        // Ensure the scroll parent is scrolled to the top before refreshing.
        _ = Js.ScrollTo(_scrollParentJSRef, 0).ConfigureAwait(false);

        if (_pullRefreshStatus == PullRefreshStatus.Default)
        {
            _duration = 300;
            SetStatus(HeadHeight, true);

            try
            {
                await OnRefresh.InvokeAsync();
                await SetResultStatus();
            }
            catch (Exception e)
            {
                SetStatus(0);
                await OnError.InvokeAsync(e);
            }
        }
    }

    private void SetStatus(double distance, bool isLoading = false)
    {
        _distance = distance;

        if (isLoading)
        {
            _pullRefreshStatus = PullRefreshStatus.Loading;
        }
        else if (distance == 0)
        {
            _pullRefreshStatus = PullRefreshStatus.Default;
            _throttleTask.Cancel();
        }
        else if (distance < Threshold)
        {
            _pullRefreshStatus = PullRefreshStatus.Pulling;
        }
        else
        {
            _pullRefreshStatus = PullRefreshStatus.CanRelease;
        }

        StateHasChanged();
    }

    private async Task OnTouchStart(TouchEventArgs args)
    {
        if (IsTouchable)
        {
            await CheckPosition(args);
        }
    }

    private async Task OnTouchMove(TouchEventArgs args)
    {
        if (IsTouchable)
        {
            if (!_reachTop)
            {
                await CheckPosition(args);
            }

            _toucher.Move(args);

            if (_reachTop && _toucher.DeltaY >= 0 && _toucher.IsVertical)
            {
                // HACK: In JavaScript, should dynamically set the `preventDefault()` here,
                // but in Blazor, it's challenging to do that. So we set it to false in `AddHtmlElementEventListener`.
                // If it's necessary, should refactor this part.
                // args.PreventDefault();

                await _throttleTask.RunAsync(async () =>
                {
                    if (IsTouchable)
                    {
                        var easeDeltaY = Ease(_toucher.DeltaY);
                        SetStatus(easeDeltaY);
                        await InvokeStateHasChangedAsync();
                    }
                });
            }
        }
    }

    private async Task OnTouchEnd()
    {
        if (_reachTop && _toucher.DeltaY > 0 && IsTouchable)
        {
            _duration = 300;

            if (_pullRefreshStatus == PullRefreshStatus.CanRelease)
            {
                SetStatus(HeadHeight, true);

                try
                {
                    await OnRefresh.InvokeAsync();
                    await SetResultStatus();
                }
                catch (Exception e)
                {
                    SetStatus(0);
                    await OnError.InvokeAsync(e);
                }
            }
            else
            {
                SetStatus(0);
            }
        }
    }

    private async Task SetResultStatus()
    {
        _pullRefreshStatus = PullRefreshStatus.Success;
        StateHasChanged();
        await Task.Delay(SuccessDuration);
        SetStatus(0);
    }

    private async Task CheckPosition(TouchEventArgs args)
    {
        _reachTop = await GetScrollTopAsync() == 0;

        if (_reachTop)
        {
            _duration = 0;
            _toucher.Start(args);
        }
    }

    private async Task<double> GetScrollTopAsync()
    {
        return await Js.InvokeAsync<double>(JsInteropConstants.GetScrollTop, _scrollParentJSRef);
    }

    private double Ease(double distance)
    {
        if (distance > Threshold)
        {
            if (distance < Threshold * 2)
            {
                distance = Threshold + (distance - Threshold) / 2;
            }
            else
            {
                distance = Threshold * 1.5 + (distance - Threshold * 2) / 4;
            }
        }

        return Math.Round(distance);
    }

    protected override bool AfterHandleEventShouldRender() => false;

    protected override async ValueTask DisposeAsyncCore()
    {
        await _scrollParentJSRef.TryDisposeAsync();
        _scrollParentJSRef = null;

        await Js.RemoveHtmlElementEventListener(_touchstartEventId);
        await Js.RemoveHtmlElementEventListener(_touchmoveEventId);
    }
}
