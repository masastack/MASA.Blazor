using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Text.RegularExpressions;

namespace Masa.Blazor.Docs.ApiGenerator;

[Generator]
public class ComponentApiGenerator : IIncrementalGenerator
{
    private const string BlazorParameterAttributeName = "ParameterAttribute";
    private const string MassApiParameterAttributeName = "MasaApiParameterAttribute";
    private const string MasaApiPublicMethodAttributeName = "MasaApiPublicMethodAttribute";

    private static Dictionary<string, string> s_typeDescCache = new();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider
                              .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetTargetDataModelForGeneration)
                              .Where(u => u != null);

        context.RegisterSourceOutput(provider.Collect(), (ctx, componentMetas) =>
        {
            if (componentMetas.Length == 0)
            {
                return;
            }

            var namespaceName = componentMetas.First()!.NamespaceName + "ApiGenerator";

            var distinctComponentMetas = componentMetas.Distinct((a, b) => a!.Name == b!.Name);

            foreach (var componentMeta in distinctComponentMetas)
            {
                if (componentMeta is null)
                {
                    continue;
                }

                var sourceText = GenComponentMeta.GetSourceText(componentMeta);
                ctx.AddSource($"Masa.Blazor.Docs.{namespaceName}.{componentMeta.Name}.g.cs", sourceText);
            }

            var sb = new StringBuilder();
            sb.Append(@"namespace Masa.Blazor.Docs
{");
            sb.AppendLine($@"
    public static partial class {namespaceName}
    {{
        public static List<ComponentMeta> ComponentMetas {{ get; }} = new();");

            sb.Append($@"
         static {namespaceName}()
         {{
             ");

            foreach (var componentMeta in componentMetas)
            {
                if (componentMeta is null)
                {
                    continue;
                }

                sb.Append($@"
             ComponentMetas.Add(Gen{componentMeta.Name}());");
            }

            sb.Append($@"
        }}
    }}
}}");

            ctx.AddSource($"Masa.Blazor.Docs.{namespaceName}.g.cs", sb.ToString());
        });
    }

    private bool IsSyntaxTargetForGeneration(SyntaxNode node, CancellationToken token)
    {
        if (node is ClassDeclarationSyntax classDeclarationSyntax)
        {
            var className = classDeclarationSyntax.Identifier.ValueText;
            return Regex.IsMatch(className, "^[M|P]{1}[A-Z]{1}");
        }

        return false;
    }

    private ComponentMeta? GetTargetDataModelForGeneration(GeneratorSyntaxContext context, CancellationToken token)
    {
        var namespaceName = context.SemanticModel.Compilation.AssemblyName?.Split('.').Last();

        var classNode = (ClassDeclarationSyntax)context.Node;

        var semanticModel = context.SemanticModel.Compilation.GetSemanticModel(classNode.SyntaxTree);
        var declaredSymbol = semanticModel.GetDeclaredSymbol(classNode);
        if (declaredSymbol is null) return null;

        var componentName = declaredSymbol.Name;

        var defaultParameters = new List<ParameterInfo>();
        var contentParameters = new List<ParameterInfo>();
        var eventParameters = new List<ParameterInfo>();
        var publicMethods = new List<ParameterInfo>();
        var ignoreParameters = new List<string>();

        // TODO: 需要继承自 ComponentBase

        while (declaredSymbol is not null)
        {
            AnalyzeParameters(declaredSymbol, defaultParameters, contentParameters, eventParameters, publicMethods, ignoreParameters);

            // BaseType could be null if the blazor component only has a razor file but no cs file.
            // So need to add a empty cs file the blazor component.
            declaredSymbol = declaredSymbol.BaseType;
        }

        var parameters = new Dictionary<string, List<ParameterInfo>>()
        {
            { "props", defaultParameters },
            { "events", eventParameters },
            { "contents", contentParameters },
            { "methods", publicMethods },
        };

        return new ComponentMeta(componentName, parameters, namespaceName ?? componentName);
    }

    private static void AnalyzeParameters(INamedTypeSymbol classSymbol, List<ParameterInfo> defaultParameters, List<ParameterInfo> contentParameters,
        List<ParameterInfo> eventParameters, List<ParameterInfo> publicMethods, List<string> ignoreParameters)
    {
        var members = classSymbol.GetMembers();

        foreach (var member in members)
        {
            if (IsIgnoreProp(member.Name))
            {
                continue;
            }

            if (member is IPropertySymbol parameterSymbol)
            {
                var attrs = parameterSymbol.GetAttributes();
                
                var apiParameterAttribute = attrs.FirstOrDefault(attr => attr.AttributeClass?.Name == MassApiParameterAttributeName);
                if (apiParameterAttribute is not null)
                {
                    if (IsIgnoredParameter(apiParameterAttribute))
                    {
                        ignoreParameters.Add(parameterSymbol.Name);
                        continue;
                    }
                }
                
                if (attrs.Any(attr => attr.AttributeClass?.Name == BlazorParameterAttributeName))
                {
                    var type = parameterSymbol.Type as INamedTypeSymbol;
                    if (type is null || ignoreParameters.Contains(parameterSymbol.Name))
                    {
                        continue;
                    }

                    string? defaultValue = null;
                    string? releasedOn = null;

                    if (apiParameterAttribute is not null)
                    {
                        releasedOn = GetReleasedOnOnApiParameterAttribute(apiParameterAttribute);
                        defaultValue = GetDefaultValueOnApiParameterAttribute(apiParameterAttribute);
                    }

                    var typeText = GetTypeText(type);

                    if (!s_typeDescCache.TryGetValue(typeText, out var typeDesc))
                    {
                        typeDesc = GetTypeDesc(type);
                        if (!string.IsNullOrWhiteSpace(typeDesc))
                        {
                            s_typeDescCache.Add(typeText, typeDesc!);
                        }
                    }

                    var isObsolete = attrs.FirstOrDefault(attr => attr.AttributeClass?.Name == "ObsoleteAttribute") is not null;

                    var parameterInfo = new ParameterInfo(parameterSymbol.Name, typeText, typeDesc, defaultValue, isObsolete, false, releasedOn);

                    if (type.Name.StartsWith("RenderFragment"))
                    {
                        contentParameters.Add(parameterInfo);
                    }
                    else if (type.Name.StartsWith("EventCallback"))
                    {
                        eventParameters.Add(parameterInfo);
                    }
                    else
                    {
                        defaultParameters.Add(parameterInfo);
                    }
                }
            }
            else if (member is IMethodSymbol methodSymbol)
            {
                var attrs = methodSymbol.GetAttributes();
                if (attrs.Any(attr => attr.AttributeClass?.Name == MasaApiPublicMethodAttributeName))
                {
                    var args = methodSymbol.Parameters.Select(p => $"{GetTypeText(p.Type as INamedTypeSymbol)} {p.Name}");
                    var returnType = GetTypeText(methodSymbol.ReturnType as INamedTypeSymbol);

                    publicMethods.Add(new ParameterInfo(methodSymbol.Name, $"({string.Join(", ", args)}) => {returnType}"));
                }
            }
        }
    }

    private static string GetTypeText(INamedTypeSymbol? type)
    {
        if (type is null) return string.Empty;

        var typeArguments = type.TypeArguments.Select(t =>
        {
            if (t is INamedTypeSymbol namedType)
            {
                if (namedType.IsTupleType)
                {
                    var fields = namedType.TupleElements.Select(f =>
                    {
                        var fieldType = GetTypeText(f.Type as INamedTypeSymbol);
                        var fieldName = f.Name;
                        return $"{fieldType} {fieldName}";
                    });

                    return "(" + string.Join(", ", fields) + ")";
                }
                else if (namedType.IsGenericType)
                {
                    return GetTypeText(namedType);
                }
            }

            return Keyword(t.Name);
        }).ToList();

        if (typeArguments.Count == 0)
        {
            return Keyword(type.Name);
        }

        if (type.Name == "Nullable")
        {
            return $"{typeArguments.First()}?";
        }

        return $"{type.Name}<{string.Join(", ", typeArguments)}>";
    }

    private static string? GetTypeDesc(INamedTypeSymbol? type)
    {
        if (type is null) return null;

        var containingNamespace = type.ContainingNamespace.ToString();

        var isCustomType = containingNamespace is not null &&
                           (containingNamespace.StartsWith("BlazorComponent") || containingNamespace.StartsWith("Masa.Blazor"));

        if (type.TypeKind == TypeKind.Enum)
        {
            var excludeNames = new[] { "value__", ".ctor" };
            return "enum: " + string.Join(" | ", type.MemberNames.Except(excludeNames));
        }
        else if (type.Name != "String" && (type.IsReferenceType || type.TypeKind == TypeKind.Struct))
        {
            if (type.BaseType is not null && type.BaseType.Name == "OneOfBase")
            {
                var typeArguments = type.BaseType.TypeArguments.Select(t => Keyword(t.Name));
                return type.Name + " → " + $"OneOf<{string.Join(",", typeArguments)}>";
            }
            else if (type.IsGenericType)
            {
                string desc = string.Empty;

                if (isCustomType)
                {
                    var text = GetClassTypeText(type);
                    if (text is not null)
                    {
                        return type.Name + "\r\n" + text;
                    }
                }
                else
                {
                    var typeArguments = type.TypeArguments
                                            .Where(t => (t.TypeKind is TypeKind.Class or TypeKind.Struct or TypeKind.Enum) && type.Name != "String")
                                            .ToList();

                    foreach (var item in typeArguments)
                    {
                        var itemTypeDesc = GetTypeDesc(item as INamedTypeSymbol);
                        desc += itemTypeDesc;
                    }
                }

                return desc;
            }
            else if (isCustomType)
            {
                var text = GetClassTypeText(type);
                if (text is not null)
                {
                    return type.Name + "\r\n" + text;
                }
            }
        }

        return null;

        static string? GetClassTypeText(INamedTypeSymbol? type)
        {
            var properties = type.GetMembers().Where(m => m.Kind == SymbolKind.Property)
                                 .Select(m =>
                                 {
                                     var type = (m as IPropertySymbol).Type;
                                     if (type.TypeKind == TypeKind.TypeParameter)
                                     {
                                         return (m.Name, Type: type.Name);
                                     }

                                     return (m.Name, Type: GetTypeText(type as INamedTypeSymbol));
                                 }).ToList();

            if (properties.Count == 0)
            {
                return null;
            }

            return "{\r\n    " + string.Join("\r\n    ", properties.Select(p => $"{p.Type} {p.Name}")) + "\r\n}";
        }
    }

    private static string Keyword(string typeName)
    {
        if (typeName == typeof(void).Name)
        {
            return "void";
        }

        return typeName switch
        {
            nameof(String)  => "string",
            nameof(Boolean) => "bool",
            nameof(Double)  => "double",
            nameof(Int32)   => "int",
            nameof(Int64)   => "long",
            nameof(Object)  => "object",
            _               => typeName
        };
    }

    private static bool IsIgnoreProp(string name)
    {
        return new[] { "Attributes", "RefBack" }.Contains(name);
    }

    private static string? GetDefaultValueOnApiParameterAttribute(AttributeData apiParameterAttributeData)
    {
        var typedConstant = apiParameterAttributeData.NamedArguments.FirstOrDefault(u => u.Key == "DefaultValue").Value;

        if (typedConstant.IsNull)
        {
            typedConstant = apiParameterAttributeData.ConstructorArguments.FirstOrDefault();
        }

        if (typedConstant.IsNull)
        {
            return null;
        }

        if (typedConstant is { Kind: TypedConstantKind.Enum, Type: not null })
        {
            var str = typedConstant.Value?.ToString();

            if (int.TryParse(str, out var index))
            {
                return typedConstant.Type.GetMembers()[index].Name;
            }

            return str;
        }

        return typedConstant.Value?.ToString();
    }

    private static string? GetReleasedOnOnApiParameterAttribute(AttributeData apiParameterAttributeData)
    {
        var typedConstant = apiParameterAttributeData.NamedArguments.FirstOrDefault(u => u.Key == "ReleasedOn").Value;

        if (typedConstant.IsNull)
        {
            typedConstant = apiParameterAttributeData.ConstructorArguments.ElementAtOrDefault(1);
        }

        if (typedConstant.IsNull)
        {
            return null;
        }

        return typedConstant.Value?.ToString();
    }

    private static bool IsIgnoredParameter(AttributeData apiParameterAttributeData)
    {
        var ignoredArgument = apiParameterAttributeData.NamedArguments.FirstOrDefault(n => n.Key == "Ignored");
        return ignoredArgument.Key is not null && ignoredArgument.Value.Value is true;
    }
}

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer) where T : notnull
        => source.Distinct(new DynamicEqualityComparer<T>(comparer));

    private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T> where T : notnull
    {
        private readonly Func<T, T, bool> _func;

        public DynamicEqualityComparer(Func<T, T, bool> func)
        {
            _func = func;
        }

        public bool Equals(T x, T y) => _func(x, y);

        public int GetHashCode(T obj) => 0;
    }
}