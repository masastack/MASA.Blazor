namespace Masa.Blazor
{
    public partial class MLabel : BLabel, IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public StringNumber Left { get; set; } = 0;

        [Parameter]
        public StringNumber Right { get; set; } = "auto";

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Focused { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        [Parameter]
        public bool Value { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-label";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => Value)
                        .AddIf($"{prefix}--is-disabled", () => Disabled)
                        .AddTheme(IsDark)
                        .AddTextColor(Color, () => Focused);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"left:{Left.ToUnit()}")
                        .Add(() => $"right:{Right.ToUnit()}")
                        .Add(() => $"position:{(Absolute ? "absolute" : "relative")}")
                        .AddTextColor(Color, () => Focused);
                });
        }
    }
}
