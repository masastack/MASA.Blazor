using Masa.Blazor.Presets.MobilePicker;

namespace Masa.Blazor.Presets;

public class PMobilePicker<TColumn, TColumnItem, TColumnItemValue> : MobilePickerBase<TColumn, TColumnItem, TColumnItemValue, List<TColumnItemValue>>
{
    [Parameter]
    public override List<TColumn> Columns { get; set; } = null!;

    [Parameter]
    public override Func<TColumnItem, string> ItemText { get; set; } = null!;

    [Parameter]
    public override Func<TColumnItem, TColumnItemValue> ItemValue { get; set; } = null!;

    [Parameter]
    public override Func<TColumnItem, List<TColumnItem>>? ItemChildren { get; set; }

    [Parameter]
    public override Func<TColumnItem, bool>? ItemDisabled { get; set; }

    protected override string ClassPrefix => "p-mobile-picker";

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        
        Columns.ThrowIfNull(nameof(PMobilePicker<TColumn, TColumnItem, TColumnItemValue>));
        ItemText.ThrowIfNull(nameof(PMobilePicker<TColumn, TColumnItem, TColumnItemValue>));
        ItemValue.ThrowIfNull(nameof(PMobilePicker<TColumn, TColumnItem, TColumnItemValue>));
    }
}
