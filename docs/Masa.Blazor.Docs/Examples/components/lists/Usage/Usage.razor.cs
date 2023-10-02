namespace Masa.Blazor.Docs.Examples.components.lists;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(AdvanceUsage.TwoLine), false },
        { nameof(AdvanceUsage.ThreeLine), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(AdvanceUsage.ShowAvatar), new CheckboxParameter() },
        { nameof(AdvanceUsage.Dense), new CheckboxParameter() }
    };

    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => nameof(MList);

    protected override IEnumerable<ParameterItem<CheckboxParameter>> ActiveCheckboxParameters
        => base.ActiveCheckboxParameters.Where(u => u.Key != nameof(AdvanceUsage.ShowAvatar));
}
