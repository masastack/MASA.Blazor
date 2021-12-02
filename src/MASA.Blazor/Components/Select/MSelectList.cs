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
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Value)] = (StringNumber)Key;
                    props[nameof(MListItem.Link)] = true;
                    props[nameof(MListItem.IsActive)] = Selected;
                    props[nameof(MListItem.Disabled)] = Disabled;
                    props[nameof(MListItem.Highlighted)] = Highlighted;

                    if (!Disabled) props[nameof(MListItem.Color)] = Selected ? "primary" : null;

                    props[nameof(MListItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, OnSelectAsync);
                })
                .Apply<BListItemAction, MListItemAction>()
                .Apply(typeof(ICheckbox), typeof(MSimpleCheckbox), props =>
                {
                    props[nameof(MSimpleCheckbox.Value)] = Selected;
                    props[nameof(MSimpleCheckbox.OnInput)] = EventCallback.Factory.Create<bool>(this, OnSelectAsync);
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