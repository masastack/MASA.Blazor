using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MCascaderSelectOption : MSelectOption<BCascaderNode, string>
    {
        [Parameter]
        public EventCallback<MCascaderSelectOption> OnOptionClick { get; set; }

        protected override void SetComponentClass()
        {
            AbstractProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Key)] = Key;
                    props[nameof(MListItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (Item.Children == null || Item.Children.Count ==0)
                        {
                            SelectWrapper.SetVisible(false);
                        }

                        if (OnOptionClick.HasDelegate)
                        {
                            await OnOptionClick.InvokeAsync(this);
                        }
                    });
                });

            base.SetComponentClass();
            Icon = Item.Children != null && Item.Children.Count > 0 ? "mdi-chevron-right" : string.Empty;
        }
    }
}
