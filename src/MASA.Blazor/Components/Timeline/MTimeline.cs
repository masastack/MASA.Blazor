using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTimeline : BTimeline,IThemeable
    {
        [Parameter]
        public bool AlignTop { get; set; }

        [Parameter]
        public bool Dense { get; set; }

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
            var prefix = "m-timeline";

            CssProvider
                .AsProvider<BTimeline>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--align-top", () => AlignTop)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--reverse", () => Reverse)
                        .AddTheme(IsDark);
                });
        }
    }
}
