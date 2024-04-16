using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MTabs : BTabs, IThemeable, IAsyncDisposable
    {
        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject]
        private IntersectJSModule IntersectJSModule { get; set; } = null!;

        [Parameter]
        public string? ActiveClass { get; set; }

        [Parameter]
        public bool AlignWithTitle { get; set; }

        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        public bool CenterActive { get; set; }

        [Parameter]
        public bool Centered { get; set; }

        [Parameter]
        public bool FixedTabs { get; set; }

        [Parameter]
        public bool Grow { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public bool IconsAndText { get; set; }

        [Parameter]
        public StringNumber? MobileBreakpoint { get; set; }

        [Parameter]
        public bool Right { get; set; }

        protected override bool RTL => MasaBlazor.RTL;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            MasaBlazor.RTLChanged += MasaBlazorOnRTLChanged;
        }

        private void MasaBlazorOnRTLChanged(object? sender, EventArgs e)
        {
            InvokeAsync(CallSlider);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await IntersectJSModule.ObserverAsync(Ref, OnIntersectAsync);
            }
        }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
        
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
                        .AddTheme(IsDark, IndependentTheme);
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
                    attrs[nameof(MTabsBar.Color)] = Color;
                    attrs[nameof(MTabsBar.ActiveClass)] = ActiveClass;
                    attrs[nameof(MTabsBar.CenterActive)] = CenterActive;
                    attrs[nameof(MTabsBar.BackgroundColor)] = BackgroundColor;
                    attrs[nameof(MTabsBar.IsDark)] = IsDark;
                    attrs[nameof(MTabsBar.Style)] = StyleBuilder.Create().AddHeight(Height).Build();
                })
                .Apply<BItem, MSlideItem>()
                .Apply<BTab, MTab>()
                .Apply<BWindow, MTabsItems>()
                .Apply<BWindowItem, MTabItem>();
        }

        private async Task OnIntersectAsync(IntersectEventArgs e)
        {
            if (e.IsIntersecting)
            {
                await InvokeAsync(CallSlider);
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                MasaBlazor.RTLChanged -= MasaBlazorOnRTLChanged;
                await IntersectJSModule.UnobserveAsync(Ref);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
