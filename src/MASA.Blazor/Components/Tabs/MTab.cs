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
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-tab")
                        .AddIf("m-tab--active", () => IsActive);
                });
        }
    }
}
