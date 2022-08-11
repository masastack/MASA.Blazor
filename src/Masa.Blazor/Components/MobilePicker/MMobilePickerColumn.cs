namespace Masa.Blazor;

public partial class MMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue> : BMobilePickerColumn<TColumn, TColumnItem, TColumnItemValue>
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply("column", cssBuilder => { cssBuilder.Add("m-mobile-picker__column"); })
            .Apply("column-wrapper", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-wrapper"); },
                styleBuilder =>
                {
                    styleBuilder.Add(() => $"transform: translate3d(0, {Offset + BaseOffset}px, 0)")
                                .Add(() => $"transition-duration: {Duration}ms")
                                .Add(() => $"transition-property: {(Duration > 0 ? "all" : "none")}");
                })
            .Apply("column-item",
                cssBuilder =>
                {
                    cssBuilder.Add("m-mobile-picker__column-item");
                    // .Add("m-mobile-picker__column-item--selected");
                },
                styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("column-text", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-text"); });
    }
}
