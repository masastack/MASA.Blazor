namespace Masa.Blazor.Docs.Examples.components.sheets;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSheet.Outlined), false },
        { nameof(MSheet.Rounded), false },
        { nameof(MSheet.Shaped), false },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MSheet.Elevation), new SliderParameter(1, 0, 24, false) },
        { nameof(MSheet.Height), new SliderParameter(100, 50 , 250, false) },
        { nameof(MSheet.Width), new SliderParameter(100, 50, 250, false) },
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MSheet.Color), new SelectParameter(new List<string>() { "white", "grey darken-2", "warning", "error", "success", "teal", "teal"},"white") },
    };

    public Usage() : base(typeof(MSheet))
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
            nameof(MSheet.Elevation) => (StringNumber)(double)parameter.Value,
            nameof(MSheet.Height) => (StringNumber)(double)parameter.Value,
            nameof(MSheet.Width) => (StringNumber)(double)parameter.Value,
            nameof(MSheet.Rounded) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }
}