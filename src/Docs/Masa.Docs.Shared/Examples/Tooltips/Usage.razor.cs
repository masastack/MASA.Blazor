namespace Masa.Docs.Shared.Examples.tooltips;

public class Usage : Masa.Docs.Shared.Components.Usage
{
    public Usage() : base(typeof(MTooltip))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenElement(0, "span");
        builder.AddContent(1, "Tooltip");
        builder.CloseComponent();
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MTooltip.Bottom),true},
            {
                nameof(MTooltip.ActivatorContent), new RenderFragment<ActivatorProps>(context => builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddAttribute(1, nameof(MButton.Color), "primary");
                    builder.AddAttribute(2, nameof(MButton.Dark), true);
                    builder.AddMultipleAttributes(3, context.Attrs);
                    builder.AddChildContent(4, "Bottom");
                    builder.CloseComponent();
                })
            }
        };
    }
}
