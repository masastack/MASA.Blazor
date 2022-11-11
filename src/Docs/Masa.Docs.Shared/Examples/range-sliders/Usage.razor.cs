using Masa.Blazor;

namespace Masa.Docs.Shared.Examples.range_sliders;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MRangeSlider<double>.Dense), false },
        { nameof(MRangeSlider<double>.Vertical), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MRangeSlider<double>.Disabled), new CheckboxParameter("false",true) },
        { nameof(MRangeSlider<double>.HideDetails), new CheckboxParameter("false",true) },
        { nameof(MRangeSlider<double>.InverseLabel), new CheckboxParameter("false",true) },
        { nameof(MRangeSlider<double>.PersistentHint), new CheckboxParameter("false",true) },
        { nameof(MRangeSlider<double>.Readonly), new CheckboxParameter("false",true) },
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
            nameof(MRangeSlider<double>.HideDetails) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MRangeSlider<double>.Hint), "Im a hint" },
            { nameof(MRangeSlider<double>.Label), "Range Slider" },
            { nameof(MRangeSlider<double>.Value), new List<double>(){-25,25 } }
        };
    }
}