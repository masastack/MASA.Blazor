using System.Diagnostics.CodeAnalysis;
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

    [Inject] private I18n I18n { get; set; } = null!;

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
    [MasaApiParameter(OverlayClickBehavior.Close, ReleasedIn = "v1.10.0")]
    public OverlayClickBehavior OverlayClickBehavior { get; set; }

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
    [MasaApiParameter("[\"previous\", \"next\", \"close\"]", ReleasedIn = "v1.10.0")]
    public PopoverButton[] ShowButtons { get; set; } =
        [PopoverButton.Previous, PopoverButton.Next, PopoverButton.Close];

    [Parameter] public PopoverButton[] DisableButtons { get; set; } = [];

    [Parameter] public bool ShowProgress { get; set; }

    [Parameter] public string? ProgressText { get; set; }

    [Parameter]
    [MasaApiParameter(DefaultNextBtnText)]
    public string? NextBtnText { get; set; } = DefaultNextBtnText;

    [Parameter]
    [MasaApiParameter(DefaultPrevBtnText)]
    public string? PrevBtnText { get; set; } = DefaultPrevBtnText;

    [Parameter]
    [MasaApiParameter(DefaultDoneBtnText)]
    public string? DoneBtnText { get; set; } = DefaultDoneBtnText;

    /// <summary>
    /// Event callback for when the next button is clicked.
    /// The argument is the selector of the current step.
    /// </summary>
    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback<string> OnNextClick { get; set; }

    /// <summary>
    /// The event callback for when the previous button is clicked.
    /// The argument is the selector of the current step.
    /// </summary>
    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback<string> OnPrevClick { get; set; }

    /// <summary>
    /// The event callback for when the close button is clicked.
    /// The argument is the selector of the current step.
    /// </summary>
    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback<string> OnCloseClick { get; set; }

    /// <summary>
    /// The event callback for when the last next button is clicked.
    /// This is typically used to finalize the driver tour.
    /// </summary>
    [Parameter] [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public EventCallback OnDoneClick { get; set; }

    private const string DefaultNextBtnText = "$masaBlazor.driverjs.next";
    private const string DefaultPrevBtnText = "$masaBlazor.driverjs.prev";
    private const string DefaultDoneBtnText = "$masaBlazor.driverjs.done";

    private bool _initSteps;
    private bool _hasRendered;
    private CancellationTokenSource? _initStepsCts;
    private List<IDriverJSStep> _steps = [];
    private IJSObjectReference? _importJSObjectReference;
    private DotNetObjectReference<MDriverJS> _dotNetObjectReference = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var importJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.JSComponents.DriverJS/MDriverJS.js?v=1.10.5").ConfigureAwait(false);
            _importJSObjectReference
                = await importJSObjectReference.InvokeAsync<IJSObjectReference>("init", GetConfig(),
                    _dotNetObjectReference);
            await importJSObjectReference.DisposeAsync();

            _hasRendered = true;

            if (_initSteps == false)
            {
                await InitDriverJsAsync();
            }
        }
    }

    /// <summary>
    /// Highlight an element.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="popover"></param>
    [MasaApiParameter]
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    public void Highlight(string selector, Action<Popover> popover)
    {
        var defaultPopover = new Popover()
        {
            ShowButtons = [PopoverButton.Close],
            NextBtnText = I18n.T(DefaultDoneBtnText), // Use done button for single highlight
            PrevBtnText = I18n.T(DefaultPrevBtnText),
        };

        popover(defaultPopover);

        var step = new DriverStep(selector, defaultPopover);

        _ = _importJSObjectReference.TryInvokeVoidAsync("highlight", step);
    }

    /// <summary>
    /// Start the driver.
    /// </summary>
    /// <param name="stepIndex"></param>
    [MasaApiParameter]
    public void Drive(int stepIndex = 0) => _ = DriveAsync(stepIndex);

    internal async Task AddStep(IDriverJSStep step)
    {
        _steps.Add(step);

        await InitDriverJsAsync();
    }

    [JSInvokable]
    public async Task NextClick(string selector)
    {
        var isLast = _steps.Count == 0 || _steps.Last().Element == selector;
        if (isLast)
        {
            await OnDoneClick.InvokeAsync();
        }
        else
        {
            await OnNextClick.InvokeAsync(selector);
        }
    }

    [JSInvokable]
    public async Task PrevClick(string selector)
    {
        await OnPrevClick.InvokeAsync(selector);
    }

    [JSInvokable]
    public async Task CloseClick(string selector)
    {
        await OnCloseClick.InvokeAsync(selector);
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

            await UpdateStepsAsync();
        }
        catch (TaskCanceledException)
        {
            // ignore
        }
    }

    private async Task UpdateStepsAsync()
    {
        if (_importJSObjectReference is null) return;

        var steps = _steps.Select(s =>
        {
            var popover = s as Popover;
            return new DriverStep(s.Element, popover!);
        }).ToArray();

        await _importJSObjectReference.InvokeVoidAsync("updateConfig", GetConfig(steps), AutoDrive);
    }

    private async Task DriveAsync(int stepIndex)
    {
        if (_importJSObjectReference is null) return;

        await _importJSObjectReference.InvokeVoidAsync("drive", stepIndex);
    }

    private Config GetConfig(DriverStep[]? steps = null)
    {
        var overlayColor = OverlayColor;
        if (string.IsNullOrWhiteSpace(overlayColor))
        {
            overlayColor = MasaBlazor.Theme.CurrentTheme.InverseSurface ??
                           (MasaBlazor.Theme.CurrentTheme.IsDarkScheme ? "light" : "dark");
        }

        var nextBtnText = I18n.T(DefaultNextBtnText);
        var prevBtnText = I18n.T(DefaultPrevBtnText);
        var doneBtnText = I18n.T(DefaultDoneBtnText);

        return new Config(
            steps,
            Animate,
            overlayColor,
            SmoothScroll,
            AllowClose,
            OverlayOpacity,
            OverlayClickBehavior,
            StagePadding,
            StageRadius,
            AllowKeyboardControl,
            DisableActiveInteraction,
            PopoverClass,
            PopoverOffset,
            ShowButtons,
            DisableButtons,
            ShowProgress,
            ProgressText,
            nextBtnText,
            prevBtnText,
            doneBtnText
        );
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _initStepsCts?.Cancel();

        if (_importJSObjectReference is not null)
        {
            await _importJSObjectReference.InvokeVoidAsync("dispose");
            await _importJSObjectReference.DisposeAsync();
            _importJSObjectReference = null;
        }
    }
}