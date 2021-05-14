using BlazorComponent;
using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MSheet : BSheet
    {
        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public OneOf<bool, string> Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-sheet";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--shaped", () => Shaped)
                        .AddTheme(Dark)
                        .AddIf($"elevation-{Elevation.Value}", () => Elevation.Value != default)
                        .AddRounded(Tile, Rounded)
                        .AddColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddBackgroundColor(Color);
                });
        }
    }
}
