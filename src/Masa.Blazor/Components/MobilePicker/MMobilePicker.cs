namespace Masa.Blazor;

public partial class MMobilePicker<TItem, TItemValue> : BMobilePicker<TItem, TItemValue>
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
            .Apply("picked", cssBuilder => { cssBuilder.Add("m-mobile-picker__picked"); }, styleBuilder =>
            {
                styleBuilder.AddHeight(ItemHeight)
                            .AddTop(ComputedTop);
            })
            .Apply("mask", cssBuilder => { cssBuilder.Add("m-mobile-picker__mask"); },
                styleBuilder => { styleBuilder.Add(() => $"backgroundSize: 100% {(WrapHeight - ItemPxHeight) / 2}px"); });

        AbstractProvider
            .Apply(typeof(BMobilePickerColumn<>), typeof(MMobilePickerColumn<TItem>))
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
