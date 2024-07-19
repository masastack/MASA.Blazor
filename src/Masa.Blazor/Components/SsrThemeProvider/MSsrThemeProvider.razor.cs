namespace Masa.Blazor;

/// <summary>
/// The theme provider for static SSR.
/// There is no need to use this component when using interactive at root.
/// </summary>
public partial class MSsrThemeProvider : IDisposable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    protected override void OnInitialized()
    {
        MasaBlazor.OnThemeChange += MasaBlazorOnOnThemeChange;
    }

    private void MasaBlazorOnOnThemeChange(Theme theme)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        MasaBlazor.OnThemeChange -= MasaBlazorOnOnThemeChange;
    }
}