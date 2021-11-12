using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTabItem : MWindowItem, ITabItem
    {
        [CascadingParameter]
        public MTabs Tabs { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Tabs?.RegisterTabItem(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Tabs?.CallSlider();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Tabs?.UnregisterTabItem(this);
        }
    }
}