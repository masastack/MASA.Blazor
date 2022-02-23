using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public partial class MContainer : BContainer
    {
        /// <summary>
        /// Removes viewport maximum-width size breakpoints
        /// </summary>
        [Parameter]
        public bool Fluid { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("container")
                        .AddIf("container--fluid", () => Fluid);
                });
        }
    }
}
