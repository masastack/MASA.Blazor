using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTabsBar : MSlideGroup, ITabsBar
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-tabs-bar";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(Dark)
                        .AddTextColor(Color);
                });

            SlotProvider
                .Merge<BSlideGroupSlot>(props =>
                {
                    props[nameof(Class)] = "m-tabs-bar__content";
                });
        }
    }
}
