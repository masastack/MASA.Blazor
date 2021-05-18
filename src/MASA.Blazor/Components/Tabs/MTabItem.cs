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
        public BTabs Tabs { get; set; }

        protected override void OnInitialized()
        {
            Tabs.AddTabItem(this);
        }

        public void Active()
        {
            IsActive = true;
        }

        public void DeActive()
        {
            IsActive = false;
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Merge(mergeStyleAction: styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !IsActive);
                });
        }
    }
}
