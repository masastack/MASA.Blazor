namespace Masa.Blazor.Docs.Examples.components.cards;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MCard))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MCard.Outlined), false },
        { nameof(MCard.Shaped), false },
        { nameof(MCard.Tile), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MCard.Disabled), new CheckboxParameter() },
        { nameof(MCard.Loading), new CheckboxParameter() },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MButton.Elevation), new SliderParameter(2, 0, 24) }
    };

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MCardTitle>(0);
        builder.AddChildContent(1, "Card title");
        builder.CloseComponent();

        builder.OpenComponent<MCardSubtitle>(2);
        builder.AddChildContent(3, "Subtitle text");
        builder.CloseComponent();

        builder.OpenComponent<MCardText>(4);
        builder.AddChildContent(5, "Greyhound divisively hello coldly wonderfully marginally far upon excluding.");
        builder.CloseComponent();

        builder.OpenComponent<MCardActions>(6);
        builder.AddAttribute(7, nameof(MCardActions.ChildContent), (RenderFragment)(childBuilder =>
        {
            childBuilder.OpenComponent<MButton>(0);
            childBuilder.AddAttribute(1, nameof(MButton.Class), "mt-6");
            childBuilder.AddAttribute(2, nameof(MButton.Color), "primary");
            childBuilder.AddAttribute(3, nameof(MButton.Text), true);
            childBuilder.AddChildContent(4, "Action 1");
            childBuilder.CloseComponent();

            childBuilder.OpenComponent<MButton>(0);
            childBuilder.AddAttribute(1, nameof(MButton.Class), "mt-6");
            childBuilder.AddAttribute(2, nameof(MButton.Color), "primary");
            childBuilder.AddAttribute(3, nameof(MButton.Text), true);
            childBuilder.AddChildContent(4, "Action 2");
            childBuilder.CloseComponent();
        }));

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
            nameof(MCard.Elevation) => (StringNumber)(double)parameter.Value,
            nameof(MCard.Loading) => (StringBoolean)(bool)parameter.Value,
            _ => parameter.Value
        };
    }
}
