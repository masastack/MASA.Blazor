using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascaderSelectOption : MSelectOption<BCascaderNode>
    {
        [Parameter]
        public EventCallback<MCascaderSelectOption> OnOptionClick { get; set; }

        protected override void SetComponentClass()
        {
            SlotProvider
                .Apply<BListItem, MListItem>(props =>
                {
                    props[nameof(MListItem.Key)] = Key;
                    props[nameof(MListItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (OnOptionClick.HasDelegate)
                        {
                            await OnOptionClick.InvokeAsync(this);
                        }
                    });
                });

            base.SetComponentClass();
            Icon = Value.Children != null && Value.Children.Count > 0 ? "mdi-chevron-right" : string.Empty;
        }
    }
}
