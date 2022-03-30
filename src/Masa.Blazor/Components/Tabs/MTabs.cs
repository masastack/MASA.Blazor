namespace Masa.Blazor
{
    public partial class MTabs : BTabs, IThemeable
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
        public bool Right { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

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
                        .AddTheme(IsDark);
                })
                .Apply("slider-wrapper",
                    cssBuilder => { cssBuilder.Add("m-tabs-slider-wrapper"); },
                    styleBuilder =>
                    {
                        styleBuilder
                            .AddWidth(Slider.width)
                            .AddHeight(Slider.height)
                            .AddIf(() => $"left:{Slider.left.ToUnit()}", () => !IsReversed)
                            .AddIf(() => $"right:{Slider.right.ToUnit()}", () => IsReversed)
                            .AddIf(() => $"top:{Slider.top.ToUnit()}", () => Vertical)
                            .AddIf(() => $"transition:none", () => Slider.left == null);
                    })
                .Apply("slider", cssBuilder =>
                    {
                        cssBuilder
                            .Add("m-tabs-slider")
                            .AddBackgroundColor(SliderColor);
                    },
                    styleBuilder => styleBuilder.AddBackgroundColor(SliderColor));

            AbstractProvider
                .ApplyTabsDefault()
                .Apply<BSlideGroup, MTabsBar>(attrs =>
                {
                    attrs[nameof(MTabsBar.ActiveClass)] = ActiveClass;
                    attrs[nameof(MTabsBar.CenterActive)] = CenterActive;
                    attrs[nameof(MTabsBar.BackgroundColor)] = BackgroundColor;
                    attrs[nameof(MTabsBar.IsDark)] = IsDark;
                })
                .Apply<BItem, MSlideItem>()
                .Apply<BTab, MTab>()
                .Apply<BWindow, MTabsItems>()
                .Apply<BWindowItem, MTabItem>();
        }
    }
}