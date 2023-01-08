namespace Masa.Blazor.Docs.Examples.components.switches;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSwitch<bool>.Inset), false },
        { nameof(MSwitch<bool>.Dense), false },
        { nameof(MSwitch<bool>.Flat), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSwitch<bool>.Disabled), new CheckboxParameter("false",true) },
        { nameof(MSwitch<bool>.Loading), new CheckboxParameter("false",true) },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MSwitch<bool>.Color), new SelectParameter(new List<string>() { "red", "indigo", "orange","primary","success","warning" }) },
    };


    public Usage() : base(typeof(MSwitch<bool>))
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
            { nameof(MSwitch<bool>.Label), "Switch" },
        };
    }
}