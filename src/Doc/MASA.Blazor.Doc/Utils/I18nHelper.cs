using BlazorComponent.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Utils
{
    public class I18nHelper
    {
        public static async Task GetLocalesAndAddLang(HttpClient httpClient)
        {
            var languageDict = await httpClient.GetFromJsonAsync<Dictionary<string, string[]>>("_content/MASA.Blazor.Doc/locale/languages.json");
            if (languageDict?.Count > 0)
            {
                string[] languages = languageDict["SupportLanguages"];

                var defaultLanguage = CultureInfo.CurrentCulture.Name;

                foreach (var language in languages)
                {
                    var content = await httpClient.GetFromJsonAsync<Dictionary<string, string>>($"_content/MASA.Blazor.Doc/locale/{language}.json");

                    var isDefaultLanguage = defaultLanguage == language;

                    I18n.AddLang(language, content, isDefaultLanguage);
                }
            }
        }
    }
}
