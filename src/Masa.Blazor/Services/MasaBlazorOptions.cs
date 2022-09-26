namespace Masa.Blazor;

public class MasaBlazorOptions
{
    public MasaBlazorOptions(Breakpoint breakpoint, Theme theme)
    {
        Breakpoint = breakpoint;
        Theme = theme;
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

    internal Theme Theme { get; }

    internal Breakpoint Breakpoint { get; }
}
