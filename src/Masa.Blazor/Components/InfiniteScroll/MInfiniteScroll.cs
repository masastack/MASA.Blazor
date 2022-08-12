namespace Masa.Blazor;

public partial class MInfiniteScroll : BInfiniteScroll
{
    [Parameter] public bool Dense { get; set; }

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
