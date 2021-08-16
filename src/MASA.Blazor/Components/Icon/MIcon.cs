using BlazorComponent;
using Microsoft.AspNetCore.Components;

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
        /// 20px
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

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


        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

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
                        .AddTextColor(Color, () => IsActive);
                }, styleBuilder =>
                {
                    // TODO: 能否拿到属性排列顺序，最后一个优先级最高
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
                });
        }
    }
}
