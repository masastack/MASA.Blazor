using MASA.Blazor.Doc.CLI.Comparers;
using MASA.Blazor.Doc.CLI.Interfaces;
using MASA.Blazor.Doc.CLI.Wrappers;
using MASA.Blazor.Doc.Models.Extensions;
using MASA.Blazor.Doc.Models;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using MASA.Blazor;

namespace MASA.Blazor.Doc.CLI.Commands
{
    public class GenerateApiJsonCommand : IAppCommand
    {
        public string Name => "api2json";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate json file for api";
            command.HelpOption();

            var assemblyDirArgument = command.Argument(
                "assembly Path", "[Required] The Path of assembly file.");

            var outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");        
          
            command.OnExecute(() =>
            {
                string assemblyPath = assemblyDirArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(assemblyPath) || !File.Exists(assemblyPath))
                {
                    Console.WriteLine("Invalid assemblyPath.");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }
                GenerateApiFiles(output,new[] { "zh-CN", "en-US" });

                return 0;
            });         
        }

        void GenerateApiFiles(string output,string[] languages)
        {
            var assembly = typeof(MApp).Assembly;//Assembly.LoadFile(assemblyPath);
            var ComponentBaseType = typeof(ComponentBase);
            var componentTypes = assembly.GetTypes().Where(type => ComponentBaseType.IsAssignableFrom(type) && type.Name.StartsWith("M"));
            var apis = new List<Api>();

            foreach (var componentType in componentTypes)
            {
                var paramterProps = componentType.GetProperties().Where(prop => prop.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ParameterAttribute)));
                var contentProps = paramterProps.Where(prop => prop.PropertyType == typeof(RenderFragment) || (prop.PropertyType.IsGenericType && prop.PropertyType == typeof(RenderFragment<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
                var eventProps = paramterProps.Where(prop => prop.PropertyType == typeof(EventCallback) || (prop.PropertyType.IsGenericType && prop.PropertyType == typeof(EventCallback<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
                var defaultProps = paramterProps.Where(props => contentProps.Any(cprops => cprops == props) is false && eventProps.Any(eprops => eprops == props) is false);

                var api = new Api
                {
                    Components = new[] { GetComponentName(componentType) },
                    Props = defaultProps.Where(prop => IgnorePrpps(prop.Name)).Select(prop => new Prop
                    {
                        Name = prop.Name,
                        Type = prop.PropertyType.Name,
                        Default = GetDefaultValue(prop.PropertyType),
                        Description = "",
                    }).OrderBy(p => p.Name).ToArray(),
                    Contents = contentProps.Select(prop => new Content
                    {
                        Name = prop.Name,
                        Description = ""
                    }).OrderBy(content => content.Name).ToArray(),
                    Events = eventProps.Select(prop => new Event
                    {
                        Name = prop.Name,
                        Description = ""
                    }).OrderBy(e => e.Name).ToArray(),
                };

                apis.Add(api);
            }

            foreach (var language in languages)
            {
                var files = Directory.GetFiles(output).Where(file => file.EndsWith($".{language}.json"));
                var jsonOption = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var basepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var propDescriptionMap = JsonSerializer.Deserialize<string[][]>(File.ReadAllText($"{basepath}/ApiSettings/propDefaultDescription.{language}.json"))
                                                        .ToDictionary(prop => prop[0], prop => prop[1]);

                var contentDescriptionMap = JsonSerializer.Deserialize<string[][]>(File.ReadAllText($"{basepath}/ApiSettings/contentDefaultDescription.{language}.json"))
                                                        .ToDictionary(prop => prop[0], prop => prop[1]);

                foreach (var api in apis)
                {
                    var file = files.FirstOrDefault(f => f.Contains($"M{api.Components[0]}.{language}.json"));
                    if (file is not null)
                    {
                        var oldApi = JsonSerializer.Deserialize<Api>(File.ReadAllText(file));
                        foreach (var prop in api.Props)
                        {
                            var oldProp = oldApi.Props.FirstOrDefault(p => p.Name == prop.Name);
                            if (oldProp is not null)
                            {
                                prop.Description = oldProp.Description;
                                prop.Default = oldProp.Default;
                            }
                        }
                    }
                    else file = $"{output}/M{api.Components[0]}.{language}.json";

                    foreach (var prop in api.Props)
                    {
                        if(propDescriptionMap.ContainsKey(prop.Name))
                        {
                            prop.Description = propDescriptionMap[prop.Name];
                        }
                    }
                    foreach (var content in api.Contents)
                    {
                        if(contentDescriptionMap.ContainsKey(content.Name))
                        {
                            content.Description = contentDescriptionMap[content.Name];
                        }
                    }

                    File.WriteAllText(file, JsonSerializer.Serialize(api, jsonOption), Encoding.UTF8);

                    //md
                    var mdFile = $"{output}/M{api.Components[0]}.{language}.md";
                    var mdContent = JsonSerializer.Deserialize<List<string>>(File.ReadAllText($"{basepath}/ApiSettings/mdContent.{language}.json"));
                    mdContent[mdContent.IndexOf("[title]")] = $"title: {api.Components[0]}";
                    File.WriteAllLines(mdFile, mdContent);
                }
            }
        }

        string GetComponentName(Type componentType)
        {
            if (componentType.IsGenericType) return componentType.Name.Remove(componentType.Name.IndexOf('`')).Remove(0, 1);
            else return componentType.Name.Remove(0, 1);
        }

        bool IgnorePrpps(string name)
        {
            return name != "Attributes" && name != "RefBack";
        }

        string GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                if (type == typeof(bool)) return "false";
                if (type == typeof(int)) return "0";
                else return "";
            }
            else return "null";
        }
    }

    public class Api
    {
        public string[] Components { get; set; }

        public Prop[] Props { get; set; }

        public Content[] Contents { get; set; }

        public Event[] Events { get; set; }
    }

    public class Prop
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Default { get; set; }

        public string Description { get; set; }
    }

    public class Content
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


