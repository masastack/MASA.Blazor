using BlazorComponent;
using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Globalization;

namespace Masa.Blazor.Doc.Shared
{
    public partial class BaseLayout : IDisposable
    {
        private string _searchBorderColor = "#00000000";
        private string _languageIcon;
        private bool _isShowMiniLogo = true;
        private StringNumber _selectTab = 0;

        public StringNumber SelectTab
        {
            get
            {
                var url = Navigation.Uri;
                if (url == Navigation.BaseUri)
                {
                    return 0;
                }
                else if (url.Contains("about/meet-the-team"))
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (value != null && value.AsT1 != 3 && value.AsT1 != 4)
                {
                    _selectTab = value;
                }
            }
        }

        [Inject]
        public I18n I18n { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public I18nConfig I18nConfig { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        public bool IsChinese { get; set; }

        public bool Drawer { get; set; } = true;

        public bool ShowSetting { get; set; }

        public bool Temporary { get; set; } = true;

        public void UpdateNav(bool drawer, bool temporary = true)
        {
            Drawer = drawer;
            Temporary = temporary;
        }

        private void TurnLanguage()
        {
            IsChinese = !IsChinese;
            var lang = IsChinese ? "zh-CN" : "en-US";

            ChangeLanguage(lang);

            I18nConfig.Language = lang;
        }

        private void ChangeLanguage(string lang)
        {
            _languageIcon = $"{lang}.png";
        }

        protected override void OnInitialized()
        {
            string lang = I18nConfig.Language ?? CultureInfo.CurrentCulture.Name;

            IsChinese = lang == "zh-CN";

            ChangeLanguage(lang);

            Navigation.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var isShowMiniLogo = _isShowMiniLogo;

            if (e.Location == Navigation.BaseUri)
                _isShowMiniLogo = true;
            else
                _isShowMiniLogo = false;

            var selectTab = SelectTab;
            if (e.Location.Contains("meet-the-team"))
                SelectTab = 2;
            else if (e.Location != Navigation.BaseUri)
                SelectTab = 1;

            if ((isShowMiniLogo != _isShowMiniLogo || selectTab != _selectTab) && MasaBlazor.Breakpoint.Mobile)
            {
                _ = InvokeAsync(StateHasChanged);
            }
        }

        private void ShowDraw()
        {
            UpdateNav(true);
        }

        public string T(string key)
        {
            return I18n.T(key);
        }

        public void Dispose()
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }
    }
}
