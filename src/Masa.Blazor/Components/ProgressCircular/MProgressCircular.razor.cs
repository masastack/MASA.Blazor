using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MProgressCircular : MasaComponentBase
{
    [Parameter] public bool Indeterminate { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter]
    [MasaApiParameter(DEFAULT_SIZE)]
    public StringNumber? Size { get; set; } = DEFAULT_SIZE;

    [Parameter]
    [MasaApiParameter(DEFAULT_ROTATE)]
    public StringNumber? Rotate { get; set; } = DEFAULT_ROTATE;

    [Parameter]
    [MasaApiParameter(DEFAULT_WIDTH)]
    public StringNumber? Width { get; set; } = DEFAULT_WIDTH;

    [Parameter]
    [MasaApiParameter(DEFAULT_VALUE)]
    public StringNumber? Value { get; set; } = DEFAULT_VALUE;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected const int DEFAULT_SIZE = 32;
    protected const int DEFAULT_WIDTH = 4;
    protected const int DEFAULT_VALUE = 0;
    protected const int DEFAULT_ROTATE = 0;

    protected int Radius => 20;

    protected bool IsVisible => true;

    protected double ViewBoxSize => Radius / (1 - (double)Width.ToInt32(DEFAULT_WIDTH) / +Size.ToInt32(DEFAULT_SIZE));

    protected double StrokeWidth =>
        (double)Width.ToInt32(DEFAULT_WIDTH) / +Size.ToInt32(DEFAULT_SIZE) * ViewBoxSize * 2;

    protected double Circumference => 2 * Math.PI * Radius;

    protected double StrokeDashArray => Math.Round(Circumference * 1000) / 1000;

    protected float NormalizedValue
    {
        get
        {
            var value = Value.ToInt32(DEFAULT_VALUE);

            return value < 0
                ? 0
                : value > 100
                    ? 100
                    : value;
        }
    }

    public string StrokeDashOffset => $"{(100 - NormalizedValue) / 100 * Circumference}px";

    public Dictionary<string, object> SvgAttrs => new()
    {
        { "viewBox", $"{ViewBoxSize} {ViewBoxSize} {ViewBoxSize * 2} {ViewBoxSize * 2}" }
    };

    public Dictionary<string, object> CircleAttrs => new()
    {
        { "fill", "transparent" },
        { "cx", $"{ViewBoxSize * 2}" },
        { "cy", $"{ViewBoxSize * 2}" },
        { "r", Radius },
        { "stroke-width", StrokeWidth },
        { "stroke-dasharray", StrokeDashArray }
    };

    private Block _block = new("m-progress-circular");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier("visible", IsVisible)
            .And(Indeterminate)
            .AddTextColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Size)
            .AddWidth(Size)
            .AddTextColor(Color)
            .GenerateCssStyles();
    }
}