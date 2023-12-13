namespace Masa.Blazor;

public partial class MSsrThemeProvider : IDisposable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    private readonly ThemeCssBuilder _themeCssBuilder = new();
    private string? _themeCss;
    private string? _ssrCss;

    protected override void OnInitialized()
    {
        BuildThemeCss(MasaBlazor.Theme);
        MasaBlazor.OnThemeChange += MasaBlazorOnOnThemeChange;
    }

    private void MasaBlazorOnOnThemeChange(Theme theme)
    {
        BuildThemeCss(theme);

        InvokeAsync(StateHasChanged);
    }

    private void BuildThemeCss(Theme theme) => BuildThemeCss(theme, theme.Dark);

    private void BuildThemeCss(Theme theme, bool isDark)
    {
        var themeOptions = isDark ? theme.Themes.Dark : theme.Themes.Light;
        _themeCss = _themeCssBuilder.Build(themeOptions);
    }

    public void Dispose()
    {
        MasaBlazor.OnThemeChange -= MasaBlazorOnOnThemeChange;
    }
}
