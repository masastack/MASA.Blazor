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
        public override void SetComponentClass()
        {
            Style = "padding: 64px 0px 0px;";

            CssBuilder
                .Add("m-main");

            WrapCssBuilder
                .Add("m-main__wrap");
        }
    }
}
