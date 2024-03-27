namespace Masa.Blazor;

/// <summary>
/// The theme provider for static SSR.
/// There is no need to use this component when using interactive at root.
/// </summary>
public partial class MSsrThemeProvider : IDisposable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    private readonly ThemeCssBuilder _themeCssBuilder = new();
    private string? _themeCss;

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

    private void BuildThemeCss(Theme theme)
    {
        _themeCss = _themeCssBuilder.Build(theme);
    }

    public void Dispose()
    {
        MasaBlazor.OnThemeChange -= MasaBlazorOnOnThemeChange;
    }
}
