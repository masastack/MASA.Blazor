using BlazorComponent;
using MASA.Blazor.Model;
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
        public bool Permanent { get; set; }

        [Parameter]
        public bool Stateless { get; set; }

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

        private bool _value = true;

        [Parameter]
        public bool Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
            }
        }

        public bool _isActive => Value;

        protected bool _isOverlay => Value && Temporary;

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

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
        /// This should be down in next version
        /// </summary>
        public bool Mobile { get; }

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
            set
            {
                _height = value;
            }
        }

        private StringNumber _top;

        [Parameter]
        public StringNumber Top
        {
            get
            {
                if (_top == null)
                {
                    return !_isBottom ? "0" : "auto";
                }

                return _top;
            }
            set
            {
                _top = value;
            }
        }

        public int? MaxHeight { get; }

        public int? Transform
        {
            get
            {
                if (_isActive)
                {
                    return 0;
                }

                if (_isBottom)
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
        public bool MiniVariant
        {
            get => _miniVariant;
            set
            {
                if (value == _miniVariant) return;
                _miniVariant = value;
            }
        }

        [Parameter]
        public EventCallback<bool> MiniVariantChanged { get; set; }

        protected BlazorComponent.Web.Element Element = null;

        protected bool _isMiniVariant =>
            (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !_isMouseover);

        protected bool _isBottom => Bottom && Mobile;

        protected override void OnInitialized()
        {
            if (Temporary)
            {
                Value = false;
                ValueChanged.InvokeAsync(Value);
            }

            base.OnInitialized();
        }

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
                        .AddIf($"{prefix}--close", () => !_isActive)
                        .AddIf($"{prefix}--fixed", () => !Absolute && (App || Fixed))
                        .AddIf($"{prefix}--floating", () => Floating)
                        .AddIf($"{prefix}--is-mobile", () => Mobile)
                        .AddIf($"{prefix}--is-mouseover", () => _isMouseover)
                        .AddIf($"{prefix}--mini-variant", () => _isMiniVariant)
                        .AddIf($"{prefix}--custom-mini-variant", () => MiniVariantWidth.IsT1 && MiniVariantWidth.AsT1 != 56)
                        .AddIf($"{prefix}--open", () => _isActive)
                        .AddIf($"{prefix}--open-on-hover", () => ExpandOnHover)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--temporary", () => Temporary)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    var translate = _isBottom ? "translateY" : "translateX";
                    styleBuilder
                        .AddHeight(Height)
                        .Add($"top:{(!_isBottom ? Top.ToUnit() : "auto")}")
                        .AddIf(() => $"maxHeight:calc(100% - {MaxHeight})", () => MaxHeight != null)
                        .AddIf(() => $"transform:{translate}({Transform}%)", () => Transform != null)
                        .Add($"width:{(_isMiniVariant ? MiniVariantWidth.ToUnit() : Width.ToUnit())}")
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
                .Apply(typeof(IImage), typeof(MImage), props =>
                {
                    props[nameof(MImage.Src)] = Src.Match(t0 => t0, t1 => t1.Src);
                    props[nameof(MImage.LazySrc)] = Src.IsT1 ? Src.AsT1.LazySrc : string.Empty;
                    props[nameof(MImage.Height)] = (StringNumber)"100%";
                    props[nameof(MImage.Width)] = (StringNumber)"100%";
                    props[nameof(MImage.Dark)] = Dark;
                    props[nameof(MImage.Light)] = Light;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.ZIndex)] = 7;
                    props[nameof(MOverlay.Absolute)] = true;
                    props[nameof(MOverlay.Value)] = _isOverlay;
                    props[nameof(MOverlay.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        Value = !Value;
                        await ValueChanged.InvokeAsync(Value);
                    });
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Element = await JsInvokeAsync<BlazorComponent.Web.Element>(
                    JsInteropConstants.GetDomInfo, Ref);
                UpdateApplication(Element);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected void UpdateApplication(BlazorComponent.Web.Element element)
        {
            var val = (!_isActive || IsMobile() || Temporary || element == null) ? 0 :
                (ComputedWidth().ToDouble() <= 0 ? element.ClientWidth : ComputedWidth().ToDouble());

            if (Right)
                GlobalConfig.Application.Right = val;
            else
                GlobalConfig.Application.Left = val;
        }

        protected bool IsMobile() =>
            !Stateless && !Permanent && Mobile;

        protected StringNumber ComputedWidth() =>
            _isMiniVariant ? MiniVariantWidth : Width;

        public override async Task Click(MouseEventArgs e)
        {
            if (MiniVariant)
            {
                MiniVariant = false;
                await MiniVariantChanged.InvokeAsync(_miniVariant);
            }

            Element = await JsInvokeAsync<BlazorComponent.Web.Element>(
                JsInteropConstants.GetDomInfo, Ref);

            UpdateApplication(Element);
        }
    }
}
