using BlazorComponent.Web;
using System.ComponentModel;

namespace Masa.Blazor
{
    public partial class MAppBar : MToolbar, IScrollable, IAsyncDisposable
    {
        [Inject]
        public MasaBlazor? MasaBlazor { get; set; }

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool ClippedLeft { get; set; }

        [Parameter]
        public bool ClippedRight { get; set; }

        [Parameter]
        public bool CollapseOnScroll { get; set; }

        [Parameter]
        [MassApiParameter("window")]
        public string ScrollTarget { get; set; } = "window";

        /// <summary>
        /// Elevates the app-bar when scrolling.
        /// </summary>
        [Parameter]
        public bool ElevateOnScroll { get; set; }

        [Parameter]
        public bool FadeImgOnScroll { get; set; }

        [Parameter]
        public bool HideOnScroll { get; set; }

        [Parameter]
        public bool InvertedScroll { get; set; }

        [Parameter]
        public bool ShrinkOnScroll { get; set; }

        [Parameter]
        public double ScrollThreshold { get; set; }

        [Parameter]
        public bool ScrollOffScreen { get; set; }

        [Parameter]
        [MassApiParameter(true)]
        public bool Value { get; set; } = true;

        private readonly string[] _applicationProperties =
        {
            "Left", "Bar", "Right"
        };

        private bool _isBooted;
        private Scroller? _scroller;

        public int? Transform { get; private set; } = 0;

        public bool CanScroll => InvertedScroll ||
                                 ElevateOnScroll ||
                                 HideOnScroll ||
                                 CollapseOnScroll ||
                                 _isBooted ||
                                 !Value;

        protected double ScrollRatio
        {
            get
            {
                var threshold = ComputedScrollThreshold;

                return Math.Max((threshold - (_scroller?.CurrentScroll ?? 0)) / threshold, 0);
            }
        }

        protected override StringNumber ComputedContentHeight
        {
            get
            {
                if (!ShrinkOnScroll)
                {
                    return base.ComputedContentHeight;
                }

                var min = Dense ? 48 : 56;
                var max = ComputedOriginalHeight;

                return min + (max - min) * ScrollRatio;
            }
        }

        protected StringNumber? ComputedFontSize
        {
            get
            {
                if (!IsProminent) return null;

                var min = 1.25;
                var max = 1.5;

                return min + (max - min) * ScrollRatio;
            }
        }

        protected double ComputedLeft
        {
            get
            {
                if (MasaBlazor == null) return 0;

                if (!App || ClippedLeft) return 0;

                return MasaBlazor.Application.Left;
            }
        }

        protected double ComputedMarginTop
        {
            get
            {
                if (MasaBlazor == null) return 0;

                if (!App) return 0;

                return MasaBlazor.Application.Bar;
            }
        }

        protected double? ComputedOpacity
        {
            get
            {
                if (!FadeImgOnScroll) return null;

                return ScrollRatio;
            }
        }

        protected double ComputedOriginalHeight
        {
            get
            {
                var height = NumberHelper.ParseInt(base.ComputedContentHeight.ToString());
                if (IsExtended) height += NumberHelper.ParseInt(ExtensionHeight?.ToString());

                return height;
            }
        }

        protected double ComputedRight
        {
            get
            {
                if (MasaBlazor == null) return 0;

                if (!App || ClippedRight) return 0;

                return MasaBlazor.Application.Right;
            }
        }

        protected double ComputedScrollThreshold
        {
            get
            {
                if (ScrollThreshold > 0) return ScrollThreshold;

                return ComputedOriginalHeight - (Dense ? 48 : 56);
            }
        }

        protected double ComputedTransform
        {
            get
            {
                if (_scroller == null) return 0;

                if (!CanScroll || (ElevateOnScroll && _scroller.CurrentScroll == 0 && _scroller.IsActive)) return 0;

                if (_scroller.IsActive) return 0;

                var scrollOffScreen = ScrollOffScreen ? ComputedHeight.ToDouble() : ComputedContentHeight.ToDouble();

                return Bottom ? scrollOffScreen : -scrollOffScreen;
            }
        }

