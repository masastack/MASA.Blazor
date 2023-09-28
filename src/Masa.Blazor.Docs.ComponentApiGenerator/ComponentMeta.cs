namespace Masa.Blazor.Docs.ApiGenerator;

public class ComponentMeta
{
    public ComponentMeta(string name, Dictionary<string, List<ParameterInfo>> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    public ComponentMeta(string name, Dictionary<string, List<ParameterInfo>> parameters, string namespaceName):this(name, parameters)
    {
        NamespaceName = namespaceName;
    }

    public string Name { get; private set; }

    public Dictionary<string, List<ParameterInfo>> Parameters { get; private set; }
    
    public string? NamespaceName { get; set; }
}