using System.Reflection;

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
                componentConfiguration.RegisterCustomElements(attr, type);
            }

            var inheritJsCustomElementTypes = assembly.GetTypes().Where(type =>
                typeof(ComponentBase).IsAssignableFrom(type) && type.BaseType is not null &&
                type.BaseType.CustomAttributes.Any(attr => attr.AttributeType == typeof(JSCustomElementAttribute)));

            foreach (var type in inheritJsCustomElementTypes)
            {
                var attr = (JSCustomElementAttribute)type.BaseType!.GetCustomAttributes(typeof(JSCustomElementAttribute)).First();
                componentConfiguration.RegisterCustomElements(attr, type);
            }
        }
    }

    private static void RegisterCustomElements(this IJSComponentConfiguration componentConfiguration, JSCustomElementAttribute attr, Type type)
    {
        var name = attr.Name;
        var includeNamespace = attr.IncludeNamespace;

        if (string.IsNullOrWhiteSpace(name))
        {
            if (includeNamespace)
            {
                var names = type.FullName!.Split(".").TakeLast(2);
                name = string.Join("-", names).ToKebab();
                name = name.Replace('_', '-');
            }
            else
            {
                name = type.Name.ToKebab();
            }
        }

        componentConfiguration.RegisterForJavaScript(type, name, "registerBlazorCustomElement");
    }

}
