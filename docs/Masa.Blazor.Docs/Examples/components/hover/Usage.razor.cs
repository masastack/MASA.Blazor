namespace Masa.Blazor.Docs.Examples.components.hover;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MHover))
    {
    }

    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MHover.Disabled), false },
    };

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MHover.CloseDelay), new SliderParameter(0, 0, 1000) },
        { nameof(MHover.OpenDelay), new SliderParameter(0, 0, 1000) }
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        if (parameter.Value == null)
        {
            return parameter.Value;
        }

        return parameter.Key switch
        {
            nameof(MHover.CloseDelay) => Convert.ToInt32(parameter.Value),
            nameof(MHover.OpenDelay) => Convert.ToInt32(parameter.Value),
            _ => parameter.Value
        };
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            {
                nameof(MHover.ChildContent), new RenderFragment<HoverProps>(context => builder =>
                {
                    builder.OpenComponent<MCard>(0);
                    builder.AddMultipleAttributes(1, context.Attrs);
                    builder.AddAttribute(2, nameof(MCard.Class), "mx-auto");
                    builder.AddAttribute(3, nameof(MCard.Height), (StringNumber)("350"));
                    builder.AddAttribute(4, nameof(MCard.MaxWidth), (StringNumber)"350");
                    builder.AddAttribute(5, nameof(MCard.Elevation), (StringNumber)$"{(context.Hover ? 12 : 2)}");
                    builder.AddAttribute(6, nameof(MCard.ChildContent), (RenderFragment)(childBuilder =>
                    {
                        childBuilder.OpenComponent<MCardText>(0);
                        childBuilder.AddAttribute(1, nameof(MCardText.Class), "my-4 text-center text-h6");
                        childBuilder.AddChildContent(2, "Hover over me!");
                        childBuilder.CloseComponent();
                    }));
                    builder.CloseComponent();
                })
            }
        };
    }
}
