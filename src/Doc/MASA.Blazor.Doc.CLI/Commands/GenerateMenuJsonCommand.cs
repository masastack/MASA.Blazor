using MASA.Blazor.Doc.CLI.Comparers;
using MASA.Blazor.Doc.CLI.Interfaces;
using MASA.Blazor.Doc.CLI.Wrappers;
using MASA.Blazor.Doc.Models.Extensions;
using MASA.Blazor.Doc.Models;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MASA.Blazor.Doc.CLI.Commands
{
    public class GenerateMenuJsonCommand : IAppCommand
    {
        public string Name => "menu2json";

        //private static readonly Dictionary<string, int> _sortMap = new Dictionary<string, int>()
        //{
        //    ["Docs"] = -2,
        //    ["文档"] = -2,
        //    ["Overview"] = -1,
        //    ["组件总览"] = -1,
        //    ["General"] = 0,
        //    ["通用"] = 0,
        //    ["Layout"] = 1,
        //    ["布局"] = 1,
        //    ["Navigation"] = 2,
        //    ["导航"] = 2,
        //    ["Data Entry"] = 3,
        //    ["数据录入"] = 3,
        //    ["Data Display"] = 4,
        //    ["数据展示"] = 4,
        //    ["Feedback"] = 5,
        //    ["反馈"] = 5,
        //    ["Localization"] = 6,
        //    ["Other"] = 7,
        //    ["其他"] = 7,
        //    ["Charts"] = 8,
        //    ["图表"] = 8
        //};

        //private static readonly Dictionary<string, string> _demoCategoryMap = new Dictionary<string, string>()
        //{
        //    ["Components"] = "组件",
        //    ["Charts"] = "图表"
        //};

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate json file for menu";
            command.HelpOption();

            var demoDirArgument = command.Argument(
                "demoDir", "[Required] The directory of docs files.");

            var docsDirArgument = command.Argument(
                "docsDir", "[Required] The directory of docs files.");

            var styleDirArgument = command.Argument(
                "styleDir", "[Required] The directory of style files.");

            var outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");

            command.OnExecute(() =>
            {
                string demoDir = demoDirArgument.Value;
                string docsDir = docsDirArgument.Value;
                string styleDir = styleDirArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(demoDir) || !Directory.Exists(demoDir))
                {
                    Console.WriteLine("Invalid demoDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(docsDir) || !Directory.Exists(docsDir))
                {
                    Console.WriteLine("Invalid docsDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(styleDir) || !Directory.Exists(styleDir))
                {
                    Console.WriteLine("Invalid demoDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }

                string demoDirectory = Path.Combine(Directory.GetCurrentDirectory(), demoDir);
                string docsDirectory = Path.Combine(Directory.GetCurrentDirectory(), docsDir);
                string styleDirectory = Path.Combine(Directory.GetCurrentDirectory(), styleDir);

                GenerateFiles(demoDirectory, docsDirectory, styleDirectory, output);

                return 0;
            });
        }

        private void GenerateFiles(string demoDirectory, string docsDirectory, string styleDirectory, string output)
        {
            var demoDirectoryInfo = new DirectoryInfo(demoDirectory);
            if (!demoDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", demoDirectory);
                return;
            }

            var docsDirectoryInfo = new DirectoryInfo(docsDirectory);
            if (!docsDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", docsDirectory);
                return;
            }

            var styleDirectoryInfo = new DirectoryInfo(styleDirectory);
            if (!styleDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", styleDirectory);
                return;
            }

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var docsMenuList = GetSubMenuList(docsDirectoryInfo, true).ToList();

            var categoryDemoMenuList = new Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>>();
            var allComponentMenuList = new List<Dictionary<string, DemoMenuItemModel>>();

            foreach (var subDemoDirectory in demoDirectoryInfo.GetFileSystemInfos().OrderBy(r => r.Name))
            {
                var category = subDemoDirectory.Name;

                var componentMenuList = GetSubMenuList(subDemoDirectory as DirectoryInfo, false).ToList();

                allComponentMenuList.AddRange(componentMenuList);

                var componentMenuI18N = componentMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                foreach (var component in componentMenuI18N)
                {
                    if (!categoryDemoMenuList.ContainsKey(component.Key))
                    {
                        categoryDemoMenuList[component.Key] = new Dictionary<string, IEnumerable<DemoMenuItemModel>>();
                    }

                    categoryDemoMenuList[component.Key].Add(category, component.Value);
                }
            }

            var categoryStyleMenuList = new Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>>();
            var allStyleMenuList = new List<Dictionary<string, DemoMenuItemModel>>();

            foreach (var subStyleDirectory in styleDirectoryInfo.GetFileSystemInfos().OrderBy(r => r.Name))
            {
                var category = subStyleDirectory.Name;

                var styleMenuList = GetSubMenuList(subStyleDirectory as DirectoryInfo, false).ToList();

                allStyleMenuList.AddRange(styleMenuList);

                var styleMenuI18N = styleMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                foreach (var style in styleMenuI18N)
                {
                    if (!categoryStyleMenuList.ContainsKey(style.Key))
                    {
                        categoryStyleMenuList[style.Key] = new Dictionary<string, IEnumerable<DemoMenuItemModel>>();
                    }

                    categoryStyleMenuList[style.Key].Add(category, style.Value);
                }
            }

            var docsMenuI18N = docsMenuList
                .SelectMany(x => x)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(x => x.Value));

            foreach (var lang in new[] { "zh-CN", "en-US" })
            {
                var menus = new List<DemoMenuItemModel>();

                var children = docsMenuI18N[lang].OrderBy(x => x.Order).ToArray();

                var categoryComponent = categoryDemoMenuList[lang];
                var categoryStyle = categoryStyleMenuList[lang];

                var componentMenus = new List<DemoMenuItemModel>();

                foreach (var component in categoryComponent)
                {
                    componentMenus.Add(new DemoMenuItemModel()
                    {
                        Order = Array.IndexOf(ConfigWrapper.Config.GenerateRule.Menus.Select(x => x.Key).ToArray(), component.Key) + 1,
                        Title = ConfigWrapper.Config.GenerateRule.Menus.First(menu => menu.Key == component.Key).Descriptions
                            .First(desc => desc.Lang == lang).Description,
                        Type = "component",
                        Url = component.Key.StructureUrl(),
                        Children = component.Value.OrderBy(x => x.Order).ToArray()
                    });
                }

                var styleMenus = new List<DemoMenuItemModel>();

                foreach (var style in categoryStyle)
                {
                    styleMenus.Add(new DemoMenuItemModel()
                    {
                        Order = Array.IndexOf(ConfigWrapper.Config.GenerateRule.Menus.Select(x => x.Key).ToArray(), style.Key) + 1,
                        Title = ConfigWrapper.Config.GenerateRule.Menus.First(menu => menu.Key == style.Key).Descriptions
                            .First(desc => desc.Lang == lang).Description,
                        Type = "component",
                        Url = style.Key.StructureUrl(),
                        Children = style.Value.OrderBy(x => x.Order).ToArray()
                    });
                }

                //Children 4 will be component menu
                children[4].Children = componentMenus[0].Children.SelectMany(r => r.Children)
                    .OrderBy(r => r.Order)
                    .ThenBy(r => r.Title)
                    .ToArray();

                //Children 3 will be style menu
                children[3].Children = styleMenus[0].Children.SelectMany(r => r.Children)
                    .OrderBy(r => r.Order)
                    .ThenBy(r => r.Title)
                    .ToArray();

                menus.AddRange(children);

                var json = JsonSerializer.Serialize(menus, jsonOptions);

                var configFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), output);
                if (!Directory.Exists(configFileDirectory))
                {
                    Directory.CreateDirectory(configFileDirectory);
                }

                var configFilePath = Path.Combine(configFileDirectory, $"menu.{lang}.json");

                if (File.Exists(configFilePath))
                {
                    File.Delete(configFilePath);
                }

                File.WriteAllText(configFilePath, json);

                var componentI18N = allComponentMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                var demos = componentI18N[lang];

                var demosPath = Path.Combine(configFileDirectory, $"demos.{lang}.json");

                if (File.Exists(demosPath))
                {
                    File.Delete(demosPath);
                }

                json = JsonSerializer.Serialize(demos, jsonOptions);
                File.WriteAllText(demosPath, json);

                var docs = docsMenuI18N[lang];
                var docsPath = Path.Combine(configFileDirectory, $"docs.{lang}.json");

                if (File.Exists(docsPath))
                {
                    File.Delete(docsPath);
                }

                json = JsonSerializer.Serialize(docs, jsonOptions);
                File.WriteAllText(docsPath, json);
            }
        }

        private IEnumerable<Dictionary<string, DemoMenuItemModel>> GetSubMenuList(DirectoryInfo directory, bool isTopMenu)
        {
            if (isTopMenu)
            {
                // 设置文档首页一级导航

                foreach (var menuDir in directory.GetDirectories())
                {
                    foreach (var menuItem in menuDir.GetFileSystemInfos().OrderBy(r => r.Name))
                    {
                        if (menuItem.Name == "index.json")
                        {
                            var content = File.ReadAllText(menuItem.FullName, Encoding.UTF8);
                            var data = JObject.Parse(content);
                            foreach (var titleItem in data["title"].ToArray())
                            {
                                yield return new Dictionary<string, DemoMenuItemModel>
                                {
                                    [titleItem["lang"].ToString()] = new DemoMenuItemModel
                                    {
                                        Order = data["order"].ToObject<int>(),
                                        Title = titleItem["content"].ToString(),
                                        Url = $"{menuDir.Name}".StructureUrl(),
                                        Icon = data["icon"].ToString(),
                                        Type = "menuItem",
                                        Children = GetSubMenuChildren(menuDir, titleItem["lang"].ToString()).OrderBy(r => r.Order)
                                            .ThenBy(r => r.Title).ToArray()
                                    }
                                };
                            }
                        }
                    }
                }
            }
            else
            {
                // 设置首页文档名为UI组件的一级导航的二级导航列表
                // 同时影响menu.json和demos.json

                var componentI18N = GetComponentI18N(directory);
                foreach (var group in componentI18N.GroupBy(x => x.Value.Type))
                {
                    var menu = new Dictionary<string, DemoMenuItemModel>();

                    foreach (var component in group.GroupBy(x => x.Key))
                    {
                        menu.Add(component.Key, new DemoMenuItemModel()
                        {
                            Order = ConfigWrapper.DocsNavOrder[group.Key],
                            Title = group.Key, // TODO: 似乎无用处
                            Type = "itemGroup",
                            Children = group.Select(x => new DemoMenuItemModel()
                            {
                                Title = x.Value.Title,
                                SubTitle = x.Value.Subtitle,
                                Url = $"{directory.Name}/{x.Value.Title}".StructureUrl(),
                                Type = "menuItem",
                                Order = x.Value.Order,
                                Cover = x.Value.Cover,
                                Children = x.Value.Children.Select(y => new DemoMenuItemModel()
                                {
                                    Title = y.Title,
                                    SubTitle = y.Subtitle,
                                    Url = $"{directory.Name}/{y.Title}".StructureUrl(),
                                    Type = "menuItem",
                                    Order = y.Order,
                                    Cover = y.Cover,
                                }).ToArray()
                            })
                                .OrderBy(x => x.Title, new MenuComparer())
                                .ToArray(),
                        });
                    }

                    yield return menu;
                }
            }
        }

        /// <summary>
        /// 设置docs/xxx/下非index的项，表现在文档首页二级导航（不包括UI组件）
        /// </summary>
        /// <param name="menuDir"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        private IEnumerable<DemoMenuItemModel> GetSubMenuChildren(DirectoryInfo menuDir, string lang)
        {
            foreach (var menuItem in menuDir.GetFileSystemInfos().OrderBy(r => r.Name))
            {
                if (menuItem.Extension == ".md")
                {
                    var args = menuItem.Name.Split('.');
                    if (args[1] == lang)
                    {
                        var content = File.ReadAllText(menuItem.FullName);
                        var data = DocWrapper.ParseHeader(content);
                        var titles = DocWrapper.ParseTitle(content);

                        yield return new DemoMenuItemModel
                        {
                            Order = Convert.ToInt32(data["order"]),
                            Title = data["title"],
                            Url = $"{menuDir.Name}/{args[0]}".StructureUrl(),
                            Type = "menuItem",
                            Contents = titles.Select(r => new ContentsItem
                            {
                                Id = r.HashSection(),
                                Title = r
                            }).ToList()
                        };
                    }
                }
            }
        }

        private IEnumerable<KeyValuePair<string, DemoComponentModel>> GetComponentI18N(DirectoryInfo directory)
        {
            IList<Dictionary<string, DemoComponentModel>> componentList = null;

            foreach (var component in directory.GetFileSystemInfos().OrderBy(r => r.Name))
            {
                if (!(component is DirectoryInfo componentDirectory))
                    continue;

                var componentDic = FormatDocDir(componentDirectory);

                var childrenDir = componentDirectory.GetFileSystemInfos("children")?.FirstOrDefault();

                if (childrenDir != null && childrenDir.Exists)
                {
                    var subDemoDirectoryInfo = (childrenDir as DirectoryInfo);

                    var subDirectories = subDemoDirectoryInfo.GetFileSystemInfos().OrderBy(r => r.Name);

                    foreach (var subComponent in subDirectories)
                    {
                        if (!(subComponent is DirectoryInfo subComponentDirectory)) continue;

                        var subComponentDic = FormatDocDir(subComponentDirectory);

                        foreach (var (language, subComponentModel) in subComponentDic)
                        {
                            componentDic[language].Children.Add(subComponentModel);
                        }
                    }
                }

                componentList ??= new List<Dictionary<string, DemoComponentModel>>();
                componentList.Add(componentDic);
            }

            if (componentList == null)
                return Enumerable.Empty<KeyValuePair<string, DemoComponentModel>>();

            var componentI18N = componentList
                .SelectMany(x => x).OrderBy(x => ConfigWrapper.DocsNavOrder[x.Value.Type]);

            return componentI18N;
        }

        private Dictionary<string, DemoComponentModel> FormatDocDir(DirectoryInfo componentDirectory)
        {
            Dictionary<string, DemoComponentModel> dict = new();

            FileSystemInfo docDir = componentDirectory.GetFileSystemInfos("doc")?.FirstOrDefault();

            if (docDir != null && docDir.Exists)
            {
                foreach (FileSystemInfo docItem in (docDir as DirectoryInfo).GetFileSystemInfos().OrderBy(r => r.Name))
                {
                    var language = docItem.Name.Replace("index.", "").Replace(docItem.Extension, "");

                    var content = File.ReadAllText(docItem.FullName);

                    var (matter, desc, _) = DocWrapper.ParseDemoDoc(content);

                    var model = new DemoComponentModel(matter, desc);

                    dict[language] = model;
                }
            }

            return dict;
        }
    }
}