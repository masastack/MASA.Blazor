using Masa.Blazor;

namespace Masa.Docs.Shared.Examples.textareas;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MTextarea.Outlined), false },
        { nameof(MTextarea.Solo), false },
        { nameof(MTextarea.Filled), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MTextarea.PrependIcon), new CheckboxParameter("false",false) },
        { nameof(MTextarea.Clearable), new CheckboxParameter("false",true) },
    };

    public Usage() : base(typeof(MTextarea))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MTextarea.Label), "Label" },
        };
    }
}