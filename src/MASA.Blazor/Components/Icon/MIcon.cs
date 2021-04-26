using BlazorComponent;
using BlazorComponent.Components.Core.CssProcess;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MIcon : BIcon
    {
        private const string XSMALL= "12px";
        private const string SMALL = "16px";
        private const string DENSE = "20px";
        private const string LARGE = "36px";
        private const string XLARGE = "40px";

        [Parameter]
        public bool Dark { get; set; }

        /// <summary>
        /// 20px
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// TODO: Disable the input
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// 36px
        /// </summary>
        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        /// <summary>
        /// 16px
        /// </summary>
        [Parameter]
        public bool Small { get; set; }

        /// <summary>
        /// TODO: Specifies a custom tag to be used
        /// </summary>
        [Parameter]
        public string Tag { get; set; } = "i";

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

        protected override void SetComponentClass()
        {
            CssBuilder
                .Add("m-icon")
                .AddIf("m-icon--link", () => Click.HasDelegate)
                .AddIf("m-icon--dense", () => Dense)
                .AddIf("m-icon--left", () => Left)
                .AddIf("m-icon--right", () => Right)
                .AddTheme(Dark);

            // TODO: 能否拿到属性排列顺序，最后一个优先级最高
            StyleBuilder
                .AddFirstIf(
                    (() => Size.Value.Match(
                         str => $"font-size: {str}",
                         num => $"font-size: {num}px"),
                       () => Size.HasValue),
                    (() => $"font-size: {XLARGE}", () => XLarge),
                    (() => $"font-size: {LARGE}", () => Large),
                    (() => $"font-size: {DENSE}", () => Dense),
                    (() => $"font-size: {SMALL}", () => Small),
                    (() => $"font-size: {XSMALL}", () => XSmall)
                );

            // 渲染颜色和变体
            var (color_class, color_style) = ColorHelper.ToCss(Color);
            CssBuilder.Add(color_class);
            StyleBuilder.Add(color_style);
        }
    }
}
