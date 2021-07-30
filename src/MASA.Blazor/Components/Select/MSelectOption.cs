using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MSelectOption<TItem, TValue> : BSelectOption<TItem, TValue>
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Key)] = Key;
                    props[nameof(MListItem.Link)] = true;
                    props[nameof(MListItem.IsActive)] = Selected;
                    props[nameof(MListItem.Disabled)] = Disabled;

                    if (!Disabled) props[nameof(MListItem.Color)] = "primary";

                    props[nameof(MListItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
                    {
                        if (!SelectWrapper.Multiple)
                        {
                            await SelectWrapper.SetSelectedAsync(Label, Value);
                        }
                        else
                        {
                            if (Selected)
                            {
                                await SelectWrapper.RemoveSelectedAsync(Label, Value);
                            }
                            else
                            {
                                await SelectWrapper.SetSelectedAsync(Label, Value);
                            }
                        }
                    });
                })
                .Apply<BListItemAction, MListItemAction>()
                .Apply<BCheckbox, MCheckbox>(props =>
                {
                    props[nameof(MCheckbox.Checked)] = Selected;
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemIcon, MListItemIcon>(props =>
                {
                    props[nameof(Style)] = "margin:12px 0 12px 12px";
                })
                .Apply<BIcon, MIcon>();
        }
    }
}