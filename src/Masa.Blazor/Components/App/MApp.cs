using BlazorComponent.Web;

namespace Masa.Blazor
{
    /// <summary>
    /// Root for application
    /// </summary>
    public partial class MApp : BApp, IThemeable
    {
        [Inject]
        public HeadJsInterop HeadJsInterop { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        [Inject]
        public Window Window { get; set; }

        /// <summary>
        /// Whether to display from left to right
        /// </summary>
        [Parameter]
        public bool LeftToRight { get; set; } = true;

        protected ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();


        protected override Task OnInitializedAsync()
        {
            MasaBlazor.OnThemeChange -= OnThemeChange;
            MasaBlazor.OnThemeChange += OnThemeChange;
            
            OnThemeChange(MasaBlazor.Theme);

            return base.OnInitializedAsync();
        }

        private void OnThemeChange(Theme theme)
        {
            var themeOptions = theme.Dark ? theme.Themes.Dark : theme.Themes.Light;
            Dark = theme.Dark;
            ThemeStyleMarkups = ThemeCssBuilder.Build(themeOptions);
            StateHasChanged();
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await MasaBlazor.Breakpoint.InitAsync();
                await Window.InitializeAsync();

                StateHasChanged();
            }
        }
    }
}
