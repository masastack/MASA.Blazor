namespace Masa.Blazor;

public partial class MBottomSheet : MDialog
{
    [Parameter] public bool Inset { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        Transition = "bottom-sheet-transition";

        return base.SetParametersAsync(parameters);
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Merge("innerContent", css =>
            {
                css.Add("m-bottom-sheet")
                   .AddIf("m-bottom-sheet--inset", () => Inset);
            });
    }
}