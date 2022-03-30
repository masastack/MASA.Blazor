namespace Masa.Blazor
{
    public class MAlert : BAlert, IThemeable
    {
        [Parameter]
        public StringBoolean Icon { get; set; }

        [Parameter]
        public bool ColoredBorder { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Prominent { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        private string ComputedType => Type != AlertTypes.None ? Type.ToString().ToLower() : "";

        private string ComputedColor => Color ?? ComputedType;

        private (bool, RenderFragment) ComputedIcon()
        {
            if (Icon != null && Icon.IsT1 && Icon.AsT1 == false) return (false, null);

            if (Icon != null && Icon.IsT0 && Icon.AsT0 != null)
                return (true, builder => builder.AddContent(0, Icon.AsT0));

            var iconText = Type switch
            {
                AlertTypes.Success => "mdi-checkbox-marked-circle-outline",
                AlertTypes.Error => "mdi-alert-circle-outline",
                AlertTypes.Info => "mdi-information",
                AlertTypes.Warning => "mdi-alert-outline",
                _ => null
            };

            if (iconText == null) return (false, null);

            return (true, builder => builder.AddContent(0, iconText));
        }

        private bool HasText => Text || Outlined;

        private bool HasColoredBorder => Border != Borders.None && ColoredBorder;

        private bool HasColoredIcon => HasText || HasColoredBorder;

        private bool HasTypedBorder => ColoredBorder && Type != AlertTypes.None;

        private string IconColor => HasColoredIcon ? ComputedColor : "";

        private bool IsDarkTheme => (Type != AlertTypes.None && !ColoredBorder && !Outlined) || IsDark;

        protected override void OnParametersSet()
        {
            (IsShowIcon, IconContent) = ComputedIcon();
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert")
                        .Add("m-sheet")
                        .AddIf("m-alert--border", () => Border != Borders.None)
                        .Add(BorderClass)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddTheme(IsDarkTheme)
                        .AddElevation(Elevation)
                        .AddFirstIf(
                            (() => "m-alert--prominent", () => Prominent),
                            (() => "m-alert--dense", () => Dense))
                        .AddIf("m-alert--text", () => Text)
                        .AddIf("m-alert--outlined", () => Outlined)
                        .AddColor(ComputedColor, HasText, () => !ColoredBorder)
                        .AddRounded(Rounded, Tile);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddColor(ComputedColor, HasText, () => !ColoredBorder)
                        .AddHeight(Height)
                        .AddMaxHeight(MaxHeight)
                        .AddMinHeight(MinHeight)
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth)
                        .AddMinWidth(MinWidth)
                        .AddIf("display:none", () => Transition == null && !Value);
                })
                .Apply("wrapper", cssBuilder => { cssBuilder.Add("m-alert__wrapper"); })
                .Apply("content", cssBuilder => { cssBuilder.Add("m-alert__content"); })
                .Apply("border", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert__border")
                        .Add(BorderClass)
                        .AddIf("m-alert__border--has-color", () => ColoredBorder)
                        .AddIf(() => Type.ToString().ToLower(), () => HasTypedBorder)
                        .AddTextColor(Color, () => ColoredBorder);
                }, styleBuilder => { styleBuilder.AddTextColor(Color, () => ColoredBorder); });

            AbstractProvider
                .Apply(typeof(BAlertWrapper<>), typeof(BAlertWrapper<MAlert>))
                .Apply(typeof(BAlertIcon<>), typeof(BAlertIcon<MAlert>))
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Color)] = IconColor;
                    attrs[nameof(MIcon.Dark)] = IsDarkTheme;
                    attrs[nameof(MIcon.Class)] = "m-alert__icon";
                })
                .Apply(typeof(BAlertContent<>), typeof(BAlertContent<MAlert>))
                .Apply(typeof(BAlertDismissButton<>), typeof(BAlertDismissButton<MAlert>))
                .Apply<BButton, MButton>("dismissible", attrs =>
                {
                    attrs[nameof(MButton.Color)] = IconColor;
                    attrs[nameof(MButton.Dark)] = IsDarkTheme;
                    attrs[nameof(MButton.Class)] = "m-alert__dismissible";
                })
                .Apply<BIcon, MIcon>("dismissible", attrs => { attrs[nameof(MIcon.Dark)] = IsDarkTheme; });

            string BorderClass() => Border switch
            {
                Borders.Left => "m-alert__border--left",
                Borders.Right => "m-alert__border--right",
                Borders.Top => "m-alert__border--top",
                Borders.Bottom => "m-alert__border--bottom",
                Borders.None => "",
                _ => throw new ArgumentOutOfRangeException(nameof(Border))
            };
        }
    }
}