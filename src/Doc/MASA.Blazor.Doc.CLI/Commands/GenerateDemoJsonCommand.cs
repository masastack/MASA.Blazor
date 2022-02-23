using Masa.Blazor.Doc.CLI.Interfaces;
using Masa.Blazor.Doc.CLI.Wrappers;
using Masa.Blazor.Doc.Models;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Blazor.Doc.CLI.Commands
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

                string demosDirectory = Path.Combine(Directory.GetCurrentDirectory(), source);

                try
                {
                    GenerateFiles(demosDirectory, output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return -1;
                }

                return 0;
            });
        }

        private void GenerateFiles(string demosDirectory, string output)
        {
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), output);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var demosDirectoryInfo = new DirectoryInfo(demosDirectory);
            if (!demosDirectoryInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("{0} is not a directory", demosDirectory);
                return;
            }

            //Components,StylesAndAnimations
            foreach (DirectoryInfo componentsDirectory in demosDirectoryInfo.GetFileSystemInfos())
            {
                var componentsDirectoryName = componentsDirectory.Name.ToLower();

                var componentModels = new List<Dictionary<string, DemoComponentModel>>();
                var demoTypes = new List<string>();
                foreach (var fileSystemInfo in componentsDirectory.GetFileSystemInfos())
                {
                    if (fileSystemInfo is not DirectoryInfo componentDirectory) continue;

                    if (componentDirectory.GetFileSystemInfos("children")?.FirstOrDefault() is DirectoryInfo childrenDirectory && childrenDirectory.Exists)
                    {
                        foreach (var subComponentFileSystemInfo in childrenDirectory.GetFileSystemInfos().OrderBy(r => r.Name))
                        {
                            if (subComponentFileSystemInfo is not DirectoryInfo subComponentDirectory) continue;

                            var subDemoComponentModels = GetComponentModels(subComponentDirectory);
                            (var demoComponents, var types) = GetDemos(subComponentDirectory, demosDirectoryInfo);
                            demoTypes.AddRange(types);

                            foreach (var (language, demoItems) in demoComponents)
                            {
                                if (!subDemoComponentModels.ContainsKey(language)) continue;

                                subDemoComponentModels[language].DemoList.AddRange(demoItems);
                                subDemoComponentModels[language].LastWriteTime = subComponentDirectory.LastWriteTime;
                            }

                            componentModels.Add(subDemoComponentModels);
                        }
                    }
                    else
                    {
                        var demoComponentModels = GetComponentModels(componentDirectory);
                        (var demoComponents, var types) = GetDemos(componentDirectory, demosDirectoryInfo);
                        demoTypes.AddRange(types);

                        foreach (var (language, demoItems) in demoComponents)
                        {
                            if (!demoComponentModels.ContainsKey(language)) continue;

                            demoComponentModels[language].DemoList.AddRange(demoItems);
                            demoComponentModels[language].LastWriteTime = componentDirectory.LastWriteTime;
                        }

                        componentModels.Add(demoComponentModels);
                    }
                }

                var componentI18NModels = componentModels
                    .SelectMany(x => x).GroupBy(x => x.Key);
                foreach (var componentI18NModel in componentI18NModels)
                {
                    var demoComponentModels = componentI18NModel.Select(x => x.Value);
                    string componentJson = JsonSerializer.Serialize(demoComponentModels, new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });

                    //components.{lang}.json
                    var componentsOutputDirectory = Path.Combine(outputDirectory, componentsDirectoryName);
                    if (!Directory.Exists(componentsOutputDirectory))
                    {
                        Directory.CreateDirectory(componentsOutputDirectory);
                    }

                    var componentsJsonFilePath = Path.Combine(outputDirectory, componentsDirectoryName, $"components.{componentI18NModel.Key}.json");
                    File.WriteAllText(componentsJsonFilePath, componentJson);
                    Console.WriteLine("Generate demo file to {0}", componentsJsonFilePath);
                }

                //demoTypes.json
                var demoTypesOutputDirectory = Path.Combine(outputDirectory, componentsDirectoryName);
                if (!Directory.Exists(demoTypesOutputDirectory))
                {
                    Directory.CreateDirectory(demoTypesOutputDirectory);
                }
                var demoTypesFilePath = Path.Combine(outputDirectory, componentsDirectoryName, $"demoTypes.json");

                var demoTypesJson = JsonSerializer.Serialize(demoTypes, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                File.WriteAllText(demoTypesFilePath, demoTypesJson);
            }
        }

        private Dictionary<string, DemoComponentModel> GetComponentModels(DirectoryInfo componentDirectory)
        {
            Dictionary<string, DemoComponentModel> dict = new();

            FileSystemInfo docDir = componentDirectory.GetFileSystemInfos("doc")?.FirstOrDefault();

            if (docDir != null && docDir.Exists)
            {
                foreach (FileSystemInfo docItem in (docDir as DirectoryInfo).GetFileSystemInfos().OrderBy(r => r.Name))
                {
                    var language = docItem.Name.Replace("index.", "").Replace(docItem.Extension, "");

                    var content = File.ReadAllText(docItem.FullName);

                    var (matter, desc, otherDocs) = DocWrapper.ParseDemoDoc(content);

                    var model = new DemoComponentModel(matter, desc, otherDocs);

                    dict[language] = model;
                }
            }

            return dict;
        }

        private (Dictionary<string, List<DemoItemModel>>, List<string> demoTypes) GetDemos(DirectoryInfo componentDirectory,
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