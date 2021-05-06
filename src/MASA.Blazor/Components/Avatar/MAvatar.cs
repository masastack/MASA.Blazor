using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MAvatar : BAvatar
    {
        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public int Size { get; set; } = 48;

        [Parameter]
        public int? Height { get; set; }

        [Parameter]
        public int? MaxHeight { get; set; }

        [Parameter]
        public int? MaxWidth { get; set; }

        [Parameter]
        public int? MinHeight { get; set; }

        [Parameter]
        public int? MinWidth { get; set; }

        [Parameter]
        public int? Width { get; set; }

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-avatar";

            CssProvider
                .Apply<BAvatar>(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf("rounded", () => Rounded)
                        .AddIf("rounded-0", () => Tile)
                        .AddIf(Color, () => !string.IsNullOrEmpty(Color));
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"height:{Size}px")
                        .Add($"min-width:{Size}px")
                        .Add($"width:{Size}px")
                        .AddIf($"height:{Height}px", () => Height != null)
                        .AddIf($"min-height:{MinHeight}px", () => MinHeight != null)
                        .AddIf($"min-width:{MinWidth}px", () => MinWidth != null)
                        .AddIf($"max-height:{MaxHeight}px", () => MaxHeight != null)
                        .AddIf($"max-width:{MaxWidth}px", () => MaxWidth != null)
                        .AddIf($"width:{Width}px", () => Width != null);
                });
        }
    }
}
