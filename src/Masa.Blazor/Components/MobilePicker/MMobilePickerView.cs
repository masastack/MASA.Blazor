namespace Masa.Blazor;

public class MMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : BMobilePickerView<TColumn, TColumnItem, TColumnItemValue>
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
    
    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply("view", cssBuilder => { cssBuilder.Add("m-mobile-picker__view").AddTheme(IsDark, IndependentTheme); },
                styleBuilder => { styleBuilder.AddHeight(WrapHeight); })
            .Apply("picked", cssBuilder => { cssBuilder.Add("m-mobile-picker__picked"); }, styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("mask", cssBuilder => { cssBuilder.Add("m-mobile-picker__mask"); },
                styleBuilder => { styleBuilder.Add(() => $"background-size: 100% {(WrapHeight - ItemPxHeight) / 2}px"); });

        AbstractProvider
            .Apply(typeof(BMobilePickerColumn<>), typeof(MMobilePickerColumn<TColumnItem>));
    }
}
