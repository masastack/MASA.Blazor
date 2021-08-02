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

        [Parameter]
        public string Key { get; set; }

        public string ComputedKey => Key ?? Tabs.TabItems.IndexOf(this).ToString();

        [Parameter]
        public override bool IsActive
        {
            get
            {
                return Tabs.Value == ComputedKey || (Tabs.Value == null && Tabs.TabItems.IndexOf(this) == 0);
            }
            set
            {
                base.IsActive = value;
            }
        }

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
