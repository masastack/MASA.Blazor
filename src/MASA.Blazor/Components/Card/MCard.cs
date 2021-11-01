using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public partial class MCard : BCard, IThemeable
    {
        /// <summary>
        /// Applies a large border radius on the top left and bottom right of the card.
        /// </summary>
        [Parameter]
        public RenderFragment ProgressContent { get; set; }

        /// <summary>
        /// Removes the card’s elevation.
        /// </summary>
        [Parameter]
        public bool Flat { get; set; }

        /// <summary>
        /// Will apply an elevation of 4dp when hovered (default 2dp)
        /// </summary>
        [Parameter]
        public bool Hover { get; set; }

        /// <summary>
        /// Designates that the component is a link. This is automatic when using the href or to prop.
        /// </summary>
        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Specifies a higher default elevation (8dp). 
        /// </summary>
        [Parameter]
        public bool ExactPath { get; set; }

        [Parameter]
        public string ExactActiveClass { get; set; }

        [Parameter]
        public object Href { get; set; }

        [Parameter]
        public object To { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public bool Replace { get; set; }

        /// <summary>
        /// Designates an elevation applied to the component between 0 and 24.
        /// </summary>
        [Parameter]
        public object Ripple { get; set; }

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
            var prefix = "m-card";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-card")
                        .AddElevation(Elevation)
                        .AddTheme(IsDark)
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddIf($"{prefix}--flat", () => Flat)
                        .AddIf($"{prefix}--hover", () => Hover)
                        .AddIf($"{prefix}--link", () => Link || OnClick.HasDelegate)
                        .AddIf($"{prefix}--loading", () => Loading)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--raised", () => Raised)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddIf($"background:url(\"{Img}\") center center / cover no-repeat", () => !string.IsNullOrWhiteSpace(Img));
                });
        }
    }
}