namespace Masa.Blazor.Docs.Examples.components.buttons;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MButton))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MButton.Depressed), false },
        { nameof(MButton.Icon), false },
        { nameof(MButton.Outlined), false },
        { nameof(MButton.Plain), false },
        { nameof(MButton.Fab), false },
        { nameof(MButton.Text), false },
        { nameof(MButton.Tile), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MButton.Block), new CheckboxParameter("false", true) },
        { nameof(MButton.Disabled), new CheckboxParameter("false", true) },
        { nameof(MButton.Large), new CheckboxParameter("false", true) },
        { nameof(MButton.Loading), new CheckboxParameter("false", true) },
        { nameof(MButton.Small), new CheckboxParameter("false", true) },
        { nameof(MButton.XLarge), new CheckboxParameter("false", true) },
        { nameof(MButton.XSmall), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MButton.Elevation), new SliderParameter(2, 0, 24) }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MButton.Color), new SelectParameter(new List<string>() { "accent", "primary", "secondary" }) },
    };

    protected override RenderFragment GenChildContent() => builder => builder.AddContent(0, "Click Me");

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MButton.Elevation) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }
}
