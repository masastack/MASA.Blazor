using BlazorComponent.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Shared
{
    public partial class BaseLayout
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
        }

        public string T(string key)
        {
            return I18n.LanguageMap.GetValueOrDefault(key);
        }

        private async Task Toggle(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                await JSRuntime.InvokeVoidAsync("window.open", url);
            }
        }
    }
}
