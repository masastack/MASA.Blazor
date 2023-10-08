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

        protected ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();

        public override IDictionary<string, IDictionary<string, object?>?>? Defaults => MasaBlazor!.Defaults;

        protected override bool IsDark => MasaBlazor?.Theme is { Dark: true };

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.OnThemeChange += OnThemeChange;
            MasaBlazor.RTLChanged += MasaBlazorOnRTLChanged;

            OnThemeChange(MasaBlazor.Theme);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            
            if (HostedInWebAssembly)
            {
                await OnJSInteropReadyAsync();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && !HostedInWebAssembly)
            {
                await OnJSInteropReadyAsync();
                StateHasChanged();
            }
        }

        private void MasaBlazorOnRTLChanged(object? sender, EventArgs e)
        {
            InvokeStateHasChanged();
        }
        
        private async Task OnJSInteropReadyAsync()
        {
            await MasaBlazor.Breakpoint.InitAsync();
            await Window.AddResizeEventListenerAsync();
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
                            var suffix = MasaBlazor.RTL ? "rtl" : "ltr";
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            MasaBlazor.OnThemeChange -= OnThemeChange;
            MasaBlazor.RTLChanged -= MasaBlazorOnRTLChanged;
        }
    }
}
