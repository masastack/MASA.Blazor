using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public partial class MTimeline : BTimeline, IThemeable
    {
        [Parameter]
        public bool AlignTop { get; set; }

        [Parameter]
        public bool Dense { get; set; }


        protected override void SetComponentClass()
        {
            var prefix = "m-timeline";

            CssProvider
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
