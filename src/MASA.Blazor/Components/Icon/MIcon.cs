using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MASA.Blazor
{
    public partial class MIcon : BIcon, IThemeable, IIcon, ISizeable
    {
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

        public IDictionary<string, object> Attrs => Attributes;

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

        private string GetSize()
        {
            return _sizer.GetSize() ?? Size?.ToUnit();
        }

        private Sizer _sizer;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _sizer = new Sizer(this);
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
                        .AddTextColor(Color, () => IsActive)
                        .AddFirstIf((() => Icon, () => _arrFa5Prefix.Any(prefix => Icon.StartsWith(prefix))),
                            (() => $"mdi {Icon}", () => Icon.StartsWith("mdi-")),
                            (() => $"material-icons", () => !string.IsNullOrWhiteSpace(NewChildren)));
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