namespace Masa.Blazor.Presets;

public partial class PDateClockPickerView<TValue> : PDateTimePickerViewBase<TValue>
{
    [Parameter] public bool NoTitle { get; set; } = true;

    [Parameter] public string? HeaderColor { get; set; }
}
