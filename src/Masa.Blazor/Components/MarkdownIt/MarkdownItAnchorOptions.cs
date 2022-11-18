namespace Masa.Blazor;

public class MarkdownItAnchorOptions
{
    /// <summary>
    /// Minimum level to apply anchors, or array of selected levels. Default value is 1. 
    /// </summary>
    public int Level { get; set; } = 1;

    /// <summary>
    /// Enable permalink
    /// </summary>
    public bool Permalink { get; set; } = true;

    /// <summary>
    /// The symbol in the permalink anchor.	
    /// </summary>
    public string PermalinkSymbol { get; set; } = "#";

    /// <summary>
    /// The class of the permalink anchor.	
    /// </summary>
    public string PermalinkClass { get; set; } = "header-anchor";
}
