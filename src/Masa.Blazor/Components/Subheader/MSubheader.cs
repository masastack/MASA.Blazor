namespace Masa.Blazor;

public class MSubheader : ThemeContainer
{
    [Parameter] public bool Inset { get; set; }

    private static Block _block = new("m-subheader");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Inset).AddTheme(ComputedTheme).Build();
    }
}