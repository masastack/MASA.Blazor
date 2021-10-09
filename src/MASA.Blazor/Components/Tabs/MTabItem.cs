using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTabItem : MWindowItem, ITabItem
    {
        [CascadingParameter]
        public MTabs Tabs { get; set; }

        [CascadingParameter(Name = "DISPLAY:NONE")]
        public bool IsDisplayNone { get; set; }

        protected override void OnInitialized()
        {
            if (Tabs == null)
            {
                base.OnInitialized();
            }
            else
            {
                if (!IsDisplayNone) return;

                if (Value == null)
                    Value = Tabs.TabItems.Count;

                if (Tabs.TabItems.Any(item => item.Value?.ToString() == Value.ToString())) return;

                Tabs.AddTabItem(this);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            
            if (IsDisplayNone) return;

            if (firstRender)
            {
                Tabs?.CallSlider();
            }
        }
    }
}