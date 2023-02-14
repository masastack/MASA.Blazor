namespace Masa.Blazor.Docs.Examples.components.textareas;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => nameof(MTextarea);

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MTextarea.Outlined), false },
        { nameof(MTextarea.Solo), false },
        { nameof(MTextarea.Filled), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MTextarea.PrependIcon), new CheckboxParameter("mdi-heart", false) },
        { nameof(MTextarea.Clearable), new CheckboxParameter("false", true) },
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MTextarea.Label), "Label" },
        };
    }
}
