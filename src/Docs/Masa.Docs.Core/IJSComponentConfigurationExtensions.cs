using System.Reflection;
using System.Text.RegularExpressions;

namespace Masa.Docs.Core;

// ReSharper disable once InconsistentNaming
public static class IJSComponentConfigurationExtensions
{
    public static void RegisterCustomElementsUsedJSCustomElementAttribute(this IJSComponentConfiguration componentConfiguration)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(u => u.FullName!.Contains("Masa")))
        {
            var hasJsCustomElementTypes = assembly.GetTypes().Where(type =>
                typeof(ComponentBase).IsAssignableFrom(type) &&
                type.CustomAttributes.Any(attr => attr.AttributeType == typeof(JSCustomElementAttribute)));
            foreach (var type in hasJsCustomElementTypes)
            {
                var attr = (JSCustomElementAttribute)type.GetCustomAttributes(typeof(JSCustomElementAttribute)).First();
                var name = attr.Name;
                var includeNamespace = attr.IncludeNamespace;

                if (string.IsNullOrWhiteSpace(name))
                {
                    if (includeNamespace)
                    {
                        var names = type.FullName!.Split(".").TakeLast(2);
                        name = ToKebab(string.Join("-", names));
                        name = name.Replace('_', '-');
                    }
                    else
                    {
                        name = ToKebab(type.Name);
                    }
                }

                componentConfiguration.RegisterForJavaScript(type, name, "registerBlazorCustomElement");
            }
        }
    }

    private static string ToKebab(string name)
    {
        var split = Regex.Split(name, @"(?<!^)(?=[A-Z])").Select(s => s.Trim('-'));
        return string.Join("-", split).ToLowerInvariant();
    }
}
