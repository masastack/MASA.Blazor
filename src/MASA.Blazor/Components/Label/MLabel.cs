using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MLabel : BLabel
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public int Left { get; set; }

        [Parameter]
        public int Right { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Active { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-label";
            CssProvider
                .Apply<BLabel>(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => Active)
                        .AddIf($"{prefix}--is-disabled", () => Disabled)
                        .AddTheme(Dark)
                        .AddIf("primary--text", () => Active);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"left:{Left}px")
                        .Add(() => $"right:{(Right == 0 ? "auto" : Right + "px")}")
                        .Add(() => $"position:{(Absolute ? "absolute" : "relative")}");
                });
        }
    }
}
