namespace Masa.Blazor
{
    public partial class MOverlay : BOverlay, IThemeable, IOverlay, IAsyncDisposable
    {
        [Inject]
        private ScrollStrategyJSModule ScrollStrategyJSModule { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Contained { get; set; }

        [Parameter]
        [MassApiParameter("#212121")]
        public string? Color { get; set; } = "#212121";

        [Parameter]
        [MassApiParameter(0.46)]
        public StringNumber Opacity { get; set; } = 0.46;

        [Parameter]
        public string? ScrimClass { get; set; }

        [Parameter]
        [MassApiParameter(5)]
        public int ZIndex { get; set; } = 5;

        public ElementReference ContentRef { get; set; }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<bool>(nameof(Value), ValueChangeCallback);
        }

        private async Task ValueChangeCallback()
        {
            if (Value)
            {
                _ = NextTickIf(async () => { await HideScroll(); }, () => ContentRef.Id is null);
            }
            else
            {
                await ShowScroll();
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-overlay")
                        .AddIf("m-overlay--active", () => Value)
                        .AddIf("m-overlay--absolute", () => Absolute || Contained)
                        .AddIf("m-overlay--contained", () => Contained)
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

        private async Task HideScroll()
        {
            if (!ScrollStrategyJSModule.Initialized)
            {
                await ScrollStrategyJSModule.InitializeAsync(Ref, ContentRef, new { Contained, scrollStrategy = "block" });
            }

            await ScrollStrategyJSModule.HideScroll();
        }

        private async Task ShowScroll()
        {
            await ScrollStrategyJSModule.ShowScroll();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                if (ScrollStrategyJSModule.Initialized)
                {
                    await ShowScroll();
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
