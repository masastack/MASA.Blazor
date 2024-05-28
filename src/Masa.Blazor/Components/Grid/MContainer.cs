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

    private static Block _block = new("container");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Fluid).Build();
    }
}