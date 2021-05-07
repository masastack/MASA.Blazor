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
                    props[nameof(MListItem.Key)] = Key;
                    props[nameof(MListItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
                    {
                        if (!SelectWrapper.Multiple)
                        {
                            SelectWrapper.SetVisible(false);
                            await SelectWrapper.SetSelectedAsync(Value);
                        }
                        else
                        {
                            if (Checked)
                            {
                                Checked = false;
                                await SelectWrapper.RemoveSelectedAsync(Value);
                            }
                            else
                            {
                                Checked = true;
                                await SelectWrapper.SetSelectedAsync(Value);
                            }
                        }
                    });
                })
                .Apply<BListItemAction, MListItemAction>()
                .Apply<BCheckbox, MCheckbox>(props =>
                {
                    props[nameof(MCheckbox.Checked)] = Checked;
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>();
        }
    }
}