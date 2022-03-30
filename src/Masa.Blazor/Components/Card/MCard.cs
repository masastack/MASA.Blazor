using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MCard : MSheet, ICard, IRoutable, ISheet, ILoadable
    {
        private IRoutable _router;

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
        public string Img { get; set; }

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 4;

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        public bool IsClickable => _router.IsClickable;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();

            Attributes["ripple"] = Ripple && IsClickable;
            Attributes["onclick"] = OnClick;
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder.Add("m-card")
                        .AddIf("m-card--flat", () => Flat)
                        .AddIf("m-card--hover", () => Hover)
                        .AddIf("m-card--link", () => IsClickable)
                        .AddIf("m-card--loading", () => Loading == true)
                        .AddIf("m-card--disabled", () => Disabled)
                        .AddIf("m-card--raised", () => Raised);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"background:url(\"{Img}\") center center / cover no-repeat", () => string.IsNullOrWhiteSpace(Img) == false);
                })
                .Apply("progress", cssBuilder => { cssBuilder.Add("m-card__progress"); });

            AbstractProvider.Merge(typeof(BSheetBody<>), typeof(BCardBody<ICard>))
                .Apply(typeof(BCardProgress<>), typeof(BCardProgress<ICard>))
                .ApplyLoadable(Loading, Color, LoaderHeight);
        }
    }
}