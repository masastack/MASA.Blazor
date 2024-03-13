namespace Masa.Blazor
{
    public partial class MTimelineItem : BTimelineItem, IThemeable, ITimelineItem
    {
        [Parameter]
        [MasaApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public bool FillDot { get; set; }

        [Parameter]
        public bool HideDot { get; set; }

        [Parameter]
        public string? Icon { get; set; }

        [Parameter]
        public string? IconColor { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Small { get; set; }

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
            var prefix = "m-timeline-item";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--fill-dot", () => FillDot)
                        .AddIf($"{prefix}--before", () => BTimeline?.Reverse is true ? Right : Left)
                        .AddIf($"{prefix}--after", () => BTimeline?.Reverse is true ? Left : Right)
                        .AddTheme(IsDark, IndependentTheme);
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__body");
                })
                .Apply("dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__dot")
                        .AddIf($"{prefix}__dot--small", () => Small)
                        .AddIf($"{prefix}__dot--large", () => Large);
                })
                .Apply("inner-dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__inner-dot")
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color);
                })
                .Apply("divider", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__divider");
                })
                .Apply("opposite", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__opposite");
                });

            Attributes.Add("data-app", true);

            AbstractProvider
                .ApplyTimelineItemDefault()
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Color)] = IconColor;
                    attrs[nameof(MIcon.Dark)] = !IsDark;
                    attrs[nameof(MIcon.Small)] = Small;
                });
        }
    }
}