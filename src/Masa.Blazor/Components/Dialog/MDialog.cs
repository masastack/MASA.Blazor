namespace Masa.Blazor
{
    public partial class MDialog : BDialog, IDialog, IThemeable
    {
        [Parameter]
        public string ContentClass { get; set; }

        [Parameter]
        public string Origin { get; set; } = "center center";

        [Parameter]
        public string OverlayScrimClass { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public string Transition { get; set; } = "dialog-transition";

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

        protected override string AttachSelector => Attach ?? ".m-application";

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
                    attrs[nameof(MOverlay.Value)] = ShowOverlay && IsActive;
                    attrs[nameof(MOverlay.ZIndex)] = ZIndex - 1;
                })
                .ApplyDialogDefault();
        }
    }
}