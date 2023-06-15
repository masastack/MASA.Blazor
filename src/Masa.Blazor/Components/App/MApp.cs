using BlazorComponent.Web;

namespace Masa.Blazor
{
    /// <summary>
    /// Root for application
    /// </summary>
    public class MApp : BApp
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject]
        public Window Window { get; set; } = null!;

        /// <summary>
        /// Whether to display from left to right
        /// </summary>
        [Parameter]
        [ApiDefaultValue(true)]
        public bool LeftToRight { get; set; } = true;

        protected ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();

        public override IDictionary<string, IDictionary<string, object?>?>? Defaults => MasaBlazor!.Defaults;

        protected override bool IsDark => MasaBlazor?.Theme is { Dark: true };

        protected override Task OnInitializedAsync()
        {
            MasaBlazor!.OnThemeChange -= OnThemeChange;
            MasaBlazor.OnThemeChange += OnThemeChange;

            OnThemeChange(MasaBlazor.Theme);

            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await MasaBlazor.Breakpoint.InitAsync();

                IsMasaBlazorReady = true;

                await Window.AddResizeEventListenerAsync();

                StateHasChanged();
            }
        }

        private void OnThemeChange(Theme theme)
        {
            var themeOptions = theme.Dark ? theme.Themes.Dark : theme.Themes.Light;
            var style = ThemeCssBuilder.Build(themeOptions);
            InvokeAsync(async () =>
            {
                await Js.InvokeVoidAsync(JsInteropConstants.UpsertThemeStyle, "masa-blazor-theme-stylesheet", style);
                StateHasChanged();
            });
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-application";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-application")
                        .Add(() =>
                        {
                            var suffix = LeftToRight ? "ltr" : "rtl";
                            return $"{prefix}--is-{suffix}";
                        })
                        .AddTheme(IsDark);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-application--wrap");
                });

            Attributes.Add("data-app", true);
        }
    }
}
