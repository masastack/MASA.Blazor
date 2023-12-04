namespace Masa.Blazor
{
    public partial class MLabel : BLabel, IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        [MassApiParameter(0)]
        public StringNumber? Left { get; set; } = 0;

        [Parameter]
        [MassApiParameter("auto")]
        public StringNumber? Right { get; set; } = "auto";

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Focused { get; set; }

        [Parameter]
        [MassApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public bool Value { get; set; }

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

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
                        .AddTheme(IsDark, IndependentTheme)
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
