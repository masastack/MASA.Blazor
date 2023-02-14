namespace Masa.Blazor.Docs.Examples.components.system_bars;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => nameof(MSystemBar);

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSystemBar.Window), false },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MSystemBar.Height), new SliderParameter(30, 1, 60) }
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSystemBar.LightsOut), new CheckboxParameter("false", true) }
    };

    protected override Dictionary<string, object>? GenAdditionalParameters() => new()
    {
        { nameof(MSystemBar.Color), "orange" }
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MToolbar.Height) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }
}
