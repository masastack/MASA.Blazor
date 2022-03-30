namespace Masa.Blazor
{
    public partial class MListItemActionText : BListItemActionText
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__action-text");
                });
        }
    }
}
