using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MContainer : BContainer
    {
        /// <summary>
        /// Removes viewport maximum-width size breakpoints
        /// </summary>
        [Parameter]
        public bool Fluid { get; set; }

        public override void SetComponentClass()
        {
            CssBuilder
                .Add("container")
                .AddIf("container--fluid", () => Fluid);
        }
    }
}
