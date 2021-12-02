using BlazorComponent.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Utils
{
    public class I18nHelper
    {
        public static void AddLang()
        {
            var root = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Locale"));
            FileInfo[] files = root.GetFiles();

            if (files.Length > 0)
            {
                var defaultLanguage = CultureInfo.CurrentCulture.Name;

                foreach (var file in files)
                {
                    var language = file.Name[..file.Name.LastIndexOf('.')];
                    var content = File.ReadAllText(file.FullName);

                    var isDefaultLanguage = defaultLanguage == language;

                    I18n.AddLang(language, JsonSerializer.Deserialize<Dictionary<string, string>>(content), isDefaultLanguage);
                }
            }
        }
    }
}
