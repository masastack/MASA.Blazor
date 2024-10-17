namespace Masa.Blazor;

public partial class MSticky : MasaComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public StringNumber? OffsetTop { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public string? Container { get; set; }

    [Parameter] public int ZIndex { get; set; }

    private static Block _block = new("m-sticky");

    private string? GetContentStyle()
    {
        return null;
    }
}