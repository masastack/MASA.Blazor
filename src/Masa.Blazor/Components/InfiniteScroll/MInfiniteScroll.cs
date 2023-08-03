namespace Masa.Blazor;

public partial class MInfiniteScroll : BInfiniteScroll
{
    [Inject]
    protected I18n I18n { get; set; } = null!;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        EmptyText ??= I18n.T("$masaBlazor.infiniteScroll.emptyText");
        ErrorText ??= I18n.T("$masaBlazor.infiniteScroll.errorText");
        LoadingText ??= I18n.T("$masaBlazor.infiniteScroll.loadingText");
        LoadMoreText ??= I18n.T("$masaBlazor.infiniteScroll.loadMoreText");

        return base.SetParametersAsync(parameters);
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-infinite-scroll"); });

        AbstractProvider
            .Apply<BProgressCircular, MProgressCircular>(attrs => { attrs[nameof(MProgressCircular.Class)] = "m-infinite-scroll__loader"; })
            .Apply<BButton, MButton>("retry", attrs =>
            {
                attrs[nameof(MButton.Class)] = "m-infinite-scroll__retry";
                attrs[nameof(MButton.Icon)] = true;
                attrs[nameof(MButton.Text)] = true;
            })
            .Apply<BButton, MButton>("loadMore", attrs =>
            {
                attrs[nameof(MButton.Class)] = "m-infinite-scroll__load-more";
                attrs[nameof(MButton.Outlined)] = true;
            });
    }
}
