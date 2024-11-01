namespace Masa.Blazor;

public class TextFieldNumberProperty
{
    public decimal? Min { get; set; }

    public decimal? Max { get; set; }

    public decimal Step { get; set; } = 1;

    //[Obsolete("Use ControlVisibility instead")]
    public bool HideControl { get; set; }

    public int? Precision { get; set; }
    
    public string? PrecisionFormat => Precision.HasValue ? "F" + Precision.Value : null;

    // TODO: 
    // public TextFieldNumberControlVisibility ControlVisibility { get; set; } = TextFieldNumberControlVisibility.Hover;
}

public enum TextFieldNumberControlVisibility
{
    Hover,
    Never,
    Always
}