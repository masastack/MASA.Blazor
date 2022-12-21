namespace Masa.Blazor.Docs.Examples.components.dividers;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MDivider)) { }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MDivider.Inset), new CheckboxParameter("false", true) },
        { nameof(MDivider.Vertical), new CheckboxParameter("false", true) },
    };
}