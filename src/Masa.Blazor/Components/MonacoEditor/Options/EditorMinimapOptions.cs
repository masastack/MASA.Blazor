namespace Masa.Blazor.Components.MonacoEditor.Options;

public class EditorMinimapOptions
{
    /// <summary>
    /// Enable the rendering of the minimap.
    /// Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Control the rendering of minimap.
    /// </summary>
    public bool Autohide { get; set; }

    /// <summary>
    /// Control the side of the minimap in editor.
    /// Defaults to 'right'.
    /// </summary>
    public string Side { get; set; } = "right";

    /// <summary>
    /// Control the minimap rendering mode.
    /// Defaults to 'actual'.
    /// </summary>
    public string Size { get; set; } = "actual";

    /// <summary>
    /// Control the rendering of the minimap slider.
    /// Defaults to 'mouseover'.
    /// </summary>
    public string ShowSlider { get; set; } = "mouseover";

    /// <summary>
    /// Render the actual text on a line (as opposed to color blocks).
    /// Defaults to true.
    /// </summary>
    public bool RenderCharacters { get; set; } = true;

    /// <summary>
    /// Limit the width of the minimap to render at most a certain number of columns.
    /// Defaults to 120.
    /// </summary>
    public int MaxColumn { get; set; } = 120;

    /// <summary>
    /// Relative size of the font in the minimap. Defaults to 1.
    /// </summary>
    public int Scale { get; set; } = 1;
}