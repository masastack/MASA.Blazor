namespace Masa.Blazor.Docs.Examples.components.otp_input;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MOtpInput))
    {
    }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MOtpInput.Disabled), new CheckboxParameter("false", true) },
        { nameof(MOtpInput.Dark), new CheckboxParameter("false", true) },
        { nameof(MOtpInput.Plain), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MOtpInput.Length), new SliderParameter(6, 2, 8) }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MOtpInput.Type), new SelectParameter(new List<string>() { "Text", "Password", "Number" }) },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MOtpInput.Length) => (int)(double)parameter.Value,
            nameof(MOtpInput.Type) => Enum.Parse<OtpInputType>((string)parameter.Value),
            _ => parameter.Value
        };
    }
}
