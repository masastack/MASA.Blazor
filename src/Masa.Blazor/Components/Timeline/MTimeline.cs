namespace Masa.Blazor
{
    public partial class MTimeline : BTimeline, IThemeable
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public bool AlignTop { get; set; }

        [Parameter]
        public bool Dense { get; set; }

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
            var prefix = "m-timeline";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--align-top", () => AlignTop)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--reverse", () => Reverse)
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}
