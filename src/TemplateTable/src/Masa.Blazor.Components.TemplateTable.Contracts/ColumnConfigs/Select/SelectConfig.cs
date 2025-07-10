namespace Masa.Blazor.Components.TemplateTable.Contracts;

public class SelectConfig
{
    public bool Color { get; set; }
    
    public List<SelectOption> Options { get; set; } = [];
}