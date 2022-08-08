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
                styleBuilder => { styleBuilder.AddHeight(ComputedHeight); })
            .Apply("column", cssBuilder => { cssBuilder.Add("m-mobile-picker__column"); })
            .Apply("column-wrapper", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-wrapper"); },
                styleBuilder => { styleBuilder.Add(() => $"transform:"); })
            .Apply("column-item",
                cssBuilder => { cssBuilder.Add("m-mobile-picker__column-item"); },
                styleBuilder => { styleBuilder.AddHeight(ItemHeight); })
            .Apply("column-text", cssBuilder => { cssBuilder.Add("m-mobile-picker__column-text"); })
            .Apply("picked", cssBuilder => { cssBuilder.Add("m-mobile-picker__picked"); }, styleBuilder =>
            {
                styleBuilder.AddHeight(ItemHeight)
                            .AddTop(ComputedTop);
            })
            .Apply("mask", cssBuilder => { cssBuilder.Add("m-mobile-picker__mask"); },
                styleBuilder => { styleBuilder.Add(() => $"backgroundSize: 100% {(ComputedHeight - ComputedItemHeight) / 2}px"); });

        AbstractProvider
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
