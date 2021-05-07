using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MListItemAction : BListItemAction
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BListItemAction>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__action");
                });
        }
    }
}
