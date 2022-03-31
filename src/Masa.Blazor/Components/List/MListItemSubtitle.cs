namespace Masa.Blazor
{
    public partial class MListItemSubtitle : BListItemSubtitle
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__subtitle");
                });
        }
    }
}
