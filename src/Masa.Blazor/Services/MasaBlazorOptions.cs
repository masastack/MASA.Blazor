#nullable enable

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

    public void ConfigureTheme(Action<Theme> configure)
    {
        configure.Invoke(Theme);
    }

    public void ConfigureBreakpoint(Action<Breakpoint> configure)
    {
        configure.Invoke(Breakpoint);
    }

    public void ConfigureIcons(Action<Icons> configure)
    {
        configure.Invoke(Icons);
    }

    public IDictionary<string, IDictionary<string, object?>?>? Defaults { get; set; }

    internal Theme Theme { get; }

    internal Breakpoint Breakpoint { get; }

    internal Icons Icons { get; }
}
