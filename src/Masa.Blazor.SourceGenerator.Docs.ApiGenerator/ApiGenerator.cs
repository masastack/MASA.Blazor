using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Masa.Blazor.SourceGenerator.Docs.ApiGenerator;

[Generator]
public class ApiGenerator : IIncrementalGenerator
{
    private const string BlazorParameterAttributeName = "ParameterAttribute";
    private const string DefaultValueAttributeName = "DefaultValueAttribute";
    private const string PublicMethodAttributeName = "PublicMethodAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            //Debugger.Launch();
        }
#endif

        var provider = context.SyntaxProvider
                              .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetTargetDataModelForGeneration)
                              .Where(u => u != null);

        context.RegisterSourceOutput(provider.Collect(), (ctx, componentMetas) =>
        {
            var sb = new StringBuilder();
            sb.Append(@"namespace Masa.Blazor.Docs
{");
            sb.AppendLine($@"
    public static partial class ApiGenerator
    {{
        public static List<ComponentMeta> ComponentMetas {{ get; private set; }}");

            sb.Append($@"
        static ApiGenerator()
        {{
            ComponentMetas = new List<ComponentMeta>();");

            foreach (var componentMeta in componentMetas)
            {
                sb.Append($@"
            ComponentMetas.Add(Gen{componentMeta.Name}());");
            }

            sb.Append($@"
        }}
    }}

    public class ComponentMeta
    {{
        public ComponentMeta(string name, Dictionary<string, List<ParameterInfo>> parameters)
        {{
            Name = name;
            Parameters = parameters;
        }}

        public string Name {{ get; private set; }}

        public Dictionary<string, List<ParameterInfo>> Parameters {{ get; private set; }}
    }}

    public class ParameterInfo
    {{
        public string Name {{ get; set; }} = null!;

        public string Type {{ get; set; }} = null!;

        public string? DefaultValue {{ get; set; }}

        public string? Description {{ get; set; }}

        public bool Required {{ get; set; }}
    }}
}}");

            ctx.AddSource("Masa.Blazor.SourceGenerator.Docs.ApiGenerator.g.cs", sb.ToString());

            foreach (var componentMeta in componentMetas)
            {
                var sourceText = GenComponentMeta.GetSourceText(componentMeta);
                ctx.AddSource($"Masa.Blazor.SourceGenerator.Docs.ApiGenerator.{componentMeta.Name}.g.cs", sourceText);
            }
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

    private ComponentMeta GetTargetDataModelForGeneration(GeneratorSyntaxContext context, CancellationToken token)
    {
        var classNode = (ClassDeclarationSyntax)context.Node;

        var semanticModel = context.SemanticModel.Compilation.GetSemanticModel(classNode.SyntaxTree);
        var declaredSymbol = semanticModel.GetDeclaredSymbol(classNode);
        var componentName = declaredSymbol.Name;

        var defaultParameters = new List<ParameterInfo>();
        var contentParameters = new List<ParameterInfo>();
        var eventParameters = new List<ParameterInfo>();
        var publicMethods = new List<ParameterInfo>();

        // TODO: 需要继承自 ComponentBase

        while (declaredSymbol is not null)
        {
            AnalyzeParameters(declaredSymbol, defaultParameters, contentParameters, eventParameters, publicMethods);
            declaredSymbol = declaredSymbol.BaseType;
        }

        var parameters = new Dictionary<string, List<ParameterInfo>>()
        {
            { "props", defaultParameters },
            { "events", eventParameters },
            { "contents", contentParameters },
            { "methods", publicMethods },
        };

        return new ComponentMeta(componentName, parameters);
    }

    private static void AnalyzeParameters(INamedTypeSymbol classSymbol, List<ParameterInfo> defaultParameters, List<ParameterInfo> contentParameters,
        List<ParameterInfo> eventParameters, List<ParameterInfo> publicMethods)
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
                if (attrs.Any(attr => attr.AttributeClass.Name == BlazorParameterAttributeName))
                {
                    var type = parameterSymbol.Type as INamedTypeSymbol;
                    if (type is null)
                    {
                        continue;
                    }

                    string? defaultValue = null;

                    var defaultValueAttribute = attrs.FirstOrDefault(attr => attr.AttributeClass.Name == DefaultValueAttributeName);
                    if (defaultValueAttribute is not null)
                    {
                        defaultValue = defaultValueAttribute.ConstructorArguments.First().Value.ToString();
                    }

                    var typeText = GetTypeText(type);

                    var parameterInfo = new ParameterInfo { Name = parameterSymbol.Name, Type = typeText, DefaultValue = defaultValue };

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
                if (attrs.Any(attr => attr.AttributeClass.Name == PublicMethodAttributeName))
                {
                    var args = methodSymbol.Parameters.Select(p => $"{GetTypeText(p.Type as INamedTypeSymbol)} {p.Name}");
                    var returnType = GetTypeText(methodSymbol.ReturnType as INamedTypeSymbol);

                    publicMethods.Add(new ParameterInfo { Name = methodSymbol.Name, Type = $"({string.Join(", ", args)}) => {returnType}" });
                }
            }
        }
    }

    private static string GetTypeText(INamedTypeSymbol? type)
    {
        var typeArguments = type.TypeArguments.Select(t => Keyword(t.Name));
        return typeArguments.Any() ? $"{type.Name}<{string.Join(", ", typeArguments)}>" : Keyword(type.Name);
    }

    private static string Keyword(string typeName)
    {
        if (typeName == typeof(void).Name)
        {
            return "void";
        }

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

    private static bool IsIgnoreProp(string name)
    {
        return new[] { "Attributes", "RefBack" }.Contains(name);
    }
}
