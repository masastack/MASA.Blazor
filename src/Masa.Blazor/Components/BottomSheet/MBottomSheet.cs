namespace Masa.Blazor;

public class MBottomSheet : MDialog
{
    [Parameter] public bool Inset { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Transition = "bottom-sheet-transition";

        return base.SetParametersAsync(parameters);
    }

    private Block _block = new("m-bottom-sheet");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(_block.Modifier(Inset).GenerateCssClasses());
    }
}