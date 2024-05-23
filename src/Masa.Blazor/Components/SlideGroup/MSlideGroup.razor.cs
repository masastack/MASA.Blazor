namespace Masa.Blazor
{
    public partial class MSlideGroup : MItemGroup
    {
        public MSlideGroup() : base(GroupType.SlideGroup)
        {
        }

        protected MSlideGroup(GroupType groupType) : base(groupType)
        {
        }

        [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Inject] protected IResizeJSModule ResizeJSModule { get; set; } = null!;

        [Parameter] public bool CenterActive { get; set; }

        [Parameter] public StringBoolean? ShowArrows { get; set; }

        [Parameter] public string? NextIcon { get; set; }

        [Parameter] public RenderFragment? NextContent { get; set; }

        [Parameter] public string? PrevIcon { get; set; }

        [Parameter] public RenderFragment? PrevContent { get; set; }

        private int _prevItemsLength;
        private StringNumber? _prevInternalValue;
        private bool _prevIsOverflowing;
        private CancellationTokenSource? _cts;

        protected bool RTL => MasaBlazor.RTL;

        protected bool IsMobile { get; set; }

        protected ElementReference WrapperRef { get; set; }

        protected ElementReference ContentRef { get; set; }

        protected double WrapperWidth { get; set; }

        protected double ContentWidth { get; set; }

        protected double ScrollOffset
        {
            get => GetValue<double>();
            set => SetValue(value);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
            IsMobile = MasaBlazor.Breakpoint.Mobile;
        }

        private async void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
        {
            if (!e.MobileChanged) return;

            IsMobile = MasaBlazor.Breakpoint.Mobile;
            await InvokeStateHasChangedAsync();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ActiveClass ??= "m-slide-item--active";
            NextIcon ??= "$next";
            PrevIcon ??= "$prev";

            if (MasaBlazor.RTL)
            {
                (NextIcon, PrevIcon) = (PrevIcon, NextIcon);
            }
        }

        private Block _block = new("m-slide-group");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                _block.Modifier(IsOverflowing)
                    .And(HasAffixes)
                    .GenerateCssClasses()
            );
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<double>(nameof(ScrollOffset), OnScrollOffsetChanged);
        }

        private string GetContentClass()
        {
            return string.Join(" ", BuildContentClass());
        }

        protected virtual IEnumerable<string> BuildContentClass()
        {
            yield return _block.Element("content").Build();
        }

        private async void OnScrollOffsetChanged(double val)
        {
            if (RTL)
            {
                val = -val;
            }

            var scroll = val <= 0
                ? Bias(-val)
                : val > ContentWidth - WrapperWidth
                    ? -(ContentWidth - WrapperWidth) + Bias(ContentWidth - WrapperWidth - val)
                    : -val;

            if (RTL)
            {
                scroll = -scroll;
            }

            if (ContentRef.Context != null)
            {
                await Js.InvokeVoidAsync(JsInteropConstants.SetStyle, ContentRef, "transform",
                    $"translateX({scroll}px)");
            }
        }

        private double Bias(double val)
        {
            var c = 0.501;
            var x = Math.Abs(val);
            return Math.Sign(val) * (x / ((1 / c - 2) * (1 - x) + 1));
        }

        private bool _firstRendered;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _firstRendered = true;
                await ResizeJSModule.ObserverAsync(Ref, OnResize);
            }

            if (_firstRendered)
            {
                var needSetWidths = false;
                StringNumber? value = null;

                if (_prevItemsLength != Items.Count)
                {
                    _prevItemsLength = Items.Count;
                    value = Value;
                    needSetWidths = true;
                }

                if (_prevInternalValue != InternalValue)
                {
                    _prevInternalValue = InternalValue;
                    value = InternalValue;
                    needSetWidths = true;
                }

                if (_prevIsOverflowing != IsOverflowing)
                {
                    _prevIsOverflowing = IsOverflowing;
                    // Make sure the value of WrapperWidth is after IsOverflowing takes effect.
                    StateHasChanged();

                    value = Value;
                    needSetWidths = true;
                }

                if (needSetWidths)
                {
                    await SetWidths(value);
                }
            }
        }

        public async Task SetWidths(StringNumber? selectedValue = null)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            await RunTaskInMicrosecondsAsync(async () =>
            {
                (WrapperWidth, ContentWidth) = await GetWidths();

                // The first time IsOverflowing is true, WrapperWidth will become smaller
                // because the left and right arrows will display 
                IsOverflowing = WrapperWidth + 1 < ContentWidth;

                await ScrollToView(selectedValue);

                StateHasChanged();
            }, 16, _cts.Token);
        }

        private async Task<(double wrapper, double content)> GetWidths()
        {
            var wrapperElement =
                await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, WrapperRef);
            var contentElement =
                await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, ContentRef);
            return (wrapperElement?.ClientWidth ?? 0, contentElement?.ClientWidth ?? 0);
        }

        public bool IsOverflowing { get; protected set; }

        public bool HasAffixes
        {
            get
            {
                var hasAffixes = !IsMobile && (IsOverflowing || Math.Abs(ScrollOffset) > 0);

                if (ShowArrows is null) return hasAffixes;

                return ShowArrows.Match(
                    str =>
                    {
                        return str switch
                        {
                            "always" => true, // Always show arrows on desktop & mobile
                            "desktop" => !IsMobile, // Always show arrows on desktop
                            "mobile" => IsMobile ||
                                        (IsOverflowing ||
                                         Math.Abs(ScrollOffset) > 0), // Show arrows on mobile when overflowing.
                            _ => hasAffixes
                        };
                    },
                    @bool =>
                    {
                        return @bool switch
                        {
                            true => IsOverflowing || Math.Abs(ScrollOffset) > 0, // Always show on mobile
                            _ => hasAffixes
                        };
                    });
            }
        }

        public bool HasNext => HasAffixes && (ContentWidth > Math.Abs(ScrollOffset) + WrapperWidth);

        public bool HasPrev => HasAffixes && ScrollOffset != 0;

        private async Task HandleOnAffixClick(string direction)
        {
            await ScrollTo(direction);

            StateHasChanged();
        }

        protected async Task ScrollToView(StringNumber? selectedValue)
        {
            if (selectedValue == null && Items.Any())
            {
                var lastItemRef = Items[^1].Ref;
                if (lastItemRef.Context == null) return;

                var lastItemPosition =
                    await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, lastItemRef);
                var wrapperPosition =
                    await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, WrapperRef);

                if ((RTL && wrapperPosition.Right < lastItemPosition.Right) ||
                    (!RTL && wrapperPosition.Left > lastItemPosition.Left))
                {
                    await ScrollTo("prev");
                }
            }

            if (selectedValue == null) return;

            var selectedItem = Items.FirstOrDefault(item => item.Value == selectedValue);
            if (selectedItem?.Ref.Context == null) return;

            if (Items.FindIndex(u => u.Value == selectedValue) == 0 || (!CenterActive && !IsOverflowing))
            {
                ScrollOffset = 0;
            }
            else if (CenterActive)
            {
                ScrollOffset = await CalculateCenteredOffset(selectedItem.Ref, WrapperWidth, ContentWidth, RTL);
            }
            else if (IsOverflowing)
            {
                ScrollOffset =
                    await CalculateUpdatedOffset(selectedItem.Ref, WrapperWidth, ContentWidth, RTL, ScrollOffset);
            }
        }

        protected async Task<double> CalculateUpdatedOffset(ElementReference selected, double wrapperWidth,
            double contentWidth, bool rtl,
            double currentScrollOffset)
        {
            var selectedDomInfo =
                await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, selected);
            var clientWidth = selectedDomInfo.ClientWidth;
            var offsetLeft =
                rtl ? (contentWidth - selectedDomInfo.OffsetLeft - clientWidth) : selectedDomInfo.OffsetLeft;

            if (rtl)
            {
                currentScrollOffset = -currentScrollOffset;
            }

            var totalWidth = wrapperWidth + currentScrollOffset;
            var itemOffset = clientWidth + offsetLeft;
            var additionalOffset = clientWidth * 0.4;

            if (offsetLeft <= currentScrollOffset)
            {
                currentScrollOffset = Math.Max(offsetLeft - additionalOffset, 0);
            }
            else if (totalWidth <= itemOffset)
            {
                currentScrollOffset = Math.Min(currentScrollOffset - (totalWidth - itemOffset - additionalOffset),
                    contentWidth - wrapperWidth);
            }

            return rtl ? -currentScrollOffset : currentScrollOffset;
        }

        protected async Task<double> CalculateCenteredOffset(ElementReference selected, double wrapperWidth,
            double contentWidth, bool rtl)
        {
            var selectedDomInfo =
                await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, selected);
            var offsetLeft = selectedDomInfo.OffsetLeft;
            var clientWidth = selectedDomInfo.ClientWidth;

            if (rtl)
            {
                var offsetCentered = contentWidth - offsetLeft - clientWidth / 2 - wrapperWidth / 2;
                return -Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
            else
            {
                var offsetCentered = offsetLeft + clientWidth / 2 - wrapperWidth / 2;
                return Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
        }

        protected async Task ScrollTo(string direction)
        {
            (WrapperWidth, ContentWidth) = await GetWidths();
            ScrollOffset = CalculateNewOffset(direction, WrapperWidth, ContentWidth, RTL, ScrollOffset);
        }

        protected static double CalculateNewOffset(string direction, double wrapperWidth, double contentWidth, bool rtl,
            double currentScrollOffset)
        {
            var sign = rtl ? -1 : 1;

            var newAbsoluteOffset = sign * currentScrollOffset + (direction == "prev" ? -1 : 1) * wrapperWidth;

            return sign * Math.Max(Math.Min(newAbsoluteOffset, contentWidth - wrapperWidth), 0);
        }

        protected async Task OnResize()
        {
            if (IsDisposed)
            {
                return;
            }

            await SetWidths();
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
            await ResizeJSModule.UnobserveAsync(Ref);
        }
    }
}