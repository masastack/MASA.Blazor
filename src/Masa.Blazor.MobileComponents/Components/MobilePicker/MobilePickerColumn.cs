namespace Masa.Blazor.Components.MobilePicker;

public class MobilePickerColumn<TItem>
{
    public List<TItem> Values { get; set; }
    
    public int Index { get; set; }

    public string? ClassName { get; set; }

    public MobilePickerColumn(List<TItem> values)
    {
        Values = values;
    }

    public MobilePickerColumn(List<TItem> values, int index): this(values)
    {
        Index = index;
    }

    public MobilePickerColumn(List<TItem> values, int index, string className): this(values, index)
    {
        ClassName = className;
    }
}
