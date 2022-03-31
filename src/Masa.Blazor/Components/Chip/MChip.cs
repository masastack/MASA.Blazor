namespace Masa.Blazor
{
    public partial class MChip : BChip, IThemeable, IChip, ISizeable
    {
        private Sizer _sizer;

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Draggable { get; set; }

        [Parameter]
        public bool Filter { get; set; }

        [Parameter]
        public string FilterIcon { get; set; } = "mdi-check";

        [Parameter]
        public bool Label { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Pill { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public string TextColor { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _sizer = new Sizer(this);

            Attributes["ripple"] = Ripple && IsClickable;
            Attributes["draggable"] = Draggable ? "true" : null;
            Attributes["tabindex"] = Matched && !Disabled ? 0 : Tabindex;
        }

        protected override void SetComponentClass()
        {
            CloseIcon ??= "mdi-close-circle";

            var prefix = "m-chip";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .Add(_sizer.SizeableClasses)
                        .AddIf($"{prefix}--clickable", () => IsClickable)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--draggable", () => Draggable)
                        .AddIf($"{prefix}--label", () => Label)
                        .AddIf($"{prefix}--link", () => IsLink)
                        .AddIf($"{prefix}--no-color", () => string.IsNullOrEmpty(Color))
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--pill", () => Pill)
                        .AddIf($"{prefix}--removable", () => Close)
                        .AddIf($"{prefix}--active {ComputedActiveClass}", () => InternalIsActive)
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddTextColor(Color, () => Outlined)
                        .AddTextColor(TextColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !Active)
                        .AddBackgroundColor(Color)
                        .AddTextColor(Color, () => Outlined)
                        .AddTextColor(Color);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-chip__content");
                });

            AbstractProvider
                .ApplyChipDefault()
                .Apply<BIcon, MIcon>("close", attrs =>
                {
                    attrs[nameof(Class)] = "m-chip__close";
                    attrs[nameof(MIcon.Right)] = true;
                    attrs[nameof(MIcon.Size)] = (StringNumber)18;
                    attrs[nameof(MIcon.OnClick)] = OnCloseClick;
                    attrs[nameof(MIcon.Attributes)] = new Dictionary<string, object>()
                    {
                        {"aria-label", CloseLabel}
                    };
                })
                .Apply<BIcon, MIcon>("filter", attrs =>
                {
                    attrs[nameof(Class)] = "m-chip__filter";
                    attrs[nameof(MIcon.Left)] = true;
                });
        }
    }
}