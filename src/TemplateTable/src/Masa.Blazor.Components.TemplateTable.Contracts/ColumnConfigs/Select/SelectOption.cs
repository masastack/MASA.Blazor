namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class SelectOption
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string? Label { get; set; }

    public string? Value { get; set; }

    public string? Color { get; set; }

    public SelectOption()
    {
    }

    public SelectOption(string label, string value)
    {
        Label = label;
        Value = value;
    }

    public SelectOption(string label, string value, string color) : this(label, value)
    {
        Color = color;
    }
}