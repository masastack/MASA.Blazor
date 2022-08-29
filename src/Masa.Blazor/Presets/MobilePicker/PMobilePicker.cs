using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobilePicker<TColumn, TColumnItem, TColumnItemValue> : MobilePickerBase<TColumn, TColumnItem, TColumnItemValue, List<TColumnItemValue>>
{
    [Parameter]
    public override List<TColumn> Columns { get; set; }

    [Parameter]
    public override Func<TColumnItem, string> ItemText { get; set; }

    [Parameter]
    public override Func<TColumnItem, TColumnItemValue> ItemValue { get; set; }

    [Parameter]
    public override Func<TColumnItem, List<TColumnItem>> ItemChildren { get; set; }

    [Parameter]
    public override Func<TColumnItem, bool> ItemDisabled { get; set; }

    protected override string ClassPrefix => "p-mobile-picker";
}
