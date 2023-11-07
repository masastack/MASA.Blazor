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

    public string Name { get; set; }

    public string Type { get; set; }

    public string? TypeDesc { get; set; }

    public string? DefaultValue { get; set; }

    public bool Required { get; set; }

    public bool IsObsolete { get; set; }

    public string? ReleasedOn { get; set; }
}
