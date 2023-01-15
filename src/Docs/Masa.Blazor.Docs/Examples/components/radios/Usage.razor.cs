namespace Masa.Blazor.Docs.Examples.components.radios;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MRadioGroup<string>.Column), false },
        { nameof(MRadioGroup<string>.Row), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MRadioGroup<string>.Label), new CheckboxParameter("Radio group label",false) },
    };

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MRadio<string>>(0);
        builder.AddAttribute(1, nameof(MRadio<string>.Label), "Radio 1");
        builder.AddAttribute(2, nameof(MRadio<string>.Value), "1");
        builder.CloseComponent();

        builder.OpenComponent<MRadio<string>>(3);
        builder.AddAttribute(4, nameof(MRadio<string>.Label), "Radio 2");
        builder.AddAttribute(5, nameof(MRadio<string>.Value), "2");
        builder.CloseComponent();

        builder.OpenComponent<MRadio<string>>(6);
        builder.AddAttribute(7, nameof(MRadio<string>.Label), "Radio 3");
        builder.AddAttribute(8, nameof(MRadio<string>.Value), "3");
        builder.CloseComponent();
    };

    public Usage() : base(typeof(MRadioGroup<string>))
    {
    }
}