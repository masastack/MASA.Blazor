using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTabs : BTabs
    {
        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public bool AlignWithTitle { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public bool CenterActive { get; set; }

        [Parameter]
        public bool Centered { get; set; }

        [Parameter]
        public bool FixedTabs { get; set; }

        [Parameter]
        public bool Grow { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool IconsAndText { get; set; }

        [Parameter]
        public StringNumber MobileBreakpoint { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public string Oprional { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool ShowArrows { get; set; }

        [Parameter]
        public string SliderColor { get; set; }

        [Parameter]
        public StringNumber SliderSize { get; set; } = 2;

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected StringNumber SlideLeft => ActiveTab?.Rect?.OffsetLeft;

        protected StringNumber SlideWidth => ActiveTab?.Rect?.ScrollWidth;

        protected override void SetComponentClass()
        {
            var prefix = "m-tabs";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--align-with-title", () => AlignWithTitle)
                        .AddIf($"{prefix}--centered", () => Centered)
                        .AddIf($"{prefix}--fixed-tabs", () => FixedTabs)
                        .AddIf($"{prefix}--grow", () => Grow)
                        .AddIf($"{prefix}--icons-and-text", () => IconsAndText)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--vertical", () => Vertical)
                        .AddTheme(Dark);
                })
                .Apply("slider-wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-tabs-slider-wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(SliderSize)
                        .AddIf(() => $"left:{SlideLeft.ToUnit()}", () => SlideLeft != null)
                        .AddIf(() => $"width:{SlideWidth.ToUnit()}", () => SlideWidth != null);
                });

            AbstractProvider
                .Apply<ITabsBar, MTabsBar>()
                .Apply<ITabsItems, MTabsItems>()
                .Apply<BTabsSlider, MTabsSlider>();
        }
    }
}
