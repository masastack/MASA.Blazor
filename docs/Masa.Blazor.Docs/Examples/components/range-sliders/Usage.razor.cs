namespace Masa.Blazor.Docs.Examples.components.range_sliders;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    private IList<double> _value = new List<double>() { -25, 25 };

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MRangeSlider<double>.Dense), false },
        { nameof(MRangeSlider<double>.Vertical), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MRangeSlider<double>.Disabled), new CheckboxParameter() },
        { nameof(MRangeSlider<double>.HideDetails), new CheckboxParameter("true", false) },
        { nameof(MRangeSlider<double>.InverseLabel), new CheckboxParameter() },
        { nameof(MRangeSlider<double>.PersistentHint), new CheckboxParameter() },
        { nameof(MRangeSlider<double>.Readonly), new CheckboxParameter() },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MRangeSlider<double>.Min), new SliderParameter(-50, -100, 100, false) },
        { nameof(MRangeSlider<double>.Max), new SliderParameter(50, -100, 100, false) }
    };

    public Usage() : base(typeof(MRangeSlider<double>))
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
            nameof(MRangeSlider<double>.HideDetails) => (StringBoolean)(parameter.Value.ToString() == "true"),
            _ => parameter.Value
        };
    }

    protected override IEnumerable<string> AdditionalParameters => new[] { "Hint=\"Im a hint\"" };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MRangeSlider<double>.Hint), "Im a hint" },
            { nameof(MRangeSlider<double>.Label), "Range Slider" },
            { nameof(MRangeSlider<double>.Value), _value },
            { nameof(MRangeSlider<double>.ValueChanged), EventCallback.Factory.Create<IList<double>>(this, val => _value = val) }
        };
    }
}
