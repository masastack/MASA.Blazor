using BlazorComponent;
using Microsoft.AspNetCore.Components;

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

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public string Img { get; set; }
        /// <summary>
        /// Designates an elevation applied to the component between 0 and 24.
        /// </summary>
        [Parameter]
        public StringNumber Elevation { get; set; }

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

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;

        [Parameter]
        public RenderFragment ProgressContent { get; set; }

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public string ActiveClass {get;set;}

        [Parameter]
        public bool Append {get;set;}

        [Parameter]
        public bool? Exact {get;set;}

        [Parameter]
        public bool ExactPath {get;set;}

        public string ExactActiveClass {get;set;}

        public object Href {get;set;}

        [Parameter]
        public object To {get;set;}

        public bool Nuxt {get;set;}

        [Parameter]
        public bool Replace {get;set;}

        [Parameter]
        public object Ripple {get;set;}

        [Parameter]
        public string Target {get;set;}

        public bool HasClick => OnClick.HasDelegate;

        protected override void SetComponentClass() => (this as IMCard).SetCardComponentClass();
    }
}