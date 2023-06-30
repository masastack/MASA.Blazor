namespace Masa.Blazor
{
    public class MIcon : BIcon, ISizeable
    {
        [Inject]
        private MasaBlazor? MasaBlazor { get; set; }

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

        public IDictionary<string, object?> Attrs => Attributes;

        public bool Medium => false;

        private string? _iconCss;

        protected override void InitIcon()
        {
            Icon? icon;

            if (Icon != null)
            {
                icon = Icon.IsAlias ? MasaBlazor!.Icons.Aliases.GetIconOrDefault(Icon.AsT0) : Icon;
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
                    icon = MasaBlazor!.Icons.Aliases.GetIconOrDefault(textContent);
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
            var set = MasaBlazor!.Icons.DefaultSet;

            var splits = cssIcon.Split(":");
            var icon = splits[0];

            if (splits.Length == 2)
            {
                set = splits[0] switch
                {
                    "mdi" => IconSet.MaterialDesignIcons,
                    "md"  => IconSet.MaterialDesign,
                    "fa"  => IconSet.FontAwesome,
                    "fa4" => IconSet.FontAwesome4,
                    _     => set
                };
                icon = splits[1];
            }

            string css;

            if (MasaBlazor.Icons.Name != null)
            {
                css = MasaBlazor.Icons.Aliases.Custom?.Invoke(icon) ?? icon;
                icon = null;
            }
            else
            {
                css = set switch
                {
                    IconSet.MaterialDesignIcons => $"mdi {icon}",
                    IconSet.MaterialDesign      => "material-icons",
                    _                           => icon
                };

                icon = set switch
                {
                    IconSet.MaterialDesign => icon,
                    _                      => null
                };
            }

            return (icon, css);
        }

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

        protected override void SetComponentClass()
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
                        .AddTheme(IsDark)
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
