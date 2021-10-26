using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public partial class MCard : BCard, IMCard
    {
        /// <summary>
        /// Applies a large border radius on the top left and bottom right of the card.
        /// </summary>
        [Parameter]
        public bool Shaped { get; set; }

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

        /// <summary>
        /// Removes the ability to click or target the component.
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Specifies a higher default elevation (8dp). 
        /// </summary>
        [Parameter]
        public bool Raised { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public string Img { get; set; }

        /// <summary>
        /// Designates an elevation applied to the component between 0 and 24.
        /// </summary>
        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public bool Append { get; set; }

        [Parameter]
        public bool? Exact { get; set; }

        [Parameter]
        public bool ExactPath { get; set; }

        [Parameter]
        public string ExactActiveClass { get; set; }

        [Parameter]
        public object Href { get; set; }

        [Parameter]
        public object To { get; set; }

        [Parameter]
        public bool Nuxt { get; set; }

        [Parameter]
        public bool Replace { get; set; }

        [Parameter]
        public object Ripple { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        /// <summary>
        /// Removes elevation (box-shadow) and adds a thin border
        /// </summary>
        [Parameter]
        public bool Outlined { get; set; }

        /// <summary>
        /// Designates the border-radius applied to the component
        /// </summary>
        [Parameter]
        public StringBoolean Rounded { get; set; }

        /// <summary>
        /// Removes the component’s border-radius
        /// </summary>
        [Parameter]
        public bool Tile { get; set; }

        public bool HasClick => OnClick.HasDelegate;

        protected override void SetComponentClass()
        {
            CssProvider
              .Apply(cssBuilder =>
              {
                  cssBuilder.Add("m-card")
                            .AddRoutable(this)
                            .AddIf("m-card--flat", () => Flat)
                            .AddIf("m-card--hover", () => Hover)
                            .AddIf("m-card--flat", () => (this as IMRoutable).IsClickable(HasClick))
                            .AddIf("m-card--loading", () => Loading == true)
                            .AddIf("m-card--disabled", () => Disabled)
                            .AddIf("m-card--disabled", () => Raised)
                            .AddSheet(this);



              }, styleBuilder =>
              {
                  styleBuilder
                      .AddSheet(this)
                      .AddIf(() => $"background:url(\"{Img}\") center center / cover no-repeat",()=> string.IsNullOrWhiteSpace(Img) == false);
              });

            AbstractProvider.Apply(typeof(BCardProgress<>),typeof(MCardProgress));
        }
    }
}