        public bool HideShadow
        {
            get
            {
                if (_scroller == null) return false;

                if (ElevateOnScroll && IsExtended)
                {
                    return _scroller.CurrentScroll < ComputedScrollThreshold;
                }

                if (ElevateOnScroll)
                {
                    return _scroller.CurrentScroll == 0 || ComputedTransform < 0;
                }

                return (!IsExtended || ScrollOffScreen) && ComputedTransform != 0;
            }
        }

        protected override bool IsCollapsed
        {
            get
            {
                if (_scroller == null) return false;

                if (!CollapseOnScroll) return base.IsCollapsed;

                return _scroller.CurrentScroll > 0;
            }
        }

        protected override bool IsProminent => base.IsProminent || ShrinkOnScroll;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _scroller = new Scroller(this)
            {
                IsActive = Value
            };

            if (InvertedScroll)
            {
                _scroller.IsActive = false;
            }

            MasaBlazor!.Application.PropertyChanged += ApplicationPropertyChanged;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                _isBooted = true;
                Attributes["data-booted"] = "true";
                StateHasChanged();
            }
        }

        private void ApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_applicationProperties.Contains(e.PropertyName))
            {
                InvokeStateHasChanged();
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            if (InvertedScroll)
            {
                Transform = -ComputedHeight.ToInt32();
            }

            if (ShrinkOnScroll)
            {
                Dense = false;
                Flat = false;
                Prominent = true;
            }

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-app-bar")
                        .AddIf("m-app-bar--clipped", () => ClippedLeft || ClippedRight)
                        .AddIf("m-app-bar--fade-img-on-scroll", () => FadeImgOnScroll)
                        .AddIf("m-app-bar--elevate-on-scroll", () => ElevateOnScroll)
                        .AddIf("m-app-bar--fixed", () => !Absolute && (App || Fixed))
                        .AddIf("m-app-bar--hide-shadow", () => HideShadow)
                        .AddIf("m-app-bar--is-scrolled", () => _scroller is { CurrentScroll: > 0 })
                        .AddIf("m-app-bar--shrink-on-scroll", () => ShrinkOnScroll);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"font-size:{ComputedFontSize.ToUnit("rem")}", () => ComputedFontSize != null)
                        .Add(() => $"margin-top:{ComputedMarginTop}px")
                        .Add(() => $"transform:translateY({ComputedTransform}px)")
                        .Add(() => $"left:{ComputedLeft}px")
                        .Add(() => $"right:{ComputedRight}px");
                })
                .Merge("image",
                    _ => { },
                    style => { style.AddIf($"opacity: {ComputedOpacity}", () => ComputedOpacity.HasValue); });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (_scroller != null)
            {
                _scroller.ScrollThreshold = ScrollThreshold;
            }

            UpdateApplication();
        }

        private void UpdateApplication()
        {
            if (MasaBlazor == null) return;
            if (!App) return;

            var val = InvertedScroll ? 0 : ComputedHeight.ToDouble() + ComputedTransform;

            if (!Bottom)
                MasaBlazor.Application.Top = val;
            else
                MasaBlazor.Application.Bottom = val;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Js.AddHtmlElementEventListener(ScrollTarget, "scroll", async () =>
                {
                    if (!CanScroll) return;

                    await _scroller!.OnScroll(ThresholdMet);

                    StateHasChanged();
                }, false);
            }
        }

        protected void ThresholdMet(Scroller _)
        {
            if (_scroller == null) return;

            if (InvertedScroll)
            {
                _scroller.IsActive = _scroller.CurrentScroll > ComputedScrollThreshold;
                return;
            }

            if (HideOnScroll)
            {
                _scroller.IsActive = _scroller.IsScrollingUp || _scroller.CurrentScroll < ComputedScrollThreshold;
            }

            if (_scroller.CurrentThreshold < ComputedScrollThreshold) return;
            _scroller.SavedScroll = _scroller.CurrentScroll;
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                await Js.RemoveHtmlElementEventListener(ScrollTarget, "scroll");
            }
            catch (Exception)
            {
                // ignored
            }

            if (MasaBlazor == null) return;

            RemoveApplication();

            MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
        }

        private void RemoveApplication()
        {
            if (!App)
            {
                return;
            }

            if (!Bottom)
                MasaBlazor!.Application.Top = 0;
            else
                MasaBlazor!.Application.Bottom = 0;
        }
    }
}
