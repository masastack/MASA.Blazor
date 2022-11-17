namespace Masa.Blazor;

public class GridstackOptions
{
    /// <summary>
    /// Integer > 0 (default 12) which can change on the fly with column(N) API, or 'auto' for nested grids to size themselves to the parent grid container (to make sub-items are the same size). 
    /// </summary>
    public int Column { get; set; } = 12;

    /// <summary>
    /// disables the oneColumnMode when the grid width is less than minW (default: 'false')
    /// </summary>
    public bool DisableOneColumnMode { get; set; }

    /// <summary>
    /// disallows resizing of widgets (default: false).
    /// </summary>
    public bool DisableResize { get; set; }

    /// <summary>
    /// enable floating widgets (default: false)
    /// </summary>
    public bool Float { get; set; }

    /// <summary>
    /// gap size around grid item and content (default: 10px)
    /// </summary>
    public int Margin { get; set; } = 10;

    /// <summary>
    /// minimum rows amount which is handy to prevent grid from collapsing when empty. Default is 0. You can also do this with min-height CSS attribute on the grid div in pixels, which will round to the closest row.
    /// </summary>
    public int MinRow { get; set; }
    
    /// <summary>
    /// if true turns grid to RTL.
    /// </summary>
    public bool Rtl { get; set; }
}
