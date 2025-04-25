namespace Masa.Blazor;

/// <summary>
/// Root for application
/// </summary>
public partial class MApp : MasaComponentBase, IDefaultsProvider
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private IPopupService InternalPopupService { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    public IDictionary<string, IDictionary<string, object?>?>? Defaults => MasaBlazor.Defaults;

    private bool IsDark => MasaBlazor.Theme is { DefaultTheme: "dark" };

    private string Theme => MasaBlazor.Theme.DefaultTheme;

    private PopupService PopupService => (PopupService)InternalPopupService;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        PopupService.StateChanged += OnStateChanged;
        MasaBlazor.OnThemeChange += OnThemeChange;
        MasaBlazor.RTLChanged += OnRTLChanged;
        MasaBlazor.DefaultsChanged += OnDefaultsChanged;

        _ = UpsertThemeStyle(MasaBlazor.Theme);
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void OnRTLChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void OnDefaultsChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void OnThemeChange(Theme theme)
    {
        InvokeAsync(async () =>
        {
            await UpsertThemeStyle(theme);
            StateHasChanged();
        });
    }

    private async Task UpsertThemeStyle(Theme theme)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.UpsertThemeStyle, "masa-blazor-theme-stylesheet", ThemeCssBuilder.Build(theme));
    }

    private static Block _block = new("m-application");
    private ModifierBuilder _blockModifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _blockModifierBuilder
            .Add(MasaBlazor.RTL ? "is-rtl" : "is-ltr")
            .AddTheme(Theme)
            .Build();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        PopupService.StateChanged -= OnStateChanged;
        MasaBlazor.OnThemeChange -= OnThemeChange;
        MasaBlazor.RTLChanged -= OnRTLChanged;
        MasaBlazor.DefaultsChanged -= OnDefaultsChanged;

        return base.DisposeAsyncCore();
    }
}