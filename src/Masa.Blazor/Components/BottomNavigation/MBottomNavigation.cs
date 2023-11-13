using BlazorComponent.Web;
using Element = BlazorComponent.Web.Element;

namespace Masa.Blazor;

public partial class MBottomNavigation : MItemGroup, IMeasurable, IScrollable, IAncestorRoutable, IAsyncDisposable
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
    [MassApiParameter(56)]
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
    [MassApiParameter(true)]
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

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Merge(cssBuilder =>
            {
                cssBuilder
                    .Add("m-bottom-navigation")
                    .AddIf("m-bottom-navigation--absolute", () => Absolute)
                    .AddIf("m-bottom-navigation--grow", () =>  Grow)
                    .AddIf("m-bottom-navigation--fixed",  () => !Absolute &&  (App || Fixed))
                    .AddIf("m-bottom-navigation--horizontal", () =>  Horizontal)
                    .AddIf("m-bottom-navigation--shift", () => Shift)
                    .AddTextColor(Color)
                    .AddBackgroundColor(BackgroundColor);
            }, styleBuilder =>
            {
                styleBuilder
                    .Add(() => IsActive ? "transform:none" : "transform:translateY(100%)")
                    .AddHeight(Height)
                    .AddMinHeight(MinHeight)
                    .AddMinWidth(MinWidth)
                    .AddMaxHeight(MaxHeight)
                    .AddMaxWidth(MaxWidth)
                    .AddWidth(Width)
                    .AddTextColor(Color)
                    .AddBackgroundColor(BackgroundColor);
            });
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
                await JsInvokeAsync(
                    JsInteropConstants.AddHtmlElementEventListener,
                    ScrollTarget,
                    "scroll",
                    DotNetObjectReference.Create(new Invoker(async () =>
                        await CreateEventCallback(async () => await _scroller!.OnScroll(ThresholdMet)).InvokeAsync()))
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
            var rect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, Ref);

            MasaBlazor.Application.Bottom = rect.ClientHeight;
        }
        else
        {
            MasaBlazor.Application.Bottom = 0;
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(ScrollTarget))
            {
                await JsInvokeAsync(JsInteropConstants.RemoveHtmlElementEventListener, ScrollTarget, "scroll");
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
