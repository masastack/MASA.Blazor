using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSelectList<TItem, TItemValue, TValue> : BSelectList<TItem, TItemValue, TValue>
    {
        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Apply<BListItem, MListItem>(attrs =>
                {
                    attrs[nameof(MListItem.Value)] = (StringNumber)Key;
                    attrs[nameof(MListItem.Link)] = true;
                    attrs[nameof(MListItem.IsActive)] = Selected;
                    attrs[nameof(MListItem.Disabled)] = Disabled;
                    attrs[nameof(MListItem.Highlighted)] = Highlighted;

                    if (!Disabled) attrs[nameof(MListItem.Color)] = Selected ? "primary" : null;

                    attrs[nameof(MListItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, OnSelectAsync);
                })
                .Apply<BListItemAction, MListItemAction>()
                .Apply(typeof(BSimpleCheckbox), typeof(MSimpleCheckbox), attrs =>
                {
                    attrs[nameof(MSimpleCheckbox.Value)] = Selected;
                    attrs[nameof(MSimpleCheckbox.OnInput)] = EventCallback.Factory.Create<bool>(this, OnSelectAsync);
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemIcon, MListItemIcon>()
                .Apply<BIcon, MIcon>();
        }

        protected virtual async Task OnSelectAsync()
        {
            if (Disabled)
            {
                return;
            }

            if (!Select.Multiple)
            {
                Select.SetVisible(false);
                await Select.SetSelectedAsync(Label, Value);
            }
            else
            {
                if (Selected)
                {
                    await Select.RemoveSelectedAsync(Label, Value);
                }
                else
                {
                    await Select.SetSelectedAsync(Label, Value);
                }
            }
        }
    }
}