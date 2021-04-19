using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private bool _drawerVisible = false;

        public string CurrentLanguage => LanguageService.CurrentCulture.Name;

        private bool _isMobile;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILanguageService LanguageService { get; set; }

        [Inject]
        public DemoService DemoService { get; set; }

        [Inject]
        public IJSRuntime JsInterop { get; set; }

        internal PrevNextNav PrevNextNav { get; set; }

        protected override async Task OnInitializedAsync()
        {
            StateHasChanged();
            await DemoService.InitializeDemos();

            LanguageService.LanguageChanged += OnLanguageChanged;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInterop.InvokeVoidAsync("window.MASA.DocSearch.init", CurrentLanguage);
            }
        }

        public async Task ChangePrevNextNav(string currentTitle)
        {
            if (string.IsNullOrWhiteSpace(currentTitle))
                return;

            var currentSubmenuUrl = DemoService.GetCurrentSubMenuUrl();
            var prevNext = await DemoService.GetPrevNextMenu(currentSubmenuUrl, currentTitle);

            PrevNextNav?.SetPrevNextNav(prevNext[0], prevNext[1]);
        }

        private async void OnLanguageChanged(object sender, CultureInfo culture)
        {
            await JsInterop.InvokeVoidAsync("window.MASA.DocSearch.localeChange", culture.Name);
            await InvokeAsync(StateHasChanged);
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            LanguageService.LanguageChanged -= OnLanguageChanged;
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
