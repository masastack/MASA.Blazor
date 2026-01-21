namespace Masa.Blazor;

internal static class MasaBlazorPreset
{
    internal static ThemeOptions GetLightTheme() => new()
    {
        IsDarkScheme = false,
        CombinePrefix = ".m-application",
        Primary = "#4318ff",
        Secondary = "#5c5a85",
        Accent = "#8f456f",
        Error = "#FF5449",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
        OnPrimary = "#FFFFFF",
        OnSecondary = "#FFFFFF",
        OnAccent = "#FFFFFF",
        OnError = "#FFFFFF",
        OnInfo = "#FFFFFF",
        OnSuccess = "#FFFFFF",
        OnWarning = "#FFFFFF",
        OnSurface = "#000000",
        SurfaceDim = "#FFFFFF",
        Surface = "#FFFFFF",
        SurfaceBright = "#FFFFFF",
        SurfaceContainerLowest = "#FFFFFF",
        SurfaceContainerLow = "#FFFFFF",
        SurfaceContainer = "#FFFFFF",
        SurfaceContainerHigh = "#f5f5f5",
        SurfaceContainerHighest = "#e0e0e0",
        InverseSurface = "#302f3b",
        InverseOnSurface = "#f3eeff",
        InversePrimary = "#c5c0ff",
        Stripe = "#ebebeb",
    };

    internal static ThemeOptions GetDarkTheme() => new()
    {
        IsDarkScheme = true,
        CombinePrefix = ".m-application",
        Primary = "#c5c0ff",
        Secondary = "#928F9F",
        Accent = "#A98897",
        Error = "#ffb4ab",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
        OnPrimary = "#2400a2",
        OnSecondary = "#302f3c",
        OnAccent = "#412935",
        OnError = "#690005",
        OnInfo = "#FFFFFF",
        OnSuccess = "#FFFFFF",
        OnWarning = "#FFFFFF",
        OnSurface = "#FFFFFF",
        SurfaceDim = "#121212",
        Surface = "#121212",
        SurfaceBright = "#121212",
        SurfaceContainerLowest = "#111111",
        SurfaceContainerLow = "#1E1E1E",
        SurfaceContainer = "#2B2B2B",
        SurfaceContainerHigh = "#383838",
        SurfaceContainerHighest = "#454545",
        InverseSurface = "#e4e0f0",
        InverseOnSurface = "#302f3b",
        InversePrimary = "#4f33ff",
        Stripe = "#424242"
    };

    internal static Breakpoint GetBreakpoint() => new()
    {
        MobileBreakpoint = 1264,
        ScrollBarWidth = 16,
        Thresholds = new BreakpointThresholds()
        {
            Xs = 600,
            Sm = 960,
            Md = 1280,
            Lg = 1920
        }
    };

    internal static Icons GetIcons() => new(IconSet.MaterialDesignIcons, new MaterialDesignIconsAliases());

    internal static Theme GetTheme() => new(GetLightTheme(), GetDarkTheme());
    
    internal static ThemeVariables GetThemeVariables() => new()
    {
        BorderOpacity = 0.12f,
        IdleOpacity = 0.04f,
        HoverOpacity = 0.04f,
        FocusOpacity = 0.12f,
        DisabledOpacity = 0.38f,
        ActivatedOpacity = 0.12f,
        HighlightOpacity = 0.16f,
        HighEmphasisOpacity = 0.87f,
        MediumEmphasisOpacity = 0.6f,
        LowEmphasisOpacity = 0.38f
    };
}