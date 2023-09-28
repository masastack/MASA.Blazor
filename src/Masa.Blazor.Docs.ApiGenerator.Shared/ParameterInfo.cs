namespace Masa.Blazor.Docs.ApiGenerator.Shared;

public class ParameterInfo
{
    public ParameterInfo(string name, string type, string? typeDesc = null, string? defaultValue = null, bool isObsolete = false)
    {
        Name = name;
        Type = type;
        TypeDesc = typeDesc;
        DefaultValue = defaultValue;
        IsObsolete = isObsolete;
    }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? TypeDesc { get; set; }

    public string? DefaultValue { get; set; }

    public string? Description { get; set; }

    public bool Required { get; set; }

    public bool IsObsolete { get; set; }
}
