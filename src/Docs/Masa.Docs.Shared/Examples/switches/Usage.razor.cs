using Masa.Blazor;

namespace Masa.Docs.Shared.Examples.switches;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSwitch.Inset), false },
        { nameof(MSwitch.Dense), false },
        { nameof(MSwitch.Flat), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSwitch.Disabled), new CheckboxParameter("false",true) },
        { nameof(MSwitch.Loading), new CheckboxParameter("false",true) },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MSwitch.Color), new SelectParameter(new List<string>() { "red", "indigo", "orange","primary","success","warning" }) },
    };


    public Usage() : base(typeof(MSwitch))
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
            nameof(MSlider<double>.Loading) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MSwitch.Label), "Switch" },
        };
    }
}