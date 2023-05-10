namespace Masa.Blazor.Presets;

public class PDrawer : ModalBase
{
    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    private string ComputedClass => Left ? "m-dialog--drawer-left" : "m-dialog--drawer-right";

    private string ComputedContentHeight => HasActions ? "height: calc(100% - 116px)" : "height: calc(100% - 63px)";

    private string ComputedTransition => Left ? "dialog-left-transition" : "dialog-right-transition";

    protected override string StaticClass => $"m-dialog--drawer {ComputedClass}";

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Class = "";
        Elevation = 0;
        Height = "100%";
        Right = true;
        Width = "33.33%";

        await base.SetParametersAsync(parameters);
    }

    protected override void OnParametersSet()
    {
        BodyStyle = $"overflow-y: auto;padding: 24px;{ComputedContentHeight}";

        Transition = ComputedTransition;

        base.OnParametersSet();
    }
}