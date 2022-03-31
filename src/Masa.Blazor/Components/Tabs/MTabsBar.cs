namespace Masa.Blazor
{
    public partial class MTabsBar : MSlideGroup, IThemeable
    {
        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        protected string ComputedColor
        {
            get
            {
                if (Color != null) return Color;
                return IsDark ? "white" : "primary";
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            const string prefix = "m-tabs-bar";

            CssProvider
                .Merge(css =>
                {
                    css.Add(prefix)
                        .AddTheme(IsDark)
                        .AddTextColor(ComputedColor)
                        .AddBackgroundColor(BackgroundColor);
                }, style =>
                {
                    style.AddTextColor(ComputedColor);
                    style.AddBackgroundColor(BackgroundColor);
                })
                .Merge("content", css => { css.Add($"{prefix}__content"); });
        }
    }
}