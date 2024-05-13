namespace Masa.Blazor
{
    public partial class MDivider : BDivider
    {
        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public int Length { get; set; }

        private bool HasContent => ChildContent is not null;

        private bool IsCenter => HasContent && (!Left && !Right || Center);

        private int PaddingY
        {
            get
            {
                if (Height <= 0)
                {
                    return 0;
                }

                return Height / 2;
            }
        }
        
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
            var prefix = "m-divider";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__wrapper")
                        .AddIf($"{prefix}__wrapper--has-content", () => HasContent)
                        .AddIf($"{prefix}__wrapper--center", () => IsCenter)
                        .AddIf($"{prefix}__wrapper--left", () => Left)
                        .AddIf($"{prefix}__wrapper--right", () => Right);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:contents", () => !HasContent)
                        .AddIf($"height: {Length}px", () => Vertical && Length > 0)
                        .AddIf($"width: {Length}px", () => !Vertical && Length > 0)
                        .AddIf($"padding: {PaddingY}px 0", () => PaddingY > 0);
                })
                .Apply("hr", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-divider")
                        .AddIf($"{prefix}--inset", () => Inset)
                        .AddIf($"{prefix}--vertical", () => Vertical)
                        .AddIf(Class, () => !HasContent)
                        .AddTheme(IsDark, IndependentTheme);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("min-height: 0", () => Vertical && Length > 0)
                        .AddIf(Style, () => !HasContent);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}--inset", () => Inset)
                        .AddTheme(IsDark, IndependentTheme);
                });
        }
    }
}
