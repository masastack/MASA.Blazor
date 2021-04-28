using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MSelectOption<TItem> : BSelectOption<TItem>
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            SlotProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        SelectWrapper.SetVisible(false);
                        SelectWrapper.SetSelected(Value);
                    });
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>();
        }
    }
}
