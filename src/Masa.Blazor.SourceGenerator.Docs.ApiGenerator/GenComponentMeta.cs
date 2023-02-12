﻿using System.Text;

namespace Masa.Blazor.SourceGenerator.Docs.ApiGenerator;

public static class GenComponentMeta
{
    internal static string GetSourceText(ComponentMeta componentMeta)
    {
        var sb = new StringBuilder(@"
namespace Masa.Blazor.Docs
{
    public static partial class ApiGenerator
    {
");
        sb.Append($@"
        private static ComponentMeta Gen{componentMeta.Name}()
        {{");
        sb.AppendLine($@"
            var dict = new Dictionary<string, List<ParameterInfo>>();");
        foreach (var parameter in componentMeta.Parameters)
        {
            var valueName = $"{parameter.Key}Value";
            sb.AppendLine($@"
            var {valueName} = new List<ParameterInfo>();");

            foreach (var item in parameter.Value)
            {
                sb.AppendLine($@"
            {valueName}.Add(new ParameterInfo
            {{
                Name = ""{item.Name}"",
                Type = ""{item.Type}"",
                TypeDesc = @""{item.TypeDesc}"",
                DefaultValue = ""{item.DefaultValue}"",
                Required = {item.Required.ToString().ToLower()}
            }});");
            }

            sb.AppendLine($@"
            dict.Add(""{parameter.Key}"", {valueName});");
        }

        sb.AppendLine($@"
            return new ComponentMeta(""{componentMeta.Name}"", dict);
        }}
    }}
}}");

        return sb.ToString();
    }
}
