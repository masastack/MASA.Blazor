namespace Masa.Blazor;

public partial class MSsrThemeProvider : IDisposable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    private readonly ThemeCssBuilder _themeCssBuilder = new();
    private string? _themeCss;

    protected override void OnInitialized()
    {
        BuildThemeCss(MasaBlazor.Theme);
        MasaBlazor.OnThemeChange += MasaBlazorOnOnThemeChange;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var theme = await InitThemeAsync();
            if (theme != null)
            {
                MasaBlazor.Theme.Dark = theme == "dark";
                BuildThemeCss(MasaBlazor.Theme, theme == "dark");

                StateHasChanged();
            }
        }
    }

    private void MasaBlazorOnOnThemeChange(Theme theme)
    {
        BuildThemeCss(theme);

        _ = UpdateThemeAsync(theme.Dark);

        InvokeAsync(StateHasChanged);
    }

    private void BuildThemeCss(Theme theme) => BuildThemeCss(theme, theme.Dark);

    private void BuildThemeCss(Theme theme, bool isDark)
    {
        var themeOptions = isDark ? theme.Themes.Dark : theme.Themes.Light;
        _themeCss = _themeCssBuilder.Build(themeOptions);
    }

    private async Task UpdateThemeAsync(bool dark)
    {
        await JSRuntime.InvokeVoidAsync(JsInteropConstants.SsrUpdateTheme, dark);
    }

    private async Task<string?> InitThemeAsync()
    {
        return await JSRuntime.InvokeAsync<string?>(JsInteropConstants.SsrInitTheme);
    }

    public void Dispose()
    {
        MasaBlazor.OnThemeChange -= MasaBlazorOnOnThemeChange;
    }
}
