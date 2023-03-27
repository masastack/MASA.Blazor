namespace Masa.Blazor.Docs.Examples.components.text_fields;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override string ComponentName => "MTextField";

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MTextField<string>.Outlined), false },
        { nameof(MTextField<string>.Solo), false },
        { nameof(MTextField<string>.Filled), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MTextField<string>.PrependIcon), new CheckboxParameter("mdi-text-box", false) },
        { nameof(MTextField<string>.Clearable), new CheckboxParameter("false", true) },
    };

    public Usage() : base(typeof(AdvanceUsage))
    {
    }
}
