using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace MASA.Blazor
{
    internal partial class MCascaderSelectOption<TItem, TValue> : MSelectOption<TItem, TValue>
    {
        [Parameter]
        public EventCallback<TItem> OnItemClick { get; set; }

        [Parameter]
        public List<TItem> Children { get; set; }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Value)] = (StringNumber)Key;
                    props[nameof(MListItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (Children == null || Children.Count == 0)
                        {
                            await Select.SetSelectedAsync(Label, Value);
                            Select.SetVisible(false);
                        }

                        if (OnItemClick.HasDelegate)
                        {
                            await OnItemClick.InvokeAsync(Item);
                        }
                    });
                });

            Icon = Children != null && Children.Count > 0 ? "mdi-chevron-right" : string.Empty;

            base.SetComponentClass();
        }
    }
}
