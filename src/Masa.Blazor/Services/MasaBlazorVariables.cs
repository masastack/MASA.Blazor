namespace Masa.Blazor;

public class MasaBlazorVariables : Variables
{
    public static Breakpoint Breakpoint { get; set; }

    /// <summary>
    /// The width of the page.
    /// </summary>
    public static double Width { get; internal set; }

    /// <summary>
    /// The height of the page,
    /// </summary>
    public static double Height { get; internal set; }
}
