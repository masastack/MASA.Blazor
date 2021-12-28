using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MASA.Blazor
{
    public class MTab : BTab
    {
        [Parameter]
        public bool Ripple { get; set; } = true;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Attributes["ripple"] = !Disabled && Ripple;
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-tab")
                        .AddIf("m-tab--disabled", () => Disabled)
                        .AddIf($"m-tab--active {ComputedActiveClass}", () => IsActive);
                });
        }
    }
}