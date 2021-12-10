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
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Utils;

namespace MASA.Blazor.Doc.Shared
{
    public partial class BaseLayout : IDisposable
    {
        [Parameter]
        public bool IsChinese { get; set; }

        private string _searchBorderColor = "#00000000";
        private string _languageIcon;
        private bool _isShowMiniLogo = true;

        public StringNumber SelectTab { get; set; } = 0;

        [Inject]
        public I18n I18n { get; set; }

        [Inject]
        public DemoService DemoService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public GlobalConfigs GlobalConfig { get; set; }

        [Parameter]
        public bool Drawer { get; set; } = true;

        [Parameter]
        public bool Temporary { get; set; } = true;

        public void UpdateNav(bool drawer, bool temporary = true)
        {
            Drawer = drawer;
            Temporary = temporary;

            InvokeAsync(StateHasChanged);
        }

        private void TurnLanguage()
        {
            IsChinese = !IsChinese;
            var lang = IsChinese ? "zh-CN" : "en-US";

            ChangeLanguage(lang);

            GlobalConfig.Language = lang;
            GlobalConfig.SaveChanges();
        }

        private void ChangeLanguage(string lang)
        {
            _languageIcon = $"{lang}.png";
            I18n.SetLang(lang);
        }

        protected override void OnInitialized()
        {
            string lang = GlobalConfig.Language ?? CultureInfo.CurrentCulture.Name;
            if (GlobalConfig.Language != null)
                lang = GlobalConfig.Language;
            else if(GlobalConfigs.StaticLanguage is not null)
                lang= GlobalConfigs.StaticLanguage;
            else
                lang = CultureInfo.CurrentCulture.Name;


            IsChinese = lang == "zh-CN";

            ChangeLanguage(lang);

            Navigation.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (e.Location == Navigation.BaseUri)
                _isShowMiniLogo = true;
            else
                _isShowMiniLogo = false;

            if (e.Location.Contains("meet-the-team"))
                SelectTab = 3;

            StateHasChanged();
        }

        private void ShowDraw()
        {
            UpdateNav(true);
        }

        public string T(string key)
        {
            return I18n.LanguageMap.GetValueOrDefault(key);
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;

            GC.SuppressFinalize(this);
        }
    }
}
