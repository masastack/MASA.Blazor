namespace Masa.Blazor.Docs.Examples.components.sliders;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSlider<double>.Dense), false },
        { nameof(MSlider<double>.Vertical), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSlider<double>.Disabled), new CheckboxParameter("false",true) },
        { nameof(MSlider<double>.HideDetails), new CheckboxParameter("false",true) },
        { nameof(MSlider<double>.InverseLabel), new CheckboxParameter("false",true) },
        { nameof(MSlider<double>.PersistentHint), new CheckboxParameter("false",true) },
        { nameof(MSlider<double>.Readonly), new CheckboxParameter("false",true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MSlider<double>.Min), new SliderParameter(-50, -100, 100, false) },
        { nameof(MSlider<double>.Max), new SliderParameter(50, -100, 100, false) }
    };

    public Usage() : base(typeof(MSlider<double>))
    {
    }

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MSlider<double>.HideDetails) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MSlider<double>.Hint), "Im a hint" },
            { nameof(MSlider<double>.Label), "Slider" },
        };
    }
}