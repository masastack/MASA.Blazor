using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MAutocompleteSelectBody<TItem> : BAutocompleteSelectBody<TItem>
    {
        [CascadingParameter(Name = "Popover")]
        public ElementReference MenuRef { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsInvokeAsync(JsInteropConstants.ScrollToPosition, MenuRef, (HighlightIndex - 1) * 48);
        }
    }
}
