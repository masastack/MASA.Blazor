namespace Masa.Blazor.Mixins.Menuable;

public class MultipleResult
{
    public WindowAndDocument WindowAndDocument { get; set; } = new();

    public MenuableDimensions Dimensions { get; set; } = new();

    public int ZIndex { get; set; }
}