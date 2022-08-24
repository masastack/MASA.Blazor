namespace Masa.Blazor;

public partial class MMobilePickerColumn<TColumnItem> : BMobilePickerColumn<TColumnItem>
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

                    if (cssBuilder.Data is true)
                    {
                        cssBuilder.Add("m-mobile-picker__column-item--disabled");
                    }
                    // .Add("m-mobile-picker__column-item--selected");
                },
                styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("column-text", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-text"); });
    }
}
