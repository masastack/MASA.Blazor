using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MWindowItem : BWindowItem
    {
        [Parameter]
        public virtual bool IsActive { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window-item")
                        .AddIf("m-window-item--active", () => IsActive);
                }, cssBuilder =>
                {
                    cssBuilder
                        .AddIf("display:none", () => !IsActive);
                });
        }
    }
}
