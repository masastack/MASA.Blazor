namespace Masa.Blazor.Docs.Examples.components.chip_groups;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    private StringNumber _value;

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

    protected override Dictionary<string, object>? GenAdditionalParameters() => new()
    {
        { nameof(MChipGroup.ActiveClass), "primary--text" },
        { nameof(MChipGroup.Value), _value },
        { nameof(MChipGroup.ValueChanged), EventCallback.Factory.Create<StringNumber>(this, val => _value = val) }
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

        builder.OpenComponent<MChip>(6);
        builder.AddChildContent(7, "Chip4");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(8);
        builder.AddChildContent(9, "Chip5");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(10);
        builder.AddChildContent(11, "Chip6");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(12);
        builder.AddChildContent(13, "Chip7");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(14);
        builder.AddChildContent(15, "Chip8");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(16);
        builder.AddChildContent(17, "Chip9");
        builder.CloseComponent();

        builder.OpenComponent<MChip>(18);
        builder.AddChildContent(19, "Chip10");
        builder.CloseComponent();
    };
}
