namespace Masa.Blazor.Presets;

public partial class PDateTimePickerView<TValue> : PDateTimePickerViewBase<TValue>
{
    [Parameter] public bool NoTitle { get; set; }

    [Parameter] public bool AmPmInTitle { get; set; }

    [Parameter] public string? HeaderColor { get; set; }
}
