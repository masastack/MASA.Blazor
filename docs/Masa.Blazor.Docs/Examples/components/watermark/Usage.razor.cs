namespace Masa.Blazor.Docs.Examples.components.watermark;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(MWatermark))
    {
    }

    protected override ParameterList<SliderParameter> GenSliderParameters() => new()
    {
        { nameof(MWatermark.TextSize), new SliderParameter(16, 12, 32) },
        { nameof(MWatermark.Rotate), new SliderParameter(-22, -180, 180, false) },
        { nameof(MWatermark.Left), new SliderParameter(0, -100, 100, false) },
        { nameof(MWatermark.Top), new SliderParameter(0, -100, 100, false) },
        { nameof(MWatermark.GapX), new SliderParameter(50, 0, 200) },
        { nameof(MWatermark.GapY), new SliderParameter(50, 0, 200) },
    };

    protected override Dictionary<string, object>? GenAdditionalParameters() => new()
    {
        { nameof(MWatermark.Text), "MASA Blazor" }
    };

    protected override object? CastValue(ParameterItem<object?> parameter)
    {
        switch (parameter.Key)
        {
            case nameof(MWatermark.TextSize):
            case nameof(MWatermark.Rotate):
            case nameof(MWatermark.Left):
            case nameof(MWatermark.Top):
            case nameof(MWatermark.GapX):
            case nameof(MWatermark.GapY):
                return Convert.ToInt32(parameter.Value);
            default:
                return base.CastValue(parameter);
        }
    }

    protected override RenderFragment? GenChildContent() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "style", "height: 250px; width: 500px;");
        builder.CloseElement();
    };
}
