namespace Masa.Blazor
{
    public partial class MTabsBar : MSlideGroup, IThemeable
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [CascadingParameter]
        public BTabs? Tabs { get; set; }
        
        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        public string? Color { get; set; }

        protected string ComputedColor
        {
            get
            {
                if (Color != null) return Color;
                return IsDark ? "white" : "primary";
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

        protected override StringNumber InitDefaultItemValue()
        {
            return Tabs?.Routable is true ? NavigationManager.Uri : base.InitDefaultItemValue();
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            const string prefix = "m-tabs-bar";

            CssProvider
                .Merge(css =>
                {
                    css.Add(prefix)
                       .AddIf($"{prefix}--is-mobile", () => IsMobile)
                       .AddTheme(IsDark, IndependentTheme)
                       .AddTextColor(ComputedColor)
                       .AddBackgroundColor(BackgroundColor);
                }, style =>
                {
                    style.AddTextColor(ComputedColor);
                    style.AddBackgroundColor(BackgroundColor);
                })
                .Merge("content", css => { css.Add($"{prefix}__content"); });
        }

        public override void Unregister(IGroupable item)
        {
            base.Unregister(item);

            Tabs?.CallSliderAfterRender();
        }
    }
}
