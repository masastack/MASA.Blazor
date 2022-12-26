namespace Masa.Blazor.Docs.Examples.components.data_iterators;

public partial class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(AdvanceUsage))
    {
    }

    protected override string ComponentName => "MDataIterator";

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(AdvanceUsage.DisableFiltering), new CheckboxParameter() },
        { nameof(AdvanceUsage.DisablePagination), new CheckboxParameter() },
        { nameof(AdvanceUsage.DisableSort), new CheckboxParameter() },
        { nameof(AdvanceUsage.HideDefaultFooter), new CheckboxParameter() },
    };
}
