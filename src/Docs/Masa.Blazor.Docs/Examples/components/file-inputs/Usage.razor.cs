using Microsoft.AspNetCore.Components.Forms;

namespace Masa.Blazor.Docs.Examples.components.file_inputs;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MFileInput<IBrowserFile>))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MFileInput<IBrowserFile>.Chips), false },
        { nameof(MFileInput<IBrowserFile>.SmallChips), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MFileInput<IBrowserFile>.Counter), new CheckboxParameter("false", true) },
        { nameof(MFileInput<IBrowserFile>.Disabled), new CheckboxParameter("false", true) },
        { nameof(MFileInput<IBrowserFile>.HideInput), new CheckboxParameter("false", true) },
        { nameof(MFileInput<IBrowserFile>.ShowSize), new CheckboxParameter("false", true) },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MFileInput<IBrowserFile>.TruncateLength), new SliderParameter(15, 0, 50) }
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MFileInput<IBrowserFile>.TruncateLength) => (StringNumber)(double)parameter.Value,
            nameof(MFileInput<IBrowserFile>.Counter) => (StringNumberBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }
}
