namespace Masa.Blazor
{
    public partial class MListItemTitle : BListItemTitle
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__title");
                });
        }
    }
}
