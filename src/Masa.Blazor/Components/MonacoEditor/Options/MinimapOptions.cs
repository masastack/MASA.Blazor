namespace Masa.Blazor;

public class MinimapOptions
{
    public bool Enabled { get; set; } = true;
    public int MaxColumn { get; set; } = 120;
    public bool RenderCharacters { get; set; } = true;
    public string ShowSlider { get; set; } = "mouseover";
    public string Side { get; set; } = "right";
}