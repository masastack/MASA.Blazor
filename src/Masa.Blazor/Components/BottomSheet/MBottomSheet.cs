namespace Masa.Blazor;

public class MBottomSheet : MDialog
{
    [Parameter] public bool Inset { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Transition = "bottom-sheet-transition";

        return base.SetParametersAsync(parameters);
    }

    private static Block _block = new("m-bottom-sheet");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            new[]
            {
                _modifierBuilder.Add(Inset).Build()
            }
        );
    }
}