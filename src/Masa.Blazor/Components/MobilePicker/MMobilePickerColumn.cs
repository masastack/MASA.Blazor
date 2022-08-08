namespace Masa.Blazor;

public partial class MMobilePickerColumn<TColumnItem> : BMobilePickerColumn<TColumnItem>
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply("column", cssBuilder => { cssBuilder.Add("m-mobile-picker__column"); })
            .Apply("column-wrapper", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-wrapper"); },
                styleBuilder => { styleBuilder.Add(() => $"transform:"); })
            .Apply("column-item",
                cssBuilder => { cssBuilder.Add("m-mobile-picker__column-item"); },
                styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("column-text", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-text"); });
    }
}
