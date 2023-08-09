namespace Masa.Blazor.Presets;

public partial class PDateTimePickerView<TValue> : PDateTimePickerViewBase<TValue>
{
    [Parameter] public bool AmPmInTitle { get; set; }

    [Parameter] public string? HeaderColor { get; set; }

    [Parameter] public Func<DateOnly, string>? HeaderDateFormat { get; set; }

    [Parameter] public bool NoTitle { get; set; }

    [Parameter] public Func<IList<DateOnly>, string>? TitleDateFormat { get; set; }
}
