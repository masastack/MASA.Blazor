namespace Masa.Blazor
{
    public partial class MListItemContent : BListItemContent
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__content");
                });
        }
    }
}
