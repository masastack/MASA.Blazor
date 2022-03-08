using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Presets;

public class ModalButton : MButton
{
    [Parameter]
    public ModalButtonProps Props { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Block = Props.Block;
        Class = Props.Class;
        Color = Props.Color;
        Depressed = Props.Depressed;
        Large = Props.Large;
        Outlined = Props.Outlined;
        Plain = Props.Plain;
        Rounded = Props.Rounded;
        Small = Props.Small;
        Style = Props.Style;
        Text = Props.Text;
        Tile = Props.Tile;
        XLarge = Props.XLarge;
        XSmall = Props.XSmall;
    }
}