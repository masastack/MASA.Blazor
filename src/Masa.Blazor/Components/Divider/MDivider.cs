namespace Masa.Blazor
{
    public partial class MDivider : BDivider, IThemeable
    {
        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

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

        protected override void SetComponentClass()
        {
            var prefix = "m-divider";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-divider")
                        .AddIf($"{prefix}--inset", () => Inset)
                        .AddIf($"{prefix}--vertical", () => Vertical)
                        .AddTheme(IsDark);
                })
                .Apply("wrapper", cssBuilder =>
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
                        .AddIf($"padding: {PaddingY}px 0", () => PaddingY > 0);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}--inset", () => Inset)
                        .AddTheme(IsDark);
                });
        }
    }
}