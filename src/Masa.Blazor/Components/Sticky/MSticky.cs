namespace Masa.Blazor.Components.Sticky;

public class MSticky : MasaComponentBase
{
    [Parameter] public RenderFragment<bool>? ChildContent { get; set; }

    [Parameter] public double Top { get; set; }

    [Parameter] public double Bottom { get; set; }

    private static Block _block = new("m-sticky");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    private bool _isSticky;

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("sticky", _isSticky).Build();
    }
    
    
}