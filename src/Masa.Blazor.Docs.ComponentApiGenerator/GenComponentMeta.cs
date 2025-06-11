using System.Text;

namespace Masa.Blazor.Docs.ApiGenerator;

public static class GenComponentMeta
{
    public static string GetSourceText(ComponentMeta componentMeta)
    {
        var sb = new StringBuilder($@"
namespace Masa.Blazor.Docs
{{
    public static partial class {componentMeta.NamespaceName}ApiGenerator
    {{
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
                Required = {item.Required.ToString().ToLower()},
                IsObsolete = {item.IsObsolete.ToString().ToLower()},
                ReleasedOn = ""{item.ReleasedOn}"",
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
