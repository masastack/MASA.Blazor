namespace Masa.Blazor.Components.Image;

public class SrcObject
{
    public string? Src { get; set; }

    // TODO: support for SrcSet
    public string? Srcset { get; set; }

    public string? LazySrc { get; set; }

    public StringNumber? Aspect { get; set; }
}
