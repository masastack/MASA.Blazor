using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MMain : BMain
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-main");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-main__wrap");
                });

            Attributes.Add("data-booted", true);
        }
    }
}
