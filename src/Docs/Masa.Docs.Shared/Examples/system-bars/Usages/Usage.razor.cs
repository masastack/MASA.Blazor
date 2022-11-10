namespace Masa.Docs.Shared.Examples.system_bars;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MToolbar))
    {
    }

    protected override Type UsageWrapperType => typeof(UsageWrapper);

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSystemBar.Window), false },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MSystemBar.Height), new SliderParameter(30, 1, 60) }
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSystemBar.LightsOut), new CheckboxParameter("false", true) }
    };

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MIcon>(0);
        builder.AddAttribute(1, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-message")));
        builder.CloseComponent();

        builder.OpenElement(2, "span");
        builder.AddContent(3, "10 unread messages");
        builder.CloseComponent();

        builder.OpenComponent<MSpacer>(4);
        builder.CloseComponent();

        builder.OpenComponent<MIcon>(5);
        builder.AddAttribute(6, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-wifi-strength-4")));
        builder.CloseComponent();

        builder.OpenComponent<MIcon>(7);
        builder.AddAttribute(8, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-signal-cellular-outline")));
        builder.CloseComponent();

        builder.OpenComponent<MIcon>(9);
        builder.AddAttribute(10, "ChildContent", (RenderFragment)(b => b.AddContent(0, "mdi-battery")));
        builder.CloseComponent();

        builder.OpenElement(11, "span");
        builder.AddContent(12, "12:30");
        builder.CloseComponent();
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MToolbar.Height) => (StringNumber)(double)parameter.Value,
            _ => parameter.Value
        };
    }
}