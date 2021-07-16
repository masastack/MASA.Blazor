using BlazorComponent;

namespace MASA.Blazor
{
    internal partial class MAutocompleteSelectBody<TItem> : BAutocompleteSelectBody<TItem>
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            HighlightClass = "m-list-item--highlighted";
            SelectedClass = "primary--text m-list-item--active";

            CssProvider
                .Apply("mask", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item__mask");
                });

            AbstractProvider
                .Apply<BList, MList>(props =>
                {
                    props[nameof(MList.Class)] = "m-select-list";
                })
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Link)] = true;
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemAction, MListItemAction>()
                .Apply<BCheckbox, MCheckbox>();
        }
    }
}
