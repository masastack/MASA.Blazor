namespace Masa.Blazor;

public class MasaBlazorOptions : BlazorComponentOptions
{
    public MasaBlazorOptions()
    {
        Breakpoint = MasaBlazorPreset.Breakpoint;
        Theme = MasaBlazorPreset.Theme;
        Icons = MasaBlazorPreset.Icons;
    }

    public bool RTL { get; set; }

    public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; set; }

    internal Theme Theme { get; }

    internal Breakpoint Breakpoint { get; }

    internal Icons Icons { get; }

    public void ConfigureTheme(Action<Theme> configure)
    {
        configure.Invoke(Theme);
    }

    public void ConfigureBreakpoint(Action<Breakpoint> configure)
    {
        configure.Invoke(Breakpoint);
    }

    public void ConfigureIcons(IconSet defaultSet, Action<IconAliases>? aliasesConfigure = null)
    {
        Icons.DefaultSet = defaultSet;
        Icons.Aliases = defaultSet switch
        {
            IconSet.MaterialDesignIcons => new MaterialDesignIconsAliases(),
            IconSet.MaterialDesign => new MaterialDesignAliases(),
            IconSet.FontAwesome => new FontAwesomeAliases(),
            IconSet.FontAwesome4 => new FontAwesome4Aliases(),
            _ => throw new ArgumentOutOfRangeException(nameof(defaultSet), defaultSet, null)
        };

        aliasesConfigure?.Invoke(Icons.Aliases);
    }

    public void ConfigureIcons(string name, IconAliases aliases)
    {
        Icons.DefaultSet = null;
        Icons.Name = name;
        Icons.Aliases = aliases;
    }
}
