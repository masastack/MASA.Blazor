namespace Masa.Blazor;

public class SSROptions
{
    /// <summary>
    /// Indicates whether globally interactive rendering is enabled.
    /// More details: https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0#apply-a-render-mode-to-the-entire-app.
    /// </summary>
    public bool IsAllInteractive { get; set; }
    
    /// <summary>
    /// The height of SystemBar
    /// </summary>
    public double Bar { get; set; }

    /// <summary>
    /// The height of AppBar
    /// </summary>
    public double Top { get; set; }

    /// <summary>
    /// The width of right NavigationDrawer
    /// </summary>
    public double Right { get; set; }

    /// <summary>
    /// The height of BottomNavigation
    /// </summary>
    public double Bottom { get; set; }

    /// <summary>
    /// The width of left NavigationDrawer
    /// </summary>
    public double Left { get; set; }

    /// <summary>
    /// The height of Footer
    /// </summary>
    public double Footer { get; set; }

    /// <summary>
    /// The height of InsetFooter
    /// </summary>
    public double InsetFooter { get; set; }

    public double MainPaddingTop => Top + Bar;

    public double MainPaddingRight => Right;

    public double MainPaddingBottom => Footer + InsetFooter + Bottom;

    public double MainPaddingLeft => Left;
}
