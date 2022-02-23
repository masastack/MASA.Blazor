using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor
{
    public partial class MDivider : BDivider, IThemeable
    {
        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-divider";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-divider")
                        .AddIf($"{prefix}--inset", () => Inset)
                        .AddIf($"{prefix}--vertical", () => Vertical)
                        .AddTheme(IsDark);
                });
        }
    }
}
