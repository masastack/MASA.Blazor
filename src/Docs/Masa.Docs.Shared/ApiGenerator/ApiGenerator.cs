using System.Reflection;
using System.Text.RegularExpressions;
using BlazorComponent.Attributes;
using OneOf;

namespace Masa.Docs.Shared.ApiGenerator;

public static class ApiGenerator
{
    public static Dictionary<string, Dictionary<string, List<ParameterInfo>>> parametersCache = new();

    public static void Run()
    {
        var assembly = typeof(MApp).Assembly;
        var componentBaseType = typeof(ComponentBase);
        var componentTypes = assembly.GetTypes().Where(type => componentBaseType.IsAssignableFrom(type) && Regex.IsMatch(type.Name, "^[A-Z]{2}"));

        foreach (var type in componentTypes)
        {
            var parameterProps = type.GetProperties()
                                     .Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(ParameterAttribute)));
            var contentProps = parameterProps.Where(prop => prop.PropertyType == typeof(RenderFragment) || (prop.PropertyType.IsGenericType &&
                prop.PropertyType == typeof(RenderFragment<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
            var eventProps = parameterProps.Where(prop => prop.PropertyType == typeof(EventCallback) || (prop.PropertyType.IsGenericType &&
                prop.PropertyType == typeof(EventCallback<>).MakeGenericType(prop.PropertyType.GenericTypeArguments[0])));
            var defaultProps = parameterProps.Where(prop =>
                contentProps.Any(renderFragmentProp => renderFragmentProp == prop) is false &&
                eventProps.Any(eventProps => eventProps == prop) is false);

            var typeName = GetTypeName(type, ignoreGenericTypeArguments: true);

            if (parametersCache.ContainsKey(typeName))
            {
                continue;
            }

            var parameters = defaultProps.Where(prop => !IsIgnoreProp(prop.Name)).Select(MapToParameterInfo).ToList();
            var contentParameters = contentProps.Select(MapToParameterInfo).ToList();
            var eventParameters = eventProps.Select(MapToParameterInfo).ToList();

            var value = new Dictionary<string, List<ParameterInfo>>()
            {
                { "props", parameters },
                { "events", eventParameters },
                { "contents", contentParameters },
            };

            parametersCache.Add(typeName, value);
        }
    }

    static ParameterInfo MapToParameterInfo(PropertyInfo propertyInfo)
    {
        var instance = new ParameterInfo()
        {
            Name = propertyInfo.Name,
            Type = GetTypeName(propertyInfo.PropertyType),
        };

        var editorRequiredAttribute = propertyInfo.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(EditorRequiredAttribute));
        instance.Required = editorRequiredAttribute is not null;

        var defaultValueAttribute = propertyInfo.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(DefaultValue));
        instance.DefaultValue = defaultValueAttribute is not null
            ? GetValueOfDefaultValueAttribute(defaultValueAttribute)
            : GetDefaultValue(propertyInfo.PropertyType);

        return instance;
    }

    static string GetTypeName(Type type, bool ignoreGenericTypeArguments = false)
    {
        if (type.IsGenericType)
        {
            var name = type.Name.Remove(type.Name.IndexOf('`'));
            if (ignoreGenericTypeArguments)
            {
                return name;
            }

            var genericTypeNames = type.GenericTypeArguments.Select(t => Keyword(t.Name));
            return $"{name}<{string.Join(", ", genericTypeNames)}>";
        }

        if (type.IsAssignableTo(typeof(IOneOf)))
        {
            var genericTypeArguments = string.Join(" | ", type.BaseType.GenericTypeArguments.Select(t => Keyword(t.Name)));
            return $"{type.Name} [{genericTypeArguments}]";
        }

        if (type.IsEnum)
        {
            var names = string.Join(" | ", Enum.GetNames(type));
            return $"{type.Name}.[{names}]";
        }

        return Keyword(type.Name);
    }

    static string? GetValueOfDefaultValueAttribute(CustomAttributeData data)
    {
        var argument = data.ConstructorArguments.First();
        if (argument.ArgumentType == typeof(string))
        {
            return $"\"{argument.Value}\"";
        }

        return argument.Value?.ToString();
    }

    static string? GetDefaultValue(Type type)
    {
        if (type.IsValueType && type.IsPrimitive)
        {
            return Activator.CreateInstance(type)?.ToString()?.ToLower() ?? null;
        }

        if (type.IsEnum)
        {
            return Enum.GetNames(type).FirstOrDefault();
        }

        if (type.IsAssignableTo(typeof(IOneOf)))
        {
            return null;
        }

        return null; // TODO: check here
    }

    static bool IsIgnoreProp(string name)
    {
        return new[] { "Attributes", "RefBack" }.Contains(name);
    }

    static string Keyword(string typeName)
    {
        return typeName switch
        {
            nameof(String) => "string",
            nameof(Boolean) => "bool",
            nameof(Double) => "double",
            nameof(Int32) => "int",
            nameof(Int64) => "long",
            _ => typeName
        };
    }
}

public class ParameterInfo
{
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? DefaultValue { get; set; }

    public string? Description { get; set; }

    public bool Required { get; set; }
}
