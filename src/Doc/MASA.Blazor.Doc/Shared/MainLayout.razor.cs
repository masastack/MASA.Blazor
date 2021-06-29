using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Doc.CLI.Models;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILanguageService LanguageService { get; set; }

        [Inject]
        public DemoService DemoService { get; set; }

        [Inject]
        public IJSRuntime JsInterop { get; set; }

        [Parameter]
        public DemoMenuItemModel[] MenuItems { get; set; } = { };

        protected override async Task OnInitializedAsync()
        {
            MenuItems = await DemoService.GetMenuAsync();

            StateHasChanged();
            await DemoService.InitializeDemos();

            LanguageService.LanguageChanged += OnLanguageChanged;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await JsInterop.InvokeVoidAsync("window.MASA.DocSearch.init", CurrentLanguage);
            }
        }

        public async Task ChangePrevNextNav(string currentTitle)
        {
            //if (string.IsNullOrWhiteSpace(currentTitle))
            //    return;

            //var currentSubmenuUrl = DemoService.GetCurrentSubMenuUrl();
            //var prevNext = await DemoService.GetPrevNextMenu(currentSubmenuUrl, currentTitle);

        }

        private async void OnLanguageChanged(object sender, CultureInfo culture)
        {
            //await JsInterop.InvokeVoidAsync("window.MASA.DocSearch.localeChange", culture.Name);
            //await InvokeAsync(StateHasChanged);
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            //_href = NavigationManager.Uri;
        }

        public void Dispose()
        {
            LanguageService.LanguageChanged -= OnLanguageChanged;
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
