namespace Masa.Blazor.Docs.Examples.components.autocompletes;

public class Usage() : Masa.Blazor.Docs.Components.Usage(typeof(AdvanceUsage))
{
    protected override string ComponentName => "MAutocomplete";

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(AdvanceUsage.Filled), false },
        { nameof(AdvanceUsage.Solo), false },
        { nameof(AdvanceUsage.SoloInverted), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(AdvanceUsage.Chips), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.Clearable), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.DeletableChips), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.Dense), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.Multiple), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.Rounded), new CheckboxParameter("false", true) },
        { nameof(AdvanceUsage.SmallChips), new CheckboxParameter("false", true) },
    };
    
}
