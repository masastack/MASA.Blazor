using Masa.Blazor;

namespace Masa.Docs.Shared.Examples.text_fields;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MTextField<string>.Outlined), false },
        { nameof(MTextField<string>.Solo), false },
        { nameof(MTextField<string>.Filled), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MTextField<string>.PrependIcon), new CheckboxParameter("false",false) },
        { nameof(MTextField<string>.Clearable), new CheckboxParameter("false",true) },
    };

    public Usage() : base(typeof(MTextField<string>))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MTextField<string>.Label), "Label" },
        };
    }
}