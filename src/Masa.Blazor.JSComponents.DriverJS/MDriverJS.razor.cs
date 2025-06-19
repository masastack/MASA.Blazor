using Masa.Blazor.Attributes;
using Masa.Blazor.Core;
using Masa.Blazor.JSComponents.DriverJS;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public partial class MDriverJS : DisposableComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Determines whether the driver should automatically start when the component is rendered.
    /// You can also start the driver manually by calling the <see cref="Drive"/> method.
    /// </summary>
    [Parameter]
    public bool AutoDrive { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool Animate { get; set; } = true;

    [Parameter]
    public string? OverlayColor { get; set; }

    [Parameter] public bool SmoothScroll { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool AllowClose { get; set; } = true;

    [Parameter]
    [MasaApiParameter(0.25f)]
    public float OverlayOpacity { get; set; } = 0.25f;

    [Parameter]
    [MasaApiParameter(10)]
    public int StagePadding { get; set; } = 10;

    [Parameter]
    [MasaApiParameter(5)]
    public int StageRadius { get; set; } = 5;

    [Parameter]
    [MasaApiParameter(true)]
    public bool AllowKeyboardControl { get; set; } = true;

    [Parameter] public bool DisableActiveInteraction { get; set; }

    [Parameter] public string? PopoverClass { get; set; }

    [Parameter]
    [MasaApiParameter(10)]
    public int PopoverOffset { get; set; } = 10;

    [Parameter]
    [MasaApiParameter(new[] { "next", "previous", "close" })]
    public string[]? ShowButtons { get; set; } = ["next", "previous", "close"];

    // disableButtons seems not working, not expose it for now, the version I tested is 1.3.5
    // [Parameter] public string[]? DisableButtons { get; set; }

    [Parameter] public bool ShowProgress { get; set; }

    [Parameter] public string? ProgressText { get; set; }

    [Parameter] public string? NextBtnText { get; set; }

    [Parameter] public string? PrevBtnText { get; set; }

    [Parameter] public string? DoneBtnText { get; set; }

    private bool _initSteps;
    private bool _hasRendered;
    private CancellationTokenSource? _initStepsCts;
    private List<IDriverJSStep> _steps = [];
    private IJSObjectReference? _importJSObjectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _importJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.JSComponents.DriverJS/MDriverJS.js").ConfigureAwait(false);

            _hasRendered = true;

            if (_initSteps == false && AutoDrive)
            {
                await InitDriverJsAsync();
            }
        }
    }

    /// <summary>
    /// Highlight an element.
    /// </summary>
    /// <param name="step"></param>
    [MasaApiParameter]
    public void Highlight(DriverStep step)
        => _ = _importJSObjectReference?.InvokeVoidAsync("highlight", GetConfig(), step).ConfigureAwait(false);

    /// <summary>
    /// Start the driver.
    /// </summary>
    /// <param name="stepIndex"></param>
    [MasaApiParameter]
    public void Drive(int stepIndex = 0) => _ = DriveAsync(stepIndex).ConfigureAwait(false);

    internal async Task AddStep(IDriverJSStep step)
    {
        _steps.Add(step);

        if (AutoDrive)
        {
            await InitDriverJsAsync();
        }
    }

    private async Task InitDriverJsAsync()
    {
        _initStepsCts?.Cancel();
        _initStepsCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(100, _initStepsCts.Token);

            if (!_hasRendered)
            {
                return;
            }

            _initSteps = true;

            await DriveAsync(0);
        }
        catch (TaskCanceledException)
        {
            // ignore
        }
    }

    private async Task DriveAsync(int stepIndex)
    {
        if (_importJSObjectReference is null)
        {
            return;
        }

        var steps = _steps.Select(s => new DriverStep(s.Element, new Popover(
            s.Title,
            s.Description,
            s.Side,
            s.Align,
            s.PopoverClass))).ToArray();

        await _importJSObjectReference.InvokeVoidAsync("drive", GetConfig(steps), stepIndex);
    }

    private Config GetConfig(DriverStep[]? steps = null)
    {
        var overlayColor = OverlayColor;
        if (string.IsNullOrWhiteSpace(overlayColor))
        {
            overlayColor = MasaBlazor.Theme.CurrentTheme.InverseSurface ?? (MasaBlazor.Theme.Dark ? "light" : "dark");
        }

        return new Config(
            steps,
            Animate,
            overlayColor,
            SmoothScroll,
            AllowClose,
            OverlayOpacity,
            StagePadding,
            StageRadius,
            AllowKeyboardControl,
            DisableActiveInteraction,
            PopoverClass,
            PopoverOffset,
            ShowButtons,
            // DisableButtons,
            ShowProgress,
            ProgressText,
            NextBtnText,
            PrevBtnText,
            DoneBtnText
        );
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _initStepsCts?.Cancel();

        if (_importJSObjectReference is not null)
        {
            await _importJSObjectReference.DisposeAsync();
        }
    }
}