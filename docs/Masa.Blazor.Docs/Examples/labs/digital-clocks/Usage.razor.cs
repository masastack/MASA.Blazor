namespace Masa.Blazor.Docs.Examples.labs.digital_clocks;

public partial class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MDigitalClock<TimeOnly>))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MDigitalClock<TimeOnly>.MultiSection), false }
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MDigitalClock<TimeOnly>.UseSeconds), new CheckboxParameter() },
        { nameof(MDigitalClock<TimeOnly>.Disabled), new CheckboxParameter() },
        { nameof(MDigitalClock<TimeOnly>.Readonly), new CheckboxParameter() }
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MDigitalClock<TimeOnly>.Format), new SelectParameter(new List<string>() { "24 Hour", "AM/PM" }, "AM/PM") },
        { nameof(MDigitalClock<TimeOnly>.Color), new SelectParameter(new List<string>() { "primary", "accent" }, "primary") },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        return parameter.Key switch
        {
            nameof(MDigitalClock<TimeOnly>.Format) => parameter.Value is "AM/PM" ? TimeFormat.AmPm : TimeFormat.Hr24,
            _                                      => base.CastValue(parameter)
        };
    }

    protected override string FormatValue(string key, object value)
    {
        return key switch
        {
            nameof(MDigitalClock<TimeOnly>.Format) => value is "AM/PM" ? "TimeFormat.AmPm" : "TimeFormat.Hr24",
            _                                      => base.FormatValue(key, value)
        };
    }
}
