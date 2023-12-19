namespace Masa.Blazor;

public class MasaBlazorOptions : BlazorComponentOptions
{
    public MasaBlazorOptions()
    {
        Breakpoint = MasaBlazorPreset.Breakpoint;
        Theme = MasaBlazorPreset.Theme;
        Icons = MasaBlazorPreset.Icons;
    }

    public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; set; }

    public bool RTL { get; set; }

    internal Breakpoint Breakpoint { get; }

    internal Icons Icons { get; }

    internal SSROptions? SSR { get; private set; }

    internal Theme Theme { get; }

    public void ConfigureBreakpoint(Action<BreakpointOptions> configure)
    {
        var defaultBreakpointOptions = new BreakpointOptions()
        {
            MobileBreakpoint = MasaBlazorPreset.Breakpoint.MobileBreakpoint,
            ScrollBarWidth = MasaBlazorPreset.Breakpoint.ScrollBarWidth,
            Thresholds = MasaBlazorPreset.Breakpoint.Thresholds
        };
        configure.Invoke(defaultBreakpointOptions);

        Breakpoint.MobileBreakpoint = defaultBreakpointOptions.MobileBreakpoint;
        Breakpoint.ScrollBarWidth = defaultBreakpointOptions.ScrollBarWidth;
        Breakpoint.Thresholds = defaultBreakpointOptions.Thresholds;
    }

    public void ConfigureIcons(IconSet defaultSet, Action<IconAliases>? aliasesConfigure = null)
    {
        Icons.DefaultSet = defaultSet;
        Icons.Aliases = defaultSet switch
        {
            IconSet.MaterialDesignIcons => new MaterialDesignIconsAliases(),
            IconSet.MaterialDesign      => new MaterialDesignAliases(),
            IconSet.FontAwesome         => new FontAwesomeAliases(),
            IconSet.FontAwesome4        => new FontAwesome4Aliases(),
            _                           => throw new ArgumentOutOfRangeException(nameof(defaultSet), defaultSet, null)
        };

        aliasesConfigure?.Invoke(Icons.Aliases);
    }

    public void ConfigureIcons(string name, IconAliases aliases)
    {
        Icons.DefaultSet = null;
        Icons.Name = name;
        Icons.Aliases = aliases;
    }

    /// <summary>
    /// Configure static SSR options, do not set this when use interactive at root.
    /// </summary>
    public void ConfigureSsr() => ConfigureSsr(_ => { });

    /// <summary>
    /// Configure static SSR options, do not set this when use interactive at root.
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureSsr(Action<SSROptions> configure)
    {
        SSR = new SSROptions();
        configure.Invoke(SSR);
    }

    public void ConfigureTheme(Action<Theme> configure)
    {
        configure.Invoke(Theme);
    }
}
