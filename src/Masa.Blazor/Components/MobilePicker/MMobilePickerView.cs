namespace Masa.Blazor;

public class MMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BMobilePickerView<TColumn, TColumnItem, TColumnItemValue>
{
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply("view", cssBuilder => { cssBuilder.Add("m-mobile-picker__view").AddTheme(IsDark); },
                styleBuilder => { styleBuilder.AddHeight(WrapHeight); })
            .Apply("picked", cssBuilder => { cssBuilder.Add("m-mobile-picker__picked"); }, styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("mask", cssBuilder => { cssBuilder.Add("m-mobile-picker__mask"); },
                styleBuilder => { styleBuilder.Add(() => $"background-size: 100% {(WrapHeight - ItemPxHeight) / 2}px"); });

        AbstractProvider
            .Apply(typeof(BMobilePickerColumn<>), typeof(MMobilePickerColumn<TColumnItem>));
    }
}
