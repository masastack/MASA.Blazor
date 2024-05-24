using BlazorComponent.Web;
using Masa.Blazor.Mixins;
using Element = BlazorComponent.Web.Element;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MBottomNavigation : MItemGroup, IMeasurable, IScrollable, IAncestorRoutable
{
    public MBottomNavigation() : base(GroupType.ButtonGroup)
    {
    }

    [Inject]
    private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public bool Absolute { get; set; }

    [Parameter]
    public bool App { get; set; }

    [Parameter]
    public bool Fixed { get; set; }

    [Parameter]
    public bool Grow { get; set; }

    [Parameter]
    [MasaApiParameter(56)]
    public StringNumber? Height { get; set; } = 56;

    [Parameter]
    public StringNumber? MaxHeight { get; set; }

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public StringNumber? MinHeight { get; set; }

    [Parameter]
    public StringNumber? MinWidth { get; set; }

    [Parameter]
    public string? ScrollTarget { get; set; } = "window";

    [Parameter]
    public double ScrollThreshold { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    public bool HideOnScroll { get; set; }

    [Parameter]
    public bool Horizontal { get; set; }

    [Parameter]
    public bool Shift { get; set; }

    [Parameter]
    [MasaApiParameter(true)]
    public bool InputValue { get; set; } = true;

    [Parameter]
    public EventCallback<bool> InputValueChanged { get; set; }

    [Parameter]
    public string? BackgroundColor { get; set; }

    [Parameter]
    public string? Color { get; set; }

    [Parameter]
    public bool Routable { get; set; }

    private Scroller? _scroller;
    private bool _haveRendered;
    private CancellationTokenSource _jsGetDomInfoCts = new();

    public bool CanScroll => HideOnScroll || !InputValue;

    private bool IsActive
    {
        get => _scroller?.IsActive ?? true;
        set
        {
            if (_scroller is not null)
            {
                _scroller.IsActive = value;
            }
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _scroller = new Scroller(this);
    }

    private Block _block = new("m-bottom-navigation");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Absolute)
                .And(Grow)
                .And("fixed", !Absolute && (App || Fixed))
                .And(Horizontal)
                .And(Shift)
                .AddTextColor(Color)
                .AddBackgroundColor(BackgroundColor)
                .GenerateCssClasses()
        );
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return base.BuildComponentStyle().Concat(
            StyleBuilder.Create()
                .AddHeight(Height)
                .AddMinHeight(MinHeight)
                .AddMinWidth(MinWidth)
                .AddMaxHeight(MaxHeight)
                .AddMaxWidth(MaxWidth)
                .AddWidth(Width)
                .AddTextColor(Color)
                .AddBackgroundColor(BackgroundColor)
                .Add("transform", $"{(IsActive ? "none" : "translateY(100%)")}")
                .GenerateCssStyles()
        );
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Mandatory = Mandatory || Value is not null;

        IsActive = InputValue;

        _scroller!.ScrollThreshold = ScrollThreshold;

        if (_haveRendered)
        {
            await UpdateApplication();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        _haveRendered = true;

        if (firstRender)
        {
            if (!string.IsNullOrWhiteSpace(ScrollTarget) && CanScroll)
            {
                await Js.InvokeVoidAsync(
                    JsInteropConstants.AddHtmlElementEventListener,
                    ScrollTarget,
                    "scroll",
                    DotNetObjectReference.Create(new Invoker(async () =>
                        await EventCallback.Factory.Create(this, async () => await _scroller!.OnScroll(ThresholdMet)).InvokeAsync()))
                );
            }

            await UpdateApplication();
            StateHasChanged();
        }
    }

    private void ThresholdMet(Scroller scrollable)
    {
        if (HideOnScroll)
        {
            IsActive = !scrollable.IsScrollingUp || scrollable.CurrentScroll > scrollable.ComputedScrollThreshold;

            if (InputValueChanged.HasDelegate)
            {
                _ = InputValueChanged.InvokeAsync(IsActive);
            }
        }

        if (scrollable.CurrentThreshold < scrollable.ComputedScrollThreshold) return;

        scrollable.SavedScroll = scrollable.CurrentScroll;
    }

    private async Task UpdateApplication()
    {
        // TODO: implement applicationable

        if (!App)
        {
            return;
        }

        if (IsActive)
        {
            var rect = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, _jsGetDomInfoCts.Token, Ref);

            MasaBlazor.Application.Bottom = rect.ClientHeight;
        }
        else
        {
            MasaBlazor.Application.Bottom = 0;
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _jsGetDomInfoCts.Cancel();

        if (App)
        {
            MasaBlazor.Application.Bottom = 0;
        }

        if (!string.IsNullOrWhiteSpace(ScrollTarget))
        {
            await Js.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, ScrollTarget, "scroll");
        }
    }
}