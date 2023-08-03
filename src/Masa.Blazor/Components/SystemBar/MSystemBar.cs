namespace Masa.Blazor
{
    public partial class MSystemBar : BSystemBar, IThemeable
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public StringNumber? Height
        {
            get => GetValue<StringNumber>();
            set => SetValue(value);
        }

        [Parameter]
        public bool LightsOut { get; set; }

        [Parameter]
        public bool Window
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool App
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public bool Fixed { get; set; }

        private StringNumber ComputedHeight => Height?.ToString() != null
            ? (Regex.IsMatch(Height.ToString()!, "^[0-9]*$") ? Height.ToInt32() : Height)
            : (Window ? 32 : 24);

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<bool>(nameof(App), (_, prev) =>
                   {
                       if (prev)
                       {
                           RemoveApplication(true);
                       }
                       else
                       {
                           CallUpdate();
                       }
                   }, immediate: true)
                   .Watch<bool>(nameof(Window), CallUpdate)
                   .Watch<StringNumber>(nameof(Height), CallUpdate);
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-system-bar")
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddIf("m-system-bar--lights-out", () => LightsOut)
                        .AddIf("m-system-bar--absolute", () => Absolute)
                        .AddIf("m-system-bar--fixed", () => !Absolute && (App || Fixed))
                        .AddIf("m-system-bar--window", () => Window);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color)
                        .Add($"height:{ComputedHeight.ToUnit()}");
                });
        }

        private async void CallUpdate()
        {
            await NextTickIf(async () => { await UpdateApplicationAsync(); }, () => Ref.Context is null);
        }

        private async Task UpdateApplicationAsync()
        {
            if (!App)
            {
                return;
            }

            var height = ComputedHeight.ToDouble();
            MasaBlazor.Application.Bar = height > 0 ? height : await GetClientHeightAsync();
        }

        private async Task<double> GetClientHeightAsync()
        {
            if (Ref.Context == null)
            {
                return 0;
            }

            var element = await JsInvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
            return element?.ClientHeight ?? 0;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            RemoveApplication();
        }

        private void RemoveApplication(bool force = false)
        {
            if (!force && !App)
            {
                return;
            }

            MasaBlazor.Application.Bar = 0;
        }
    }
}
