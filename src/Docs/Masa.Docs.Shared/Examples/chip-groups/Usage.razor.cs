namespace Masa.Docs.Shared.Examples.chip_groups;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MChipGroup))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MChipGroup.Column), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MChipGroup.CenterActive), new CheckboxParameter("false", true) },
        { nameof(MChipGroup.Mandatory), new CheckboxParameter("false", true) },
        { nameof(MChipGroup.Multiple), new CheckboxParameter("false", true) },
        { nameof(MChipGroup.ShowArrows), new CheckboxParameter("false", true) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MChipGroup.ShowArrows) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MChip>(0);
        builder.AddChildContent(1, "Chip1");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(2);
        builder.AddChildContent(3, "Chip2");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(4);
        builder.AddChildContent(5, "Chip3");
        builder.CloseComponent();
    };
}
