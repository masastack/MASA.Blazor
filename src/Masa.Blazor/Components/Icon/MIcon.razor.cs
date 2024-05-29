using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MIcon : MasaComponentBase
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public bool If { get; set; } = true;

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public Icon? Icon { get; set; }

        [Parameter] public bool Left { get; set; }

        [Parameter] public bool Right { get; set; }

        [Parameter] public StringNumber? Size { get; set; }

        [Parameter] [MasaApiParameter("i")] public string? Tag { get; set; } = "i";

        [Parameter] public Dictionary<string, object>? SvgAttributes { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        [Parameter] public string? Color { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public bool OnClickPreventDefault { get; set; }

        [Parameter] public bool OnClickStopPropagation { get; set; }

        [Parameter] public bool OnMouseupPreventDefault { get; set; }

        [Parameter] public bool OnMouseupStopPropagation { get; set; }

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

        private const string svgPattern1 = @"^[mzlhvcsqta]\s*[-+.0-9][^mlhvzcsqta]+";
        private const string svgPattern2 = @"[\dz]$";

        private static readonly Dictionary<string, object> s_defaultSvgAttrs = new()
        {
            { "viewBox", "0 0 24 24" },
            { "role", "img" },
            { "aria-hidden", "true" }
        };

        private readonly Dictionary<string, string> _sizeMap = new()
        {
            { nameof(XSmall), "12px" },
            { nameof(Small), "16px" },
            { "Default", "24px" },
            { nameof(Medium), "28px" },
            { nameof(Large), "36px" },
            { nameof(XLarge), "40px" },
        };

        private bool _clickEventRegistered;

        private string? _iconCss;

        public bool Medium => false;

        /// <summary>
        /// Icon from ChildContent
        /// </summary>
        protected string? IconContent { get; set; }

        private Icon? ComputedIcon { get; set; }

        private Dictionary<string, object> SvgAttrs
        {
            get
            {
                if (SvgAttributes is null) return s_defaultSvgAttrs;

                var attrs = new Dictionary<string, object>(SvgAttributes);

                foreach (var (k, v) in s_defaultSvgAttrs)
                {
                    attrs.TryAdd(k, v);
                }

                return attrs;
            }
        }

        protected void InitIcon()
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

                if (string.IsNullOrWhiteSpace(textContent))
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

        private (string? icon, string? css) ResolveIcon(string cssIcon)
        {
            var defaultAliases = MasaBlazor.Icons.Aliases;

            var splits = cssIcon.Split(":");
            var icon = splits[0];

            if (splits.Length == 2)
            {
                defaultAliases = splits[0] switch
                {
                    "mdi" => DefaultIconAliases.MaterialDesignIcons,
                    "md" => DefaultIconAliases.MaterialDesign,
                    "fa6" => DefaultIconAliases.FontAwesome6,
                    "fa" => DefaultIconAliases.FontAwesome,
                    "fa4" => DefaultIconAliases.FontAwesome4,
                    _ => defaultAliases
                };
                icon = splits[1];
            }

            return (defaultAliases.ContentFormatter?.Invoke(icon), defaultAliases.CssFormatter?.Invoke(icon));
        }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            InitIcon();
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
            if (OnClick.HasDelegate)
            {
                Tag = "button";
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!_clickEventRegistered)
            {
                await TryRegisterClickEvent();
            }
        }

        protected override async Task OnElementReferenceChangedAsync()
        {
            await TryRegisterClickEvent();
        }

        public async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        private static Block _block = new("m-icon");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder.Add("link", OnClick.HasDelegate)
                .Add(Dense)
                .Add(Left)
                .Add(Disabled)
                .Add(Right)
                .AddTheme(IsDark, IndependentTheme)
                .AddTextColor(Color)
                .AddClass(_iconCss, ComputedIcon is { IsSvg: false })
                .Build();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            var builder = StyleBuilder.Create().AddTextColor(Color);
            var fontSize = GetSize();
            if (fontSize != null)
            {
                builder.Add("font-size", fontSize);
            }

            return builder.GenerateCssStyles();
        }

        private async Task TryRegisterClickEvent()
        {
            if (Ref.Context is not null && OnClick.HasDelegate)
            {
                _clickEventRegistered = true;

                await Js.AddHtmlElementEventListener<MouseEventArgs>(Ref, "click",
                    HandleOnClick, false,
                    new EventListenerExtras
                    {
                        PreventDefault = OnClickPreventDefault,
                        StopPropagation = OnClickStopPropagation
                    });
            }
        }

        protected static bool CheckIfSvg(string iconOrPath)
        {
            return RegexSvgPath(iconOrPath);
        }

        /// <summary>
        /// Check if the string is a valid svg path
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexSvgPath(string str)
        {
            var reg1 = new Regex(svgPattern1, RegexOptions.IgnoreCase);
            var reg2 = new Regex(svgPattern2, RegexOptions.IgnoreCase);
            return reg1.Match(str).Success && reg2.Match(str).Success && str.Length > 4;
        }
    }
}