namespace Masa.Blazor;

public partial class MMobilePicker<TColumnItem, TColumnItemValue> : BMobilePicker<TColumnItem, TColumnItemValue>
{
    [Inject]
    protected I18n I18n { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CancelText ??= I18n.T("$masaBlazor.cancel");
        OkText ??= I18n.T("$masaBlazor.ok");
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-mobile-picker"); })
            .Apply("toolbar", cssBuilder => { cssBuilder.Add("m-mobile-picker__toolbar"); })
            .Apply("title", cssBuilder => { cssBuilder.Add("m-mobile-picker__title"); })
            .Apply("columns", cssBuilder => { cssBuilder.Add("m-mobile-picker__columns"); },
                styleBuilder => { styleBuilder.AddHeight(WrapHeight); })
            .Apply("picked", cssBuilder => { cssBuilder.Add("m-mobile-picker__picked"); }, styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("mask", cssBuilder => { cssBuilder.Add("m-mobile-picker__mask"); },
                styleBuilder => { styleBuilder.Add(() => $"background-size: 100% {(WrapHeight - ItemPxHeight) / 2}px"); });

        AbstractProvider
            .Apply(typeof(BMobilePickerColumn<,>), typeof(MMobilePickerColumn<TColumnItem, TColumnItemValue>))
            .Apply<BButton, MButton>("cancel", attrs =>
            {
                attrs[nameof(MButton.Text)] = true;
                attrs[nameof(MButton.Plain)] = true;
            })
            .Apply<BButton, MButton>("ok", attrs =>
            {
                attrs[nameof(MButton.Text)] = true;
                attrs[nameof(MButton.Color)] = "primary";
            });
    }
}
