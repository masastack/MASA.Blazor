namespace Masa.Blazor.Docs.Examples.components.progress_linear;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MProgressLinear))
    {
    }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MProgressLinear.Indeterminate), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MProgressLinear.Height), new SliderParameter(4, 0, 12) }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MProgressLinear.Color), new SelectParameter(new List<string>() { "primary", "purple", "error","lime" }) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MProgressLinear.Height) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MProgressLinear.Value), (double)15 },
        };
    }
}
