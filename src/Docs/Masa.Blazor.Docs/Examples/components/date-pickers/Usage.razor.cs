namespace Masa.Blazor.Docs.Examples.components.date_pickers;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MDatePicker<DateOnly>))
    {
    }

    private DateOnly _picker = DateOnly.FromDateTime(DateTime.Now);

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MDatePicker<DateOnly>.Value), _picker },
            { nameof(MDatePicker<DateOnly>.ValueChanged), EventCallback.Factory.Create<DateOnly>(this, val =>
            {
                _picker = val;
            }) },
        };
    }
}
