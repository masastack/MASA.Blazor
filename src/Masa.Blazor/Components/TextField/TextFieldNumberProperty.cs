namespace Masa.Blazor;

public class TextFieldNumberProperty
{
    public decimal? Min { get; set; }

    public decimal? Max { get; set; }

    public decimal Step { get; set; } = 1;

    public bool HideControl { get; set; }
}