namespace Masa.Blazor.SourceGenerator.Docs.ApiGenerator;

internal class ParameterInfo
{
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? DefaultValue { get; set; }

    public string? Description { get; set; }

    public bool Required { get; set; }
}