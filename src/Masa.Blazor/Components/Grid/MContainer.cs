namespace Masa.Blazor;

public class MContainer : Container
{
    [Parameter] [MasaApiParameter("div")] public string Tag { get; set; } = "div";

    /// <summary>
    /// Removes viewport maximum-width size breakpoints
    /// </summary>
    [Parameter]
    public bool Fluid { get; set; }

    protected override string TagName => Tag;

    private Block _block = new("container");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Fluid).GenerateCssClasses();
    }
}