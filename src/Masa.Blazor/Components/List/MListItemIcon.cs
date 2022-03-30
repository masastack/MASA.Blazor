namespace Masa.Blazor
{
    public partial class MListItemIcon : BListItemIcon
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__icon");
                });
        }
    }
}
