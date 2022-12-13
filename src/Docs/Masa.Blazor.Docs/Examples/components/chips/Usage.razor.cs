namespace Masa.Blazor.Docs.Examples.components.chips;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MChip))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MChip.Filter), false },
        { nameof(MChip.Label), false },
        { nameof(MChip.Link), false },
        { nameof(MChip.Outlined), false },
        { nameof(MChip.Pill), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MChip.Close), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MChip.CloseIcon), new SelectParameter(new List<string>() { "mdi-close-outline", "mdi-delete" }) },
        { nameof(MChip.Color), new SelectParameter(new List<string>() { "red", "orange", "yellow", "green", "blue", "purple" }) },
    };

    protected override RenderFragment GenChildContent() => builder => builder.AddContent(0, "Chip Component");
}
