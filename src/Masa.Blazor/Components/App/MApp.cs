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
        public IThemeService ThemeService { get; set; } = null!;

        [Inject]
        public Window Window { get; set; } = null!;

        public override IDictionary<string, IDictionary<string, object?>?>? Defaults => MasaBlazor!.Defaults;

        protected override bool IsDark => ThemeService.Dark;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ThemeService.RTLChanged += ThemeServiceOnRTLChanged;
            ThemeService.DarkChanged += ThemeServiceOnDarkChanged;

            ThemeService.UpdateTheme();
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

        private void ThemeServiceOnRTLChanged(bool rtl)
        {
            InvokeStateHasChanged();
        }

        private void ThemeServiceOnDarkChanged(bool dark)
        {
            InvokeStateHasChanged();
        }

        private async Task OnJSInteropReadyAsync()
        {
            await MasaBlazor.Breakpoint.InitAsync();
            await Window.AddResizeEventListenerAsync();
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
                            var suffix = ThemeService.RTL ? "rtl" : "ltr";
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

            ThemeService.RTLChanged -= ThemeServiceOnRTLChanged;
            ThemeService.DarkChanged -= ThemeServiceOnDarkChanged;
        }
    }
}
