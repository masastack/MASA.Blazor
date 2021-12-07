using BlazorComponent;
using BlazorComponent.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Shared
{
    public partial class BaseLayout : IDisposable
    {
        private bool _isChinese;
        private string _searchBorderColor = "#00000000";
        private string _languageIcon;

        [Inject]
        public I18n I18n { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public StringNumber SelectTab { get; set; }

        private void TurnLanguage()
        {
            _isChinese = !_isChinese;
            var lang = _isChinese ? "zh-CN" : "en-US";
            ChangeLanguage(lang);
        }

        private void ChangeLanguage(string lang)
        {
            _languageIcon = $"{lang}.png";

            I18n.SetLang(lang);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _isChinese = CultureInfo.CurrentCulture.Name == "zh-CN";
            var lang = _isChinese ? "zh-CN" : "en-US";
            ChangeLanguage(lang);

            Navigation.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (Navigation.BaseUri == e.Location)
                SelectTab = null;
            else if (e.Location.Contains("meet-the-team"))
                SelectTab = 2;
            else
                SelectTab = 0;

            StateHasChanged();
        }

        public string T(string key)
        {
            return I18n.LanguageMap.GetValueOrDefault(key);
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }
    }
}
