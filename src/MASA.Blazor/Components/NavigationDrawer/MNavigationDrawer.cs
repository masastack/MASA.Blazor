using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;


namespace MASA.Blazor
{
    using StringNumber = OneOf<string, int>;

    public partial class MNavigationDrawer : BNavigationDrawer
    {
        private StringNumber _height;

        [Parameter]
        public bool Dark { get; set; }

        /// <summary>
        /// Applies position: absolute to the component.
        /// </summary>
        [Parameter]
        public bool Absolute { get; set; }

        /// <summary>
        /// Expands from the bottom of the screen on mobile devices
        /// </summary>
        [Parameter]
        public bool IsBottom { get; set; }

        [Parameter]
        public bool Clipped { get; set; }

        [Parameter]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Designates the component as part of the application layout. Used for dynamically adjusting content sizing.
        /// Components using this prop should reside outside of v-main component to function properly.
        /// You can find more information about layouts on the application page. 
        /// Note: this prop automatically applies position: fixed to the layout element.
        /// You can overwrite this functionality by using the absolute prop
        /// </summary>
        [Parameter]
        public bool App { get; set; }

        /// <summary>
        /// Applies position: fixed to the component.
        /// </summary>
        [Parameter]
        public bool Fixed { get; set; }

        /// <summary>
        /// A floating drawer has no visible container (no border-right)
        /// </summary>
        [Parameter]
        public bool Floating { get; set; }

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public bool Mobile { get; }

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public bool IsMouseover { get; }

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public bool IsMiniVariant { get; }

        /// <summary>
        /// Designates the width assigned when the mini prop is turned on
        /// </summary>
        [Parameter]
        public StringNumber MiniVariantWidth { get; set; } = 56;

        /// <summary>
        /// Collapses the drawer to a mini-variant until hovering with the mouse
        /// </summary>
        [Parameter]
        public bool ExpandOnHover { get; set; }

        /// <summary>
        /// Places the navigation drawer on the right
        /// </summary>
        [Parameter]
        public bool Right { get; set; }

        /// <summary>
        /// A temporary drawer sits above its application and uses a scrim (overlay) to darken the background
        /// </summary>
        [Parameter]
        public bool Temporary { get; set; }

        /// <summary>
        /// Sets the height of the navigation drawer
        /// </summary>
        [Parameter]
        public StringNumber Height
        {
            get
            {
                if (_height.Value == default)
                {
                    return App ? "100vh" : "100%";
                }

                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public string Top => !IsBottom ? "0" : "auto";

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public int? MaxHeight { get; }

        public int? Transform
        {
            get
            {
                if (IsActive)
                {
                    return 0;
                }

                if (IsBottom)
                {
                    return 100;
                }

                return Right ? 100 : -100;
            }
        }

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public StringNumber Width { get; set; } = "256px";

        private bool _drawer;

        [Parameter]
        public bool Drawer
        {
            get
            {
                return _drawer;
            }
            set
            {
                _drawer = value;
                IsActive = _drawer;
                StateHasChanged();
            }
        }

        public override List<BListItem> ListItems { get; set; } = new List<BListItem>();

        protected override void OnInitialized()
        {
            if (Temporary)
            {
                IsActive = false;
            }

            base.OnInitialized();
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-navigation-drawer";

            CssProvider
                .AsProvider<BNavigationDrawer>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-navigation-drawer")
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--bottom", () => IsBottom)
                        .AddIf($"{prefix}--clipped", () => Clipped)
                        .AddIf($"{prefix}--close", () => !IsActive)
                        .AddIf($"{prefix}--fixed", () => !Absolute && (App || Fixed))
                        .AddIf($"{prefix}--floating", () => Floating)
                        .AddIf($"{prefix}--is-mobile", () => Mobile)
                        .AddIf($"{prefix}--is-mouseover", () => IsMouseover)
                        .AddIf($"{prefix}--mini-variant", () => IsMiniVariant)
                        .AddIf($"{prefix}--custom-mini-variant", () => MiniVariantWidth.IsT1 && MiniVariantWidth.AsT1 != 56)
                        .AddIf($"{prefix}--open", () => IsActive)
                        .AddIf($"{prefix}--open-on-hover", () => ExpandOnHover)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--temporary", () => Temporary)
                        .AddTheme(Dark);
                },styleBuilder=> {
                    var translate = IsBottom ? "translateY" : "translateX";
                    styleBuilder
                        .Add($"height:{Height.Value}")
                        .Add($"top:{Top}")
                        .AddIf(() => $"maxHeight:calc(100% - {MaxHeight})", () => MaxHeight != null)
                        .AddIf(() => $"transform:{translate}({Transform}%)", () => Transform != null)
                        .Add($"width:{(IsMiniVariant ? MiniVariantWidth.Value : Width.Value)}");
                })
                .Apply("content",cssBuilder=> {
                    cssBuilder
                        .Add($"{prefix}__content");
                })
                .Apply("border", cssBuilder => {
                    cssBuilder
                        .Add($"{prefix}__border");
                });

            Attributes.Add("data-booted", true);
        }

        public override void Select(BListItem selectItem)
        {
            foreach (var item in ListItems)
            {
                if (item is MListItem mItem)
                {
                    if (mItem != selectItem)
                    {
                        mItem.Deactive();
                    }
                }
            }
        }
    }
}
