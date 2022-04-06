namespace Masa.Blazor
{
    public partial class MOverlay : BOverlay, IThemeable, IOverlay
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public string Color { get; set; } = "#212121";

        [Parameter]
        public StringNumber Opacity { get; set; } = 0.46;

        [Parameter]
        public string ScrimClass { get; set; }

        [Parameter]
        public int ZIndex { get; set; } = 5;

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay")
                        .AddIf("m-overlay--active", () => Value)
                        .AddIf("m-overlay--absolute", () => Absolute)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"z-index: {ZIndex}");
                })
                .Apply("scrim", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay__scrim")
                        .AddBackgroundColor(Color)
                        .Add(ScrimClass);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color)
                        .Add(() => $"opacity:{(Value ? Opacity.TryGetNumber().number : 0)}");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay__content");
                });

            AbstractProvider
                .ApplyOverlayDefault();
        }
    }
}