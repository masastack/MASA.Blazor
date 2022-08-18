namespace Masa.Blazor;

public partial class MInfiniteScroll : BInfiniteScroll
{
    [Inject]
    protected I18n I18n { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoMoreText ??= I18n.T("$masaBlazor.infiniteScroll.noMore");
        FailedToLoadText ??= I18n.T("$masaBlazor.infiniteScroll.failedToLoad");
        ReloadText ??= I18n.T("$masaBlazor.infiniteScroll.reload");
        LoadingText ??= I18n.T("$masaBlazor.infiniteScroll.loading");
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-infinite-scroll"); })
            .Apply("text--no-more", cssBuilder => { cssBuilder.Add("m-infinite-scroll__text--no-more"); })
            .Apply("text--loading", cssBuilder => { cssBuilder.Add("m-infinite-scroll__text--loading"); })
            .Apply("text--failed", cssBuilder => { cssBuilder.Add("m-infinite-scroll__text--failed"); });
    }
}
