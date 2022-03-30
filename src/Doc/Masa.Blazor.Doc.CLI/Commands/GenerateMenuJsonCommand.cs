using Masa.Blazor.Doc.CLI.Comparers;
using Masa.Blazor.Doc.CLI.Interfaces;
using Masa.Blazor.Doc.CLI.Wrappers;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Models.Extensions;
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

namespace Masa.Blazor.Doc.CLI.Commands
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

            var outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");

            command.OnExecute(() =>
            {
                string demosDir = demoDirArgument.Value;
                string docsDir = docsDirArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(demosDir) || !Directory.Exists(demosDir))
                {
                    Console.WriteLine("Invalid demoDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(docsDir) || !Directory.Exists(docsDir))
                {
                    Console.WriteLine("Invalid docsDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }

                string demosDirectory = Path.Combine(Directory.GetCurrentDirectory(), demosDir);
                string docsDirectory = Path.Combine(Directory.GetCurrentDirectory(), docsDir);

                GenerateFiles(demosDirectory, docsDirectory, output);

                return 0;
            });
        }

        private void GenerateFiles(string demosDirectory, string docsDirectory, string output)
        {
            var demosDirectoryInfo = new DirectoryInfo(demosDirectory);
            if (!demosDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", demosDirectory);
                return;
            }

            var docsDirectoryInfo = new DirectoryInfo(docsDirectory);
            if (!docsDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", docsDirectory);
                return;
            }

            VisitDemosDirectory(demosDirectoryInfo, docsDirectoryInfo, out var categoryDemoMenuList, out var allComponentMenuList, out var categoryStyleMenuList);
            WriteToJsonFiles(output, docsDirectoryInfo, categoryDemoMenuList, categoryStyleMenuList, allComponentMenuList);
        }

        private void VisitDemosDirectory(DirectoryInfo demosDirectoryInfo, DirectoryInfo docsDirectoryInfo, out Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>> categoryComponentsMenuItems, out List<Dictionary<string, DemoMenuItemModel>> componentsMenuItems, out Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>> categoryStyleAndAnimationsMenuItems)
        {
            //Components
            var componentsDirectory = demosDirectoryInfo.GetFileSystemInfos().FirstOrDefault(info => info.Name == "Components") as DirectoryInfo;
            categoryComponentsMenuItems = new Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>>();

            componentsMenuItems = GetMenuItems(componentsDirectory, false).ToList();
            var componentsMenuI18NItems = componentsMenuItems
                .SelectMany(x => x)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

            foreach (var componentsMenuI18NItem in componentsMenuI18NItems)
            {
                if (!categoryComponentsMenuItems.ContainsKey(componentsMenuI18NItem.Key))
                {
                    categoryComponentsMenuItems[componentsMenuI18NItem.Key] = new Dictionary<string, IEnumerable<DemoMenuItemModel>>();
                }

                categoryComponentsMenuItems[componentsMenuI18NItem.Key].Add(componentsDirectory.Name, componentsMenuI18NItem.Value);
            }

            //Styles & animations
            var stylesAndAnimationsDirectory = demosDirectoryInfo.GetFileSystemInfos().FirstOrDefault(info => info.Name == "StylesAndAnimations") as DirectoryInfo;
            categoryStyleAndAnimationsMenuItems = new Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>>();

            var styleAndAnimationsMenuItems = GetMenuItems(stylesAndAnimationsDirectory, false).ToList();
            var styleAndAnimationsMenuI18NItems = styleAndAnimationsMenuItems
                .SelectMany(x => x)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

            foreach (var styleAndAnimationsMenuI18NItem in styleAndAnimationsMenuI18NItems)
            {
                if (!categoryStyleAndAnimationsMenuItems.ContainsKey(styleAndAnimationsMenuI18NItem.Key))
                {
                    categoryStyleAndAnimationsMenuItems[styleAndAnimationsMenuI18NItem.Key] = new Dictionary<string, IEnumerable<DemoMenuItemModel>>();
                }

                categoryStyleAndAnimationsMenuItems[styleAndAnimationsMenuI18NItem.Key].Add(stylesAndAnimationsDirectory.Name, styleAndAnimationsMenuI18NItem.Value);
            }
        }

        private IEnumerable<Dictionary<string, DemoMenuItemModel>> GetMenuItems(DirectoryInfo directory, bool isTopMenu)
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
                                var children = GetSubMenuChildren(menuDir, titleItem["lang"].ToString()).OrderBy(r => r.Order)
                                            .ThenBy(r => r.Title).ToArray();
                                yield return new Dictionary<string, DemoMenuItemModel>
                                {
                                    [titleItem["lang"].ToString()] = new DemoMenuItemModel
                                    {
                                        Order = data["order"].ToObject<int>(),
                                        Title = titleItem["content"].ToString(),
                                        Url = children != null && children.Length > 0 ? $"{menuDir.Name}".StructureUrl() : null,
                                        Icon = data["icon"].ToString(),
                                        Type = "menuItem",
                                        Children = children
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
                                Url = x.Value.Children.Count == 0 ? $"{directory.Name}/{x.Value.Title}".StructureUrl() : null,
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

        private void WriteToJsonFiles(string output, DirectoryInfo docsDirectoryInfo, Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>> categoryDemoMenuList, Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>> categoryStyleMenuList, List<Dictionary<string, DemoMenuItemModel>> allComponentMenuList)
        {
            var docsMenuItems = GetMenuItems(docsDirectoryInfo, true).ToList();
            var docsMenuI18N = docsMenuItems
                 .SelectMany(x => x)
                 .GroupBy(x => x.Key)
                 .ToDictionary(x => x.Key, x => x.Select(x => x.Value));

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            foreach (var lang in new[] { "zh-CN", "en-US" })
            {
                //1、menu.{lang}.json
                var menuItems = docsMenuI18N[lang].OrderBy(x => x.Order).ToList();

                //Components menus
                var componentsMenus = new List<DemoMenuItemModel>();
                var categoryComponent = categoryDemoMenuList[lang];
                foreach (var component in categoryComponent)
                {
                    componentsMenus.Add(new DemoMenuItemModel()
                    {
                        Order = Array.IndexOf(ConfigWrapper.Config.GenerateRule.Menus.Select(x => x.Key).ToArray(), component.Key) + 1,
                        Title = ConfigWrapper.Config.GenerateRule.Menus.First(menu => menu.Key == component.Key).Descriptions
                            .First(desc => desc.Lang == lang).Description,
                        Type = "component",
                        Url = component.Key.StructureUrl(),
                        Children = component.Value.OrderBy(x => x.Order).ToArray()
                    });
                }

                //Children 4 will be components menu
                menuItems[4].Children = componentsMenus[0].Children.SelectMany(r => r.Children)
                    .OrderBy(r => r.Order)
                    .ThenBy(r => r.Title)
                    .ToArray();

                //Styles & animations menu
                var stylesAndAnimationsMenus = new List<DemoMenuItemModel>();
                var categoryStyle = categoryStyleMenuList[lang];
                foreach (var style in categoryStyle)
                {
                    stylesAndAnimationsMenus.Add(new DemoMenuItemModel()
                    {
                        Order = Array.IndexOf(ConfigWrapper.Config.GenerateRule.Menus.Select(x => x.Key).ToArray(), style.Key) + 1,
                        Title = ConfigWrapper.Config.GenerateRule.Menus.First(menu => menu.Key == style.Key).Descriptions
                            .First(desc => desc.Lang == lang).Description,
                        Type = "component",
                        Url = style.Key.StructureUrl(),
                        Children = style.Value.OrderBy(x => x.Order).ToArray()
                    });
                }

                //Children 3 will be styles & animations menu
                menuItems[3].Children = stylesAndAnimationsMenus[0].Children.SelectMany(r => r.Children)
                    .OrderBy(r => r.Order)
                    .ThenBy(r => r.Title)
                    .ToArray();

                var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), output);
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }
                var menuFilePath = Path.Combine(outputDirectory, $"menu.{lang}.json");

                var menuJson = JsonSerializer.Serialize(menuItems, jsonOptions);
                File.WriteAllText(menuFilePath, menuJson);

                //2、demos.{lang}.json
                var componentI18N = allComponentMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                var demos = componentI18N[lang];
                var demosPath = Path.Combine(outputDirectory, $"demos.{lang}.json");

                var demosJson = JsonSerializer.Serialize(demos, jsonOptions);
                File.WriteAllText(demosPath, demosJson);

                //3、docs.{lang}.json
                var docs = docsMenuI18N[lang];
                var docsPath = Path.Combine(outputDirectory, $"docs.{lang}.json");

                var docsJson = JsonSerializer.Serialize(docs, jsonOptions);
                File.WriteAllText(docsPath, docsJson);
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