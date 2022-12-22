namespace Masa.Blazor.Docs.Examples.components.progress_circular;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MProgressCircular))
    {
    }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MProgressCircular.Indeterminate), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MProgressCircular.Size), new SliderParameter(40, 0, 128) },
        { nameof(MProgressCircular.Width), new SliderParameter(4, 0, 12) }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MProgressCircular.Color), new SelectParameter(new List<string>() { "primary", "purple", "error","lime" }) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MProgressCircular.Size) => (StringNumber)(double)parameter.Value,
            nameof(MProgressCircular.Width) => (StringNumber)((double)parameter.Value),
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MProgressCircular.Value), (StringNumber)20 },
        };
    }
}
