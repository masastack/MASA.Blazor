namespace Masa.Blazor
{
    public partial class MProgressCircular : BProgressCircular, IProgressCircular
    {
        protected int Radius => 20;

        protected bool IsVisible => true;

        protected double ViewBoxSize => Radius / (1 - (double)Width.ToInt32(DEFAULT_WIDTH) / +Size.ToInt32(DEFAULT_SIZE));

        protected double StrokeWidth => (double)Width.ToInt32(DEFAULT_WIDTH) / +Size.ToInt32(DEFAULT_SIZE) * ViewBoxSize * 2;

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

        public Dictionary<string, object?> SvgAttrs => new()
        {
            { "viewBox", $"{ViewBoxSize} {ViewBoxSize} {ViewBoxSize * 2} {ViewBoxSize * 2}" }
        };

        public Dictionary<string, object?> CircleAttrs => new()
        {
            { "fill", "transparent" },
            { "cx", $"{ViewBoxSize * 2}" },
            { "cy", $"{ViewBoxSize * 2}" },
            { "r", Radius },
            { "stroke-width", StrokeWidth },
            { "stroke-dasharray", StrokeDashArray }
        };

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular")
                        .AddIf("m-progress-circular--visible", () => IsVisible)
                        .AddIf("m-progress-circular--indeterminate", () => Indeterminate)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Size)
                        .AddWidth(Size)
                        .AddTextColor(Color);
                })
                .Apply("svg", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add($"transform: rotate({Rotate}deg)");
                })
                .Apply("underlay-circle", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular__underlay");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"stroke: {BackgroundColor}", () => !string.IsNullOrWhiteSpace(BackgroundColor));
                })
                .Apply("overlay-circle", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular__overlay");
                })
                .Apply("info", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-circular__info");
                });

            AbstractProvider
                .ApplyProgressCircularDefault();
        }
    }
}
