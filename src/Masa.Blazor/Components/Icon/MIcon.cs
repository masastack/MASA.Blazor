﻿namespace Masa.Blazor
{
    public class MIcon : BIcon, ISizeable
    {
        /// <summary>
        /// Attention! End with a space
        /// </summary>
        private static string[] s_arrFa5Prefix = new[] { "fa ", "fad ", "fak ", "fab ", "fal ", "far ", "fas ", "mi" };

        private readonly Dictionary<string, string> _sizeMap = new()
        {
            { nameof(XSmall), "12px" },
            { nameof(Small), "16px" },
            { "Default", "24px" },
            { nameof(Medium), "28px" },
            { nameof(Large), "36px" },
            { nameof(XLarge), "40px" },
        };

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

        public IDictionary<string, object> Attrs => Attributes;

        public bool Medium => false;

        public string GetSize()
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
                        .AddIf("m-icon--link", () => OnClick.HasDelegate)
                        .AddIf("m-icon--dense", () => Dense)
                        .AddIf("m-icon--left", () => Left)
                        .AddIf("m-icon--disabled", () => Disabled)
                        .AddIf("m-icon--right", () => Right)
                        .AddTheme(IsDark)
                        .AddTextColor(Color, () => IsActive);

                    if (IconType == IconType.Webfont && Icon is not null)
                    {
                        cssBuilder.AddFirstIf(
                            (() => Icon, () => s_arrFa5Prefix.Any(prefix => Icon.StartsWith(prefix))),
                            (() => $"mdi {Icon}", () => Icon.StartsWith("mdi-"))
                        );
                    }
                    else if (IconType == IconType.WebfontNoPseudo)
                    {
                        cssBuilder.Add("material-icons");
                    }
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
