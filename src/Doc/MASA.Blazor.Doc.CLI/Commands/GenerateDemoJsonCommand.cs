using MASA.Blazor.Doc.CLI.Interfaces;
using MASA.Blazor.Doc.CLI.Wrappers;
using MASA.Blazor.Doc.Models;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MASA.Blazor.Doc.CLI.Commands
{
    public class GenerateDemoJsonCommand : IAppCommand
    {
        public string Name => "demo2json";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate json file of demos";
            command.HelpOption();

            CommandArgument directoryArgument = command.Argument(
                "source", "[Required] The directory of demo files.");

            CommandArgument outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");

            command.OnExecute(() =>
            {
                string source = directoryArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(source) || !Directory.Exists(source))
                {
                    Console.WriteLine($"Invalid source: {source}");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }

                string demoDirectory = Path.Combine(Directory.GetCurrentDirectory(), source);

                try
                {
                    GenerateFiles(demoDirectory, output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return -1;
                }

                return 0;
            });
        }

        private void GenerateFiles(string demoDirectory, string output)
        {
            DirectoryInfo demoDirectoryInfo = new DirectoryInfo(demoDirectory);
            if (!demoDirectoryInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("{0} is not a directory", demoDirectory);
                return;
            }

            List<Dictionary<string, DemoComponentModel>> componentList = new();
            List<string> demoTypes = new();

            var directories = demoDirectoryInfo.GetFileSystemInfos()
                .SelectMany(x => (x as DirectoryInfo).GetFileSystemInfos()).OrderBy(r => r.Name);

            foreach (FileSystemInfo component in directories)
            {
                if (!(component is DirectoryInfo componentDirectory)) continue;

                Dictionary<string, DemoComponentModel> componentDic = new();
                Dictionary<string, List<DemoItemModel>> demoDic = new();

                var childrenDir = componentDirectory.GetFileSystemInfos("children")?.FirstOrDefault();
                if (childrenDir != null && childrenDir.Exists)
                {
                    var subDemoDirectoryInfo = (childrenDir as DirectoryInfo);

                    var subDirectories = subDemoDirectoryInfo.GetFileSystemInfos().OrderBy(r => r.Name);

                    foreach (var subComponent in subDirectories)
                    {
                        // TODO: check

                        if (!(subComponent is DirectoryInfo subComponentDirectory)) continue;

                        var subComponentDic = FormatDocDir(subComponentDirectory);

                        (demoDic, var subTypes) = FormatDemoDir(subComponentDirectory, demoDirectoryInfo);

                        demoTypes.AddRange(subTypes);

                        foreach (var (language, demoItems) in demoDic)
                        {
                            if (!subComponentDic.ContainsKey(language)) continue;

                            subComponentDic[language].DemoList.AddRange(demoItems);
                            subComponentDic[language].LastWriteTime = subComponentDirectory.LastWriteTime;
                        }

                        componentList.Add(subComponentDic);
                    }
                }
                else
                {
                    componentDic = FormatDocDir(componentDirectory);
                    (demoDic, var types) = FormatDemoDir(componentDirectory, demoDirectoryInfo);
                    demoTypes.AddRange(types);

                    foreach (var (language, demoItems) in demoDic)
                    {
                        if (!componentDic.ContainsKey(language)) continue;

                        componentDic[language].DemoList.AddRange(demoItems);
                        componentDic[language].LastWriteTime = componentDirectory.LastWriteTime;
                    }

                    componentList.Add(componentDic);
                }
            }

            string configFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), output);

            if (!Directory.Exists(configFileDirectory))
            {
                Directory.CreateDirectory(configFileDirectory);
            }

            var componentI18N = componentList
                .SelectMany(x => x).GroupBy(x => x.Key);

            foreach (IGrouping<string, KeyValuePair<string, DemoComponentModel>> componentDic in componentI18N)
            {
                IEnumerable<DemoComponentModel> components = componentDic.Select(x => x.Value);
                string componentJson = JsonSerializer.Serialize(components, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                string configFilePath = Path.Combine(configFileDirectory, $"components.{componentDic.Key}.json");

                if (File.Exists(configFilePath))
                {
                    File.Delete(configFilePath);
                }

                File.WriteAllText(configFilePath, componentJson);

                Console.WriteLine("Generate demo file to {0}", configFilePath);
            }

            var demoJson = JsonSerializer.Serialize(demoTypes, new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            string demoFilePath = Path.Combine(configFileDirectory, $"demoTypes.json");
            if (File.Exists(demoFilePath))
            {
                File.Delete(demoFilePath);
            }

            File.WriteAllText(demoFilePath, demoJson);
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
                    string content = File.ReadAllText(docItem.FullName);
                    var (meta, desc, others) = DocWrapper.ParseDemoDoc(content);

                    var model = new DemoComponentModel()
                    {
                        Title = meta["title"],
                        SubTitle = meta.TryGetValue("subtitle", out string subtitle) ? subtitle : null,
                        Type = meta["type"],
                        Desc = desc,
                        Cols = meta.TryGetValue("cols", out var cols) ? int.Parse(cols) : (int?)null,
                        Cover = meta.TryGetValue("cover", out var cover) ? cover : null,
                        OtherDocs = others
                    };

                    dict[language] = model;
                }
            }

            return dict;
        }

        private(Dictionary<string, List<DemoItemModel>>, List<string> demoTypes) FormatDemoDir(DirectoryInfo componentDirectory,
            DirectoryInfo demosDirectory)
        {
            List<string> demoTypes = new();
            Dictionary<string, List<DemoItemModel>> dict = new();

            //TODO: demo文件夹仅仅是为了兼容之前的写法，待没有示例在demo文件夹内时可删除
            var demoDir = componentDirectory.GetFileSystemInfos("demo")?.FirstOrDefault();
            var usageDir = componentDirectory.GetFileSystemInfos("usage")?.FirstOrDefault();
            var propsDir = componentDirectory.GetFileSystemInfos("props")?.FirstOrDefault();
            var eventsDir = componentDirectory.GetFileSystemInfos("events")?.FirstOrDefault();
            var contentsDir = componentDirectory.GetFileSystemInfos("contents")?.FirstOrDefault();
            var miscDir = componentDirectory.GetFileSystemInfos("misc")?.FirstOrDefault();

            FormatDemoSystemInfos(demoDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Props);
            FormatDemoSystemInfos(usageDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Usage);
            FormatDemoSystemInfos(propsDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Props);
            FormatDemoSystemInfos(eventsDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Events);
            FormatDemoSystemInfos(contentsDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Contents);
            FormatDemoSystemInfos(miscDir, demosDirectory, ref dict, ref demoTypes, DemoGroup.Misc);

            return (dict, demoTypes);
        }

        private void FormatDemoSystemInfos(FileSystemInfo dir, DirectoryInfo demosDirectory, ref Dictionary<string, List<DemoItemModel>> dict,
            ref List<string> demoTypes, DemoGroup group)
        {
            if (dir == null || !dir.Exists) return;

            foreach (IGrouping<string, FileSystemInfo> demo in (dir as DirectoryInfo).GetFileSystemInfos()
                     .OrderBy(r => r.Name)
                     .GroupBy(x => x.Name.Replace(x.Extension, "")
                         .Replace("-", "")
                         .Replace("_", "")
                         .Replace("Demo", "")
                         .ToLower()))
            {
                List<FileSystemInfo> showCaseFiles = demo.ToList();
                FileSystemInfo razorFile = showCaseFiles.FirstOrDefault(x => x.Extension == ".razor");
                FileSystemInfo descriptionFile = showCaseFiles.FirstOrDefault(x => x.Extension == ".md");
                string code = razorFile != null ? File.ReadAllText(razorFile.FullName) : null;

                string demoType =
                    $"{demosDirectory.Name}{razorFile.FullName.Replace(demosDirectory.FullName, "").Replace("/", ".").Replace("\\", ".").Replace(razorFile.Extension, "")}";
                demoTypes.Add(demoType);

                (DescriptionYaml meta, string style, Dictionary<string, string> descriptions) descriptionContent =
                    descriptionFile != null ? DocWrapper.ParseDescription(File.ReadAllText(descriptionFile.FullName)) : default;

                foreach (var (language, value) in descriptionContent.meta.Title)
                {
                    var model = new DemoItemModel()
                    {
                        Title = value,
                        Order = descriptionContent.meta.Order,
                        Iframe = descriptionContent.meta.Iframe,
                        Code = code,
                        Description = descriptionContent.descriptions[language],
                        Name = descriptionFile?.Name.Replace(".md", ""),
                        Style = descriptionContent.style,
                        Debug = descriptionContent.meta.Debug,
                        Docs = descriptionContent.meta.Docs,
                        Type = demoType,
                        Group = group
                    };

                    if (!dict.ContainsKey(language))
                    {
                        dict[language] = new List<DemoItemModel>();
                    }

                    dict[language].Add(model);
                }
            }
        }
    }
}