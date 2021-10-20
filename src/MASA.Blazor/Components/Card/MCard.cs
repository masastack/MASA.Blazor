using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MCard : BCard, IThemeable
    {
        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Raised { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public string Img { get; set; }

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
            var prefix = "m-card";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-card")
                        .AddIf("elevation-2", () => !Outlined && !Flat)
                        .AddTheme(IsDark)
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddIf($"{prefix}--flat", () => Flat)
                        .AddIf($"{prefix}--hover", () => Hover)
                        .AddIf($"{prefix}--link", () => Link || OnClick.HasDelegate)
                        .AddIf($"{prefix}--loading", () => Loading!=false)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--raised", () => Raised)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddIf($"background:url(\"{Img}\") center center / cover no-repeat", () => !string.IsNullOrWhiteSpace(Img));
                });
            AbstractProvider.Apply<BProgressLinear,MProgressLinear>();
        }
    }
}