namespace Masa.Blazor;

/// <summary>
/// Root for application
/// </summary>
public partial class MApp : MasaComponentBase, IDefaultsProvider
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private Window Window { get; set; } = null!;

    [Inject] private IPopupProvider PopupProvider { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected ThemeCssBuilder ThemeCssBuilder { get; } = new();

    public IDictionary<string, IDictionary<string, object?>?>? Defaults => MasaBlazor.Defaults;

    protected bool IsDark => MasaBlazor?.Theme is { Dark: true };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        PopupProvider.StateChanged += OnStateChanged;
        MasaBlazor.OnThemeChange += OnThemeChange;
        MasaBlazor.RTLChanged += MasaBlazorOnRTLChanged;

        OnThemeChange(MasaBlazor.Theme);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OnJSInteropReadyAsync();
            StateHasChanged();
        }
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void MasaBlazorOnRTLChanged(object? sender, EventArgs e)
    {
        InvokeStateHasChanged();
    }

    private async Task OnJSInteropReadyAsync()
    {
        await MasaBlazor.Breakpoint.InitAsync(Js);
        await Window.AddResizeEventListenerAsync();
    }

    private void OnThemeChange(Theme theme)
    {
        var style = ThemeCssBuilder.Build(theme);
        InvokeAsync(async () =>
        {
            await Js.InvokeVoidAsync(JsInteropConstants.UpsertThemeStyle, "masa-blazor-theme-stylesheet", style);
            StateHasChanged();
        });
    }

    private Block _block = new("m-application");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(MasaBlazor.RTL ? "is-rtl" : "is-ltr")
            .AddTheme(IsDark, isIndependent: false)
            .GenerateCssClasses();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        PopupProvider.StateChanged -= OnStateChanged;
        MasaBlazor.OnThemeChange -= OnThemeChange;
        MasaBlazor.RTLChanged -= MasaBlazorOnRTLChanged;

        return base.DisposeAsyncCore();
    }
}