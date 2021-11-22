using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Doc.Models;
using MASA.Blazor.Doc.Localization;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        //TODO:use i18n
        private bool _isEnglish;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILanguageService LanguageService { get; set; }

        [Inject]
        public IJSRuntime JsInterop { get; set; }

        [Parameter]
        public DemoMenuItemModel[] MenuItems { get; set; } = { };

        protected override void OnInitialized()
        {
            LanguageService.LanguageChanged += OnLanguageChanged;
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLanguageChanged(object sender, CultureInfo culture)
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
