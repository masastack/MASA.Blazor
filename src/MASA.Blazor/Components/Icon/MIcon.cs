using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Linq;
using System.Reflection;

namespace MASA.Blazor
{
    public partial class MIcon : BIcon, IThemeable
    {
        private const string XSMALL = "12px";
        private const string SMALL = "16px";
        private const string DENSE = "20px";
        private const string LARGE = "36px";
        private const string XLARGE = "40px";

        /// <summary>
        /// Attention! End with a space
        /// </summary>
        private static string[] _arrFa5Prefix = new string[] { "fa ", "fad ", "fak ", "fab ", "fal ", "far ", "fas " };

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

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

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

                return Themeable != null && Themeable.IsDark;
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BIcon>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-icon")
                        .AddIf("m-icon--link", () => OnClick.HasDelegate)
                        .AddIf("m-icon--dense", () => Dense)
                        .AddIf("m-icon--left", () => Left)
                        .AddIf("m-icon--disabled", () => Disabled)
                        .AddIf("m-icon--right", () => Right)
                        .AddTheme(IsDark)
                        .AddTextColor(Color, () => IsActive)
                        .AddFirstIf((() => Icon, () => _arrFa5Prefix.Any(prefix => Icon.StartsWith(prefix))),
                        (() => $"mdi {Icon}", () => Icon.StartsWith("mdi-")),
                        (() => $"material-icons", () => !string.IsNullOrWhiteSpace(NewChildren)));
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color, () => IsActive)
                        .AddFirstIf(
                            (() => $"font-size: {Size.ToUnit()}", () => Size != null),
                            (() => $"font-size: {XLARGE}", () => XLarge),
                            (() => $"font-size: {LARGE}", () => Large),
                            (() => $"font-size: {DENSE}", () => Dense),
                            (() => $"font-size: {SMALL}", () => Small),
                            (() => $"font-size: {XSMALL}", () => XSmall)
                        );
                }).Apply("m-icon-svg", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-icon__svg");
                });

            AbstractProvider
                .Apply(typeof(BButtonIconSlot<>), typeof(BButtonIconSlot<MIcon>))
                .Apply(typeof(BFontIconSlot<>), typeof(BFontIconSlot<MIcon>))
                .Apply(typeof(BSvgIconSlot<>), typeof(BSvgIconSlot<MIcon>));

            SvgAttrs = new()
            {
                { "viewBox", "0 0 24 24" },
                { "role", "img" },
                { "aria-hidden", "true" }
            };
        }
    }
}
