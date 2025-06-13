namespace Masa.Blazor;

public class ThemeOptions
{
    public bool IsDarkScheme { get; internal set; }

    public string ColorScheme => IsDarkScheme ? "dark" : "normal";

    public string? CombinePrefix { get; set; }

    public string? Primary { get; set; }

    public string? OnPrimary { get; set; }

    public string? Secondary { get; set; }

    public string? OnSecondary { get; set; }

    /// <summary>
    /// Teritary
    /// </summary>
    public string? Accent { get; set; }

    public string? OnAccent { get; set; }

    public string? Error { get; set; }

    public string? OnError { get; set; }

    public string? Info { get; set; }

    public string? OnInfo { get; set; }

    public string? Success { get; set; }

    public string? OnSuccess { get; set; }

    public string? Warning { get; set; }

    public string? OnWarning { get; set; }

    public string? SurfaceDim { get; set; }

    public string? Surface { get; set; }

    public string? SurfaceBright { get; set; }

    public string? SurfaceContainerLowest { get; set; }

    public string? SurfaceContainerLow { get; set; }

    public string? SurfaceContainer { get; set; }

    public string? SurfaceContainerHigh { get; set; }

    public string? SurfaceContainerHighest { get; set; }

    public string? OnSurface { get; set; }

    public string? InverseSurface { get; set; }

    public string? InverseOnSurface { get; set; }

    public string? InversePrimary { get; set; }

    // TODO: Scrim, Shadow

    public ThemeVariables Variables { get; } = MasaBlazorPreset.GetThemeVariables();

    public Dictionary<string, ColorPairing> UserDefined { get; } = new();

    public ThemeOptions ShallowCopy()
    {
        return (ThemeOptions)this.MemberwiseClone();
    }
}