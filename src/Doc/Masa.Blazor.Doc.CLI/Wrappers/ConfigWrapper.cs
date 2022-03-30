using Masa.Blazor.Doc.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Masa.Blazor.Doc.CLI.Wrappers
{
    public class ConfigWrapper
    {
        public static ConfigModel Config { get; private set; }

        public static Dictionary<string, int> DocsNavOrder { get; private set; } = null;

        static ConfigWrapper()
        {
            var json = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json"));
            Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigModel>(json);

            var docsMenus = Config.GenerateRule.Menus
                .First(menu => menu.Key == "Docs")
                .Children;

            DocsNavOrder = Enumerable.Range(0, docsMenus.Count)
                .ToDictionary(
                    order =>
                    {
                        return docsMenus[order].Descriptions.Select(desc => desc.Description);
                    },
                    order => order
                )
                .SelectMany(d =>
                {
                    return d.Key.Distinct().Select(k => new { k, d.Value });
                })
                .ToDictionary(d => d.k, d => d.Value);
        }
    }
}
