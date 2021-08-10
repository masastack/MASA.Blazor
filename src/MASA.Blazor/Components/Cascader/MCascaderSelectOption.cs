using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    internal partial class MCascaderSelectOption : MSelectOption<BCascaderNode, string>
    {
        [Parameter]
        public EventCallback<BCascaderNode> OnItemClick { get; set; }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Key)] = Key;
                    props[nameof(MListItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (Item.Children == null || Item.Children.Count == 0)
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

            base.SetComponentClass();
            Icon = Item.Children != null && Item.Children.Count > 0 ? "mdi-chevron-right" : string.Empty;
        }
    }
}
