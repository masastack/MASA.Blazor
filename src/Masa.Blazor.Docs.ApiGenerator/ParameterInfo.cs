namespace Masa.Blazor.Docs.ApiGenerator;

public class ParameterInfo
{
    public ParameterInfo(
        string name,
        string type,
        string? typeDesc = null,
        string? defaultValue = null,
        bool isObsolete = false,
        bool required = false,
        string? releasedOn = null)
    {
        Name = name;
        Type = type;
        TypeDesc = typeDesc;
        DefaultValue = defaultValue;
        IsObsolete = isObsolete;
        Required = required;
        ReleasedOn = releasedOn;
    }

    public string Name { get; }

    public string Type { get; }

    public string? TypeDesc { get; }

    public string? DefaultValue { get; }

    public bool Required { get; }

    public bool IsObsolete { get; }

    public string? ReleasedOn { get; }
}
