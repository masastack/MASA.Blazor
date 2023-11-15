using Masa.Blazor.PullToRefresh;
using Masa.Blazor.Utils;

namespace Masa.Blazor;

public partial class MPullRefresh : IAsyncDisposable
{
    [CascadingParameter(Name = "IsDark")] private bool CascadingIsDark { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] [EditorRequired] public EventCallback OnRefresh { get; set; }

    [Parameter] public EventCallback<Exception> OnError { get; set; }

    [Parameter] [MassApiParameter(50)] public int HeadHeight { get; set; } = 50;

    [Parameter] [MassApiParameter(500)] public int SuccessDuration { get; set; } = 500;

    [Parameter] [MassApiParameter(50)] public int Threshold { get; set; } = 50;

    [Parameter] public string? PullingText { get; set; }

    [Parameter] public string? CanReleaseText { get; set; }

    [Parameter] public string? SuccessText { get; set; }

    [Parameter] public RenderFragment? DefaultContent { get; set; }

    [Parameter] public RenderFragment<double>? LoadingContent { get; set; }

    [Parameter] public RenderFragment<double>? CanReleaseContent { get; set; }

    [Parameter] public RenderFragment<double>? PullingContent { get; set; }

    [Parameter] public RenderFragment? SuccessContent { get; set; }

    private readonly Toucher _toucher = new();
    private readonly ThrottleTask _throttleTask = new(16);

    private IJSObjectReference? _scrollParentJSRef;

    private ElementReference _trackRef;
    private PullRefreshStatus _pullRefreshStatus;
    private BlazorComponent.Web.Element? _root;
    private int _duration;
    private bool _reachTop;
    private double _distance;

    private bool IsTouchable => _pullRefreshStatus != PullRefreshStatus.Loading && _pullRefreshStatus != PullRefreshStatus.Success && !Disabled;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PullingText ??= I18n.T("$masaBlazor.pullRefresh.pullingText");
        CanReleaseText ??= I18n.T("$masaBlazor.pullRefresh.canReleaseText");
        SuccessText ??= I18n.T("$masaBlazor.pullRefresh.successText");
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.UseBem("m-pull-refresh", css => { css.Modifiers(m => m.Modifier(_pullRefreshStatus, "status")).AddTheme(CascadingIsDark); })
                   .Element("track", _ => { }, style =>
                   {
                       style.Add($"transition-duration: {_duration}ms;")
                            .AddIf($"transform:translate3d(0,{_distance}px, 0)", () => _distance > 0);
                   })
                   .Element("header", _ => { }, style => { style.AddHeight(HeadHeight); });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _scrollParentJSRef = await Js.InvokeAsync<IJSObjectReference>(JsInteropConstants.GetScrollParent, Ref);

            var trackSelector = _trackRef.GetSelector()!;

            _ = Js.AddHtmlElementEventListener<TouchEventArgs>(trackSelector, "touchstart", OnTouchStart, new EventListenerOptions()
            {
                Passive = true
            });

            _ = Js.AddHtmlElementEventListener<TouchEventArgs>(trackSelector, "touchmove", OnTouchMove, new EventListenerOptions()
            {
                Passive = false
            }, new EventListenerExtras()
            {
                PreventDefault = true,
                Throttle = 16
            });
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
            _root = null;
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

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await _scrollParentJSRef.TryDisposeAsync();

            if (_trackRef.TryGetSelector(out var trackSelector))
            {
                await Js.RemoveHtmlElementEventListener(trackSelector, "touchstart");
                await Js.RemoveHtmlElementEventListener(trackSelector, "touchmove");
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
