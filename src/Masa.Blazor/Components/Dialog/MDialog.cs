namespace Masa.Blazor
{
    public partial class MDialog : BDialog, IDialog, IThemeable
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public string? ContentClass { get; set; }

        [Parameter]
        [MassApiParameter("center center")]
        public string Origin { get; set; } = "center center";

        [Parameter]
        public string? OverlayScrimClass { get; set; }

        /// <summary>
        /// The lazy content would be created in a [data-permanent] element.
        /// It's useful when you use this component in a layout.
        /// </summary>
        [Parameter]
        public bool Permanent { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        [MassApiParameter("dialog-transition")]
        public string? Transition { get; set; } = "dialog-transition";

        public Dictionary<string, object> ContentAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object>
                {
                    { "role", "document" }
                };

                if (IsActive)
                {
                    attrs.Add("tabindex", 0);
                }

                return attrs;
            }
        }

        bool IDialog.IsBooted => IsBooted;

        protected override string AttachSelector
            => Attach ?? (Permanent ? ".m-application__permanent" : ".m-application");

        protected override bool IsFullscreen => Fullscreen && MasaBlazor.Breakpoint.SmAndDown;

        public override IEnumerable<string> DependentSelectors
            => base.DependentSelectors.Concat(new[] { MSnackbar.ROOT_CSS_SELECTOR, PEnqueuedSnackbars.ROOT_CSS_SELECTOR }).Distinct();

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__container")
                        .AddIf($"{prefix}__container--attached", () => Attach != null);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}__content--active", () => IsActive)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"z-index: {ZIndex}");
                })
                .Apply("innerContent", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .Add(ContentClass)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--persistent", () => Persistent)
                        .AddIf($"{prefix}--fullscreen", () => Fullscreen)
                        .AddIf($"{prefix}--scrollable", () => Scrollable)
                        .AddIf($"{prefix}--animated", () => Animated);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"transform-origin: {Origin}")
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth);
                });

            AbstractProvider
                .Apply<BOverlay, MOverlay>(attrs =>
                {
                    attrs[nameof(MOverlay.ScrimClass)] = OverlayScrimClass;
                    attrs[nameof(MOverlay.Value)] = IsActive;
                    attrs[nameof(MOverlay.Scrim)] = ShowOverlay;
                    attrs[nameof(MOverlay.ZIndex)] = ZIndex - 1;
                })
                .ApplyDialogDefault();
        }
    }
}
