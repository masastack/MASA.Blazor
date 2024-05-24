using Masa.Blazor.Components.Tabs;

namespace Masa.Blazor
{
    public partial class MTabs : MasaComponentBase, IThemeable
    {
        [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

        [Inject] protected IResizeJSModule ResizeJSModule { get; set; } = null!;

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        [Parameter] public string? ActiveClass { get; set; }

        [Parameter] public bool AlignWithTitle { get; set; }

        [Parameter] public string? BackgroundColor { get; set; }

        [Parameter] public bool CenterActive { get; set; }

        [Parameter] public bool Centered { get; set; }

        [Parameter] public bool FixedTabs { get; set; }

        [Parameter] public bool Grow { get; set; }

        [Parameter] public StringNumber? Height { get; set; }

        [Parameter] public bool IconsAndText { get; set; }

        // [Parameter]
        // public StringNumber? MobileBreakpoint { get; set; }

        [Parameter] public bool Right { get; set; }

        [Parameter] public virtual bool HideSlider { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public bool Optional { get; set; }

        [Parameter] public string? SliderColor { get; set; }

        [Parameter] [MasaApiParameter(2)] public StringNumber SliderSize { get; set; } = 2;

        [Parameter] public StringNumber? Value { get; set; }

        private EventCallback<StringNumber>? _valueChanged;

        [Parameter]
        public EventCallback<StringNumber> ValueChanged
        {
            get
            {
                if (_valueChanged.HasValue)
                {
                    return _valueChanged.Value;
                }

                return EventCallback.Factory.Create<StringNumber>(this, (v) => Value = v);
            }
            set => _valueChanged = value;
        }

        [Parameter] public bool Vertical { get; set; }

        [Parameter] public string? NextIcon { get; set; }

        [Parameter] public string? PrevIcon { get; set; }

        [Parameter] public StringBoolean? ShowArrows { get; set; }

        [Parameter] public bool Routable { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }
        private StringNumber? _prevValue;
        private int _registeredTabItemsIndex;
        private bool _callSliderOnAfterRender;
        private CancellationTokenSource? _callSliderCts;

        private List<ITabItem> TabItems { get; set; } = new();

        private object? TabsBarRef { get; set; }

        protected (StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width)
            Slider { get; set; }

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

                return CascadingIsDark;
            }
        }

        protected bool RTL => MasaBlazor.RTL;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.RTLChanged += MasaBlazorOnRTLChanged;
        }

        private void MasaBlazorOnRTLChanged(object? sender, EventArgs e)
        {
            InvokeAsync(CallSlider);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await ResizeJSModule.ObserverAsync(Ref, OnResize);
                await IntersectJSModule.ObserverAsync(Ref, OnIntersectAsync);
                _callSliderOnAfterRender = true;
            }
            else if (_prevValue != Value)
            {
                _prevValue = Value;
                _callSliderOnAfterRender = true;
            }

            if (_callSliderOnAfterRender)
            {
                _callSliderOnAfterRender = false;
                await CallSlider();
            }
        }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

        private Block _block = new("m-tabs");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(AlignWithTitle)
                .And(Centered)
                .And(FixedTabs)
                .And(Grow)
                .And(IconsAndText)
                .And(Right)
                .And(Vertical)
                .AddTheme(IsDark, IndependentTheme)
                .GenerateCssClasses();
        }

        private async Task OnIntersectAsync(IntersectEventArgs e)
        {
            if (e.IsIntersecting)
            {
                await InvokeAsync(CallSlider);
            }
        }

        public bool IsReversed => RTL && Vertical;

        public MSlideGroup? Instance => TabsBarRef as MSlideGroup;

        public void RegisterTabItem(ITabItem tabItem)
        {
            tabItem.Value ??= _registeredTabItemsIndex++;

            if (TabItems.Any(item => item.Value != null && item.Value.Equals(tabItem.Value))) return;

            TabItems.Add(tabItem);
        }

        public void UnregisterTabItem(ITabItem tabItem)
        {
            TabItems.Remove(tabItem);
        }

        /// <summary>
        /// Re-render slider immediately. For the case of deleting tabs,
        /// it is recommended to use <see cref="CallSliderAfterRender"/>.
        /// </summary>
        [MasaApiPublicMethod]
        public async Task CallSlider()
        {
            if (HideSlider) return;

            _callSliderCts?.Cancel();
            _callSliderCts = new();

            try
            {
                await Task.Delay(16, _callSliderCts.Token);

                var item = Instance?.Items?.FirstOrDefault(item => item.Value == Instance.Value);
                if (item?.Ref.Context == null)
                {
                    Slider = (0, 0, 0, 0, 0);
                }
                else
                {
                    var el = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, item.Ref);
                    var height = !Vertical ? SliderSize.TryGetNumber().number : el.ScrollHeight;
                    var left = Vertical ? 0 : el.OffsetLeft;
                    var right = Vertical ? 0 : el.OffsetLeft + el.OffsetWidth;
                    var top = el.OffsetTop;
                    var width = Vertical
                        ? SliderSize.TryGetNumber().number
                        : el.ClientWidth; // REVIEW: el.ScrollWidth was used in Vuetify2

                    Slider = (height, left, right, top, width);
                }

                StateHasChanged();
            }
            catch (TaskCanceledException)
            {
                // ignored
            }
        }

        private async Task OnResize()
        {
            if (IsDisposed)
            {
                return;
            }

            await CallSlider();
        }

        /// <summary>
        /// Re-render slider in <see cref="OnAfterRenderAsync"/>
        /// </summary>
        [MasaApiPublicMethod]
        public void CallSliderAfterRender()
        {
            _callSliderOnAfterRender = true;
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            MasaBlazor.RTLChanged -= MasaBlazorOnRTLChanged;
            await IntersectJSModule.UnobserveAsync(Ref);
            await ResizeJSModule.UnobserveAsync(Ref);
        }
    }
}