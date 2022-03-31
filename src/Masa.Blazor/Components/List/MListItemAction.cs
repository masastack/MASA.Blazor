namespace Masa.Blazor
{
    public partial class MListItemAction : BListItemAction
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__action");
                });
        }
    }
}
