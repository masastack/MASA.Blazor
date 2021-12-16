using System.ComponentModel;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MNavigationDrawer : BNavigationDrawer, INavigationDrawer
    {
        private StringNumber _height;

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

        /// <summary>
        /// Applies position: absolute to the component.
        /// </summary>
        [Parameter]
        public bool Absolute { get; set; }

        /// <summary>
        /// Expands from the bottom of the screen on mobile devices
        /// </summary>
        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Clipped { get; set; }

        [Parameter]
        public bool DisableResizeWatcher { get; set; }

        [Parameter]
        public bool DisableRouteWatcher { get; set; }

        [Parameter]
        public bool Touchless { get; set; }

        /// <summary>
        /// Designates the width assigned when the mini prop is turned on
        /// </summary>
        [Parameter]
        public StringNumber MiniVariantWidth { get; set; } = 56;

        /// <summary>
        /// A floating drawer has no visible container (no border-right)
        /// </summary>
        [Parameter]
        public bool Floating { get; set; }

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
        /// Places the navigation drawer on the right
        /// </summary>
        [Parameter]
        public bool Right { get; set; }

        /// <summary>
        /// Sets the height of the navigation drawer
        /// </summary>
        [Parameter]
        public StringNumber Height
        {
            get
            {
                if (_height == null)
                {
                    return App ? "100vh" : "100%";
                }

                return _height;
            }
            set { _height = value; }
        }

        private StringNumber _top;

        [Parameter]
        public StringNumber Top
        {
            get
            {
                if (_top == null)
                {
                    return !IsBottom ? "0" : "auto";
                }

                return _top;
            }
            set { _top = value; }
        }

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

        [Parameter]
        public RenderFragment AppendContent { get; set; }

        [Parameter]
        public RenderFragment PrependContent { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        [Parameter]
        public EventCallback<bool> MiniVariantChanged { get; set; }

        protected BlazorComponent.Web.Element Element = null;

        protected bool IsMiniVariant => (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !IsMouseover);

        protected bool IsBottom => Bottom && IsMobile;

        protected StringNumber ComputedTop
        {
            get
            {
                if (!HasApp)
                {
                    return 0;
                }

                var computedTop = GlobalConfig.Application.Bar;
                computedTop += Clipped ? GlobalConfig.Application.Top : 0;

                return computedTop;
            }
        }

        protected int ZIndex { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-navigation-drawer";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-navigation-drawer")
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--clipped", () => Clipped)
                        .AddIf($"{prefix}--close", () => !IsActive)
                        .AddIf($"{prefix}--fixed", () => !Absolute && (App || Fixed))
                        .AddIf($"{prefix}--floating", () => Floating)
                        .AddIf($"{prefix}--is-mobile", () => IsMobile)
                        .AddIf($"{prefix}--is-mouseover", () => IsMouseover)
                        .AddIf($"{prefix}--mini-variant", () => IsMiniVariant)
                        .AddIf($"{prefix}--custom-mini-variant", () => MiniVariantWidth.IsT1 && MiniVariantWidth.AsT1 != 56)
                        .AddIf($"{prefix}--open", () => IsActive)
                        .AddIf($"{prefix}--open-on-hover", () => ExpandOnHover)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--temporary", () => Temporary) //
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    var translate = IsBottom ? "translateY" : "translateX";
                    styleBuilder
                        .AddHeight(Height)
                        .Add(() => $"top:{(!IsBottom ? ComputedTop.ToUnit() : "auto")}")
                        .AddIf(() => $"max-height:calc(100% - {ComputedMaxHeight.ToUnit()})", () => ComputedMaxHeight != null)
                        .AddIf(() => $"transform:{translate}({Transform}%)", () => Transform != null)
                        .Add($"width:{(IsMiniVariant ? MiniVariantWidth.ToUnit() : Width.ToUnit())}")
                        .AddBackgroundColor(Color);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content");
                })
                .Apply("border", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__border");
                })
                .Apply("prepend", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__prepend");
                })
                .Apply("append", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__append");
                })
                .Apply("image", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__image");
                });

            Attributes.Add("data-booted", "true");

            AbstractProvider
                .ApplyNavigationDrawerDefault()
                .Apply(typeof(IImage), typeof(MImage), attrs =>
                {
                    attrs[nameof(MImage.Src)] = Src;
                    attrs[nameof(MImage.Height)] = (StringNumber)"100%";
                    attrs[nameof(MImage.Width)] = (StringNumber)"100%";
                    attrs[nameof(MImage.Dark)] = Dark;
                    attrs[nameof(MImage.Light)] = Light;
                })
                .Apply<BOverlay, MOverlay>(attrs =>
                {
                    attrs[nameof(MOverlay.ZIndex)] = ZIndex;
                    attrs[nameof(MOverlay.Absolute)] = !Fixed;
                    attrs[nameof(MOverlay.Value)] = InternalShowOverlay;
                });
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateApplicationAsync();
        }

        protected async Task UpdateApplicationAsync()
        {
            var val = (!IsActive || IsMobile || Temporary)
                ? 0
                : (ComputedWidth().ToDouble() <= 0 ? await GetClientWidthAsync() : ComputedWidth().ToDouble());

            if (Right)
                GlobalConfig.Application.Right = val;
            else
                GlobalConfig.Application.Left = val;
        }

        private async Task<double> GetClientWidthAsync()
        {
            var element = await JsInvokeAsync<BlazorComponent.Web.Element>(
                   JsInteropConstants.GetDomInfo, Ref);
            return element.ClientWidth;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (_valueChangedToTrue)
            {
                ZIndex = await ActiveZIndex();
                _valueChangedToTrue = false;
            }
        }

        protected StringNumber ComputedWidth() => IsMiniVariant ? MiniVariantWidth : Width;

        protected StringNumber ComputedMaxHeight
        {
            get
            {
                if (!HasApp) return null;

                var computedMaxHeight = GlobalConfig.Application.Bottom + GlobalConfig.Application.Footer + GlobalConfig.Application.Bar;

                if (!Clipped) return computedMaxHeight;

                return computedMaxHeight + GlobalConfig.Application.Top;
            }
        }

        protected bool HasApp => App && (!IsMobile && !Temporary);

        public override async Task Click(MouseEventArgs e)
        {
            if (MiniVariant)
            {
                MiniVariant = false;
                await MiniVariantChanged.InvokeAsync(_miniVariant);
            }
        }

        private Task<int> ActiveZIndex() => JsInvokeAsync<int>(JsInteropConstants.GetZIndex, Ref);

        protected override void Dispose(bool disposing)
        {
            RemoveApplication();
        }

        private void RemoveApplication()
        {
            if (Right)
                GlobalConfig.Application.Right = 0;
            else
                GlobalConfig.Application.Left = 0;
        }
    }
}