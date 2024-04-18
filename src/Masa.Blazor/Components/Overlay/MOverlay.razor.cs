using BlazorComponent.JSInterop.ScrollStrategy;

namespace Masa.Blazor;

public partial class MOverlay : IThemeable
{
    [Inject] private ScrollStrategyJSModule ScrollStrategyJSModule { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public bool Value
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool Contained { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool Eager { get; set; }

    [Parameter] public StringNumber? Opacity { get; set; }

    [Parameter] public string? ScrimClass { get; set; }

    [Parameter] [MasaApiParameter(5)] public int ZIndex { get; set; } = 5;

    [Parameter] [MasaApiParameter(true)] public bool Dark { get; set; } = true;

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    private readonly Block _block = new("m-overlay");

    private bool _booted;

    public ElementReference ContentRef { get; private set; }

    private string? ComputedColor => Color ?? MasaBlazor.Theme.CurrentTheme.OnSurface;

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Value), ValueChangeCallback);
    }

    private async Task ValueChangeCallback()
    {
        if (Value)
        {
            _ = NextTickIf(async () => { await HideScroll(); }, () => ContentRef.Context is null);
        }
        else
        {
            await ShowScroll();
        }
    }

    private bool _isActive;

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!_booted)
        {
            _booted = true;
            _isActive = Value;
        }
        else
        {
            _isActive = Value;
        }


#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier("active", Value)
            .And("absolute", Absolute || Contained)
            .And(Contained)
            .AddTheme(IsDark, IndependentTheme)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return $"z-index: {ZIndex}";
        if (Opacity != null)
        {
            yield return $"--m-overlay-opacity: {Opacity.TryGetNumber().number}";
        }
    }

    private async Task HideScroll()
    {
        if (!ScrollStrategyJSModule.Initialized)
        {
            await ScrollStrategyJSModule.InitializeAsync(Ref, ContentRef,
                new(ScrollStrategy.Block, Contained));
        }

        await ScrollStrategyJSModule.BindAsync();
    }

    private async Task ShowScroll()
    {
        await ScrollStrategyJSModule.UnbindAsync();
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (ScrollStrategyJSModule.Initialized)
        {
            await ShowScroll();
        }
    }
}