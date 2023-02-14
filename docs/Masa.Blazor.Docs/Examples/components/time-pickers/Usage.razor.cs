namespace Masa.Blazor.Docs.Examples.components.time_pickers;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MTimePicker))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MTimePicker.Landscape), false },
        { nameof(MTimePicker.Scrollable), false }
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MTimePicker.AmPmInTitle),new CheckboxParameter("false",true)},
        { nameof(MTimePicker.Disabled),new CheckboxParameter("false",true)},
        { nameof(MTimePicker.FullWidth),new CheckboxParameter("false",true)},
        { nameof(MTimePicker.NoTitle),new CheckboxParameter("false",true)},
        { nameof(MTimePicker.Readonly),new CheckboxParameter("false",true)},
        { nameof(MTimePicker.UseSeconds),new CheckboxParameter("false",true)},
    };

    protected override ParameterList<SelectParameter> GenSelectParameters() => new()
    {
        { nameof(MTimePicker.Format), new SelectParameter(new List<string>() { "AmPm", "Hr24"}, "AmPm") },
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MTimePicker.Format) => Enum.Parse<TimeFormat>((string)parameter.Value),
            _ => parameter.Value
        };
    }
}
