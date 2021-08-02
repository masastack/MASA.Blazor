using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MDivider : BDivider, IThemeable
    {
        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        } 

        protected override void SetComponentClass()
        {
            var prefix = "m-divider";

            CssProvider
                .Apply<BDivider>(cssBuilder =>
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
