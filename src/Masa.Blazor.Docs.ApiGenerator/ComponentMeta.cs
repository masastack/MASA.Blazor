namespace Masa.Blazor.Docs.ApiGenerator;

internal class ComponentMeta
{
    public ComponentMeta(string name, Dictionary<string, List<ParameterInfo>> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    public string Name { get; private set; }

    public Dictionary<string, List<ParameterInfo>> Parameters { get; private set; }
}