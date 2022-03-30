namespace Masa.Blazor
{
    public partial class MProgressCircular : BProgressCircular, IProgressCircular
    {
        protected int Radius => 20;

        protected bool IsVisible => true;

        protected double ViewBoxSize => Radius / (1 - (double)Width.ToInt32() / +Size.ToInt32());

        protected double StrokeWidth => (double)Width.ToInt32() / +Size.ToInt32() * ViewBoxSize * 2;

        protected double Circumference => 2 * Math.PI * Radius;

        protected double StrokeDashArray => Math.Round(Circumference * 1000) / 1000;

        protected float NormalizedValue => Value.ToInt32() < 0 ? 0 :
            Value.ToInt32() > 100 ? 100 : Value.ToInt32();

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
                        .AddWidth(Size);
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
