using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MCounter : BCounter
    {


        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-counter")
                        .AddIf("error--text", () => Max != null && (Value.ToInt32() > Max.ToInt32()))
                        .AddTheme(IsDark);
                });
        }
    }
}
