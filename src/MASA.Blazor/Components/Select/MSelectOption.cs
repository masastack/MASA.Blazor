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
                    props[nameof(MListItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
                    {
                        if (!SelectWrapper.Multiple)
                        {
                            SelectWrapper.SetVisible(false);

                            await SelectWrapper.SetSelectedAsync(Label, Value);
                        }
                        else
                        {
                            if (_checked)
                            {
                                _checked = false;
                                await SelectWrapper.RemoveSelectedAsync(Label, Value);
                            }
                            else
                            {
                                _checked = true;
                                await SelectWrapper.SetSelectedAsync(Label, Value);
                            }
                        }
                    });
                })
                .Apply<BListItemAction, MListItemAction>()
                .Apply<BCheckbox, MCheckbox>(props =>
                {
                    props[nameof(MCheckbox.Checked)] = _checked;
                })
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemIcon, MListItemIcon>(props =>
                {
                    props[nameof(Style)] = "margin:12px 0 12px 12px";
                })
                .Apply<BIcon, MIcon>();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            _checked = SelectWrapper.Values.Contains(Value);
        }
    }
}