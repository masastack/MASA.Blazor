namespace Masa.Blazor
{
    public class MIcon : BIcon, ISizeable
    {
        [Inject]
        private MasaBlazor MasaBlazor { get; set; } = null!;

        /// <summary>
        /// 36px
        /// </summary>
        [Parameter]
        public bool Large { get; set; }

        /// <summary>
        /// 16px
        /// </summary>
        [Parameter]
        public bool Small { get; set; }

        /// <summary>
        /// 40px
        /// </summary>
        [Parameter]
        public bool XLarge { get; set; }

        /// <summary>
        /// 12px
        /// </summary>
        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool IsActive { get; set; } = true;

        private readonly Dictionary<string, string> _sizeMap = new()
        {
            { nameof(XSmall), "12px" },
            { nameof(Small), "16px" },
            { "Default", "24px" },
            { nameof(Medium), "28px" },
            { nameof(Large), "36px" },
            { nameof(XLarge), "40px" },
        };

        private string? _iconCss;

        public bool Medium => false;

        protected override void InitIcon()
        {
            Icon? icon;

            if (Icon != null)
            {
                icon = Icon.IsAlias ? MasaBlazor.Icons.Aliases.GetIconOrDefault(Icon.AsT0) : Icon;
            }
            else
            {
                var textContent = ChildContent?.GetTextContent();
                IconContent = textContent;

                if (textContent is null)
                {
                    return;
                }

                if (textContent.StartsWith("$"))
                {
                    icon = MasaBlazor.Icons.Aliases.GetIconOrDefault(textContent);
                }
                else
                {
                    icon = CheckIfSvg(textContent) ? new SvgPath(textContent) : textContent;
                }
            }

            if (icon is null)
            {
                return;
            }

            if (icon.IsSvg)
            {
                ComputedIcon = icon;
            }
            else
            {
                (ComputedIcon, _iconCss) = ResolveIcon(icon.AsT0);
            }
        }

        private(string? icon, string? css) ResolveIcon(string cssIcon)
        {
            var defaultAliases = MasaBlazor.Icons.Aliases;

            var splits = cssIcon.Split(":");
            var icon = splits[0];

            if (splits.Length == 2)
            {
                defaultAliases = splits[0] switch
                {
                    "mdi" => DefaultIconAliases.MaterialDesignIcons,
                    "md"  => DefaultIconAliases.MaterialDesign,
                    "fa"  => DefaultIconAliases.FontAwesome,
                    "fa4" => DefaultIconAliases.FontAwesome4,
                    _     => defaultAliases
                };
                icon = splits[1];
            }

            return (defaultAliases.ContentFormatter?.Invoke(icon), defaultAliases.CssFormatter?.Invoke(icon));
        }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        public string? GetSize()
        {
            var sizes = new Dictionary<string, bool>()
            {
                { nameof(XSmall), XSmall },
                { nameof(Small), Small },
                { nameof(Medium), Medium },
                { nameof(Large), Large },
                { nameof(XLarge), XLarge },
            };

            var key = sizes.FirstOrDefault(item => item.Value).Key;

            if (key != null && _sizeMap.TryGetValue(key, out var px))
            {
                return px;
            }

            return Size?.ToUnit();
        }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
        }

        protected override void SetComponentCss()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-icon")
                        .AddIf(_iconCss, () => ComputedIcon is { IsSvg: false })
                        .AddIf("m-icon--link", () => OnClick.HasDelegate)
                        .AddIf("m-icon--dense", () => Dense)
                        .AddIf("m-icon--left", () => Left)
                        .AddIf("m-icon--disabled", () => Disabled)
                        .AddIf("m-icon--right", () => Right)
                        .AddTheme(IsDark, IndependentTheme)
                        .AddTextColor(Color, () => IsActive);
                }, styleBuilder =>
                {
                    styleBuilder = styleBuilder.AddTextColor(Color, () => IsActive);

                    var fontSize = GetSize();
                    styleBuilder.AddIf($"font-size:{fontSize}", () => fontSize != null);
                }).Apply("svg",
                    cssBuilder => { cssBuilder.Add("m-icon__svg"); },
                    styleBuilder =>
                    {
                        var size = GetSize();
                        if (size != null)
                        {
                            styleBuilder
                                .Add($"font-size:{size}")
                                .Add($"height:{size}")
                                .Add($"width:{size}");
                        }
                    });
        }
    }
}
