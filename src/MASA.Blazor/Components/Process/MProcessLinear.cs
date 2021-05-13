using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MProcessLinear : BProcessLinear
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Query { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Striped { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public int Height { get; set; } = 4;

        [Parameter]
        public bool Active { get; set; } = true;

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-progress-linear";

            CssProvider
                .AsProvider<BProcessLinear>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-linear")
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--fixed", () => Fixed)
                        .AddIf($"{prefix}--query", () => Query)
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--striped", () => Striped)
                        .AddTheme(Dark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("bottom:0", () => Bottom)
                        .AddIf("top:0", () => Top)
                        .AddIf(() => $"height:{Height}px", () => Active);
                })
                .Apply("stream", cssBuilder =>
                 {
                     cssBuilder
                         .Add($"{prefix}__stream");
                 })
                .Apply("background", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__background")
                        .AddIf(Color, () => Color != null);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("opacity: 0.3; left: 0%; width: 100%;");
                })
                .Apply("buffer", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__buffer");
                })
                .Apply("determinate", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__determinate");
                })
                .Apply("indeterminate", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .AddIf($"{prefix}__indeterminate--active", () => Active);
                })
                .Apply("long", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .Add("long")
                        .AddIf(Color, () => Color != null);
                })
                .Apply("short", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .Add("short")
                        .AddIf(Color, () => Color != null);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content");
                });
        }
    }
}
