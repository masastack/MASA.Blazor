using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MTooltip : BTooltip, ITooltip
    {
        private int _stackMinZIndex = 6;
        private Window _window = new();
        private Element _bodyRect = new();
        private Element _activatorRect = new();
        private Element _contentRect = new();
        private double _pageYOffset;
        private bool _isActive;
        private Guid _activatorId;

        protected bool HasWindow => _window != null;

        protected double OffsetLeft { get; set; }

        protected double OffsetTop { get; set; }

        public ElementReference ContentRef { get; set; }

        public Dictionary<string, object> ActivatorAttrs => new()
        {
            { "_activator_" + ActivatorId, true },
            { "role", "button" },
            { "aria-haspopup", "true" },
            { "aria-expanded", Value.ToString() }
        };

        public Guid ActivatorId
        {
            get
            {
                if (_activatorId == Guid.Empty)
                {
                    _activatorId = Guid.NewGuid();
                }

                return _activatorId;
            }
        }

        [Inject]
        public Document Document { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Attached { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [CascadingParameter(Name = "Fixed")]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Attach { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public int NudgeLeft { get; set; }

        [Parameter]
        public int NudgeRight { get; set; }

        [Parameter]
        public int NudgeTop { get; set; }

        [Parameter]
        public int NudgeBottom { get; set; }

        [Parameter]
        public bool OffsetOverflow { get; set; }

        [Parameter]
        public bool AllowOverflow { get; set; }

        [Parameter]
        public int ZIndex { get; set; }

        [Parameter]
        public bool Value
        {
            get => _isActive;
            set
            {
                if (value == _isActive) return;
                _isActive = value;
                ValueChanged.InvokeAsync(_isActive);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-tooltip";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--top", () => Top)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--attached", () => Attached);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf("menuable__content__active", () => Value)
                        .AddIf($"{prefix}__content--fixed", () => Fixed)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddMaxWidth(MaxWidth)
                        .AddMinWidth(MinWidth)
                        .Add(() => $"left:{OffsetLeft}px")
                        .Add(() => $"top:{OffsetTop}px")
                        .Add($"opacity:{(Value ? 0.9 : 0)}")
                        .Add($"opacity:{(Value ? 0.9 : 0)}")
                        .Add($"z-index:{ZIndex}")
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .ApplyTooltipDefault();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, ".m-application");

                var activator = Document.QuerySelector($"[_activator_{ActivatorId}]");

                await activator.AddEventListenerAsync("click", EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                {
                    Value = !Value;
                }), false);

                await activator.AddEventListenerAsync("mouseenter", CreateEventCallback<MouseEventArgs>(HandleOnMouseEnter), false);

                await activator.AddEventListenerAsync("mouseleave", CreateEventCallback<MouseEventArgs>(HandleOnMouseLeave), false);

                _window = await JsInvokeAsync<Window>(JsInteropConstants.GetWindow);

                DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);

                await OptimizPosition();
            }
        }

        private async Task<int> ActiveZIndex()
        {
            int zIndex;
            if (!Value)
            {
                zIndex = await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef);
            }
            else
            {
                zIndex = await GetMaxZIndex() + 2;
            }

            return zIndex;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

            return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
        }

        protected async Task<int> CalculateZIndex()
        {
            if (ZIndex != 0)
            {
                return ZIndex;
            }
            else
            {
                return await JsInvokeAsync<int>(JsInteropConstants.GetMaxZIndex, _contentRect);
            }
        }

        private double CalculatedLeft()
        {
            if (_activatorRect == null) return 0;

            bool unknown = !Bottom && !Left && !Top && !Right;
            double left = 0;

            if (Top || Bottom || unknown)
            {
                left = (Attach ? _activatorRect.OffsetLeft : _activatorRect.AbsoluteLeft) +
                    (_activatorRect.OffsetWidth / 2) -
                    (_contentRect.OffsetWidth / 2);
            }
            else if (Left || Right)
            {
                left = (Attach ? _activatorRect.OffsetLeft : _activatorRect.AbsoluteLeft) +
                    (Right ? _activatorRect.OffsetWidth : -_contentRect.OffsetWidth) +
                    (Right ? 10 : -10);
            }

            if (NudgeLeft != 0)
                left -= NudgeLeft;

            if (NudgeRight != 0)
                left += NudgeRight;

            return CalcXOverflow(left, _contentRect.OffsetWidth);
        }

        protected double CalcXOverflow(double left, double menuWidth)
        {
            var xOverflow = left + menuWidth - _bodyRect.ClientWidth + 12;

            if ((!Left || Right) && xOverflow > 0)
            {
                left = Math.Max(left - xOverflow, 0);
            }
            else
            {
                left = Math.Max(left, 12);
            }

            return left + this.GetOffsetLeft();
        }

        private double GetOffsetLeft()
        {
            if (!HasWindow) return 0;

            return _window != default ? _window.PageXOffset : _bodyRect.ScrollLeft;
        }

        private double GetOffsetTop()
        {
            if (!HasWindow) return 0;

            return _window != null ? _window.PageYOffset : _bodyRect.ScrollTop;
        }

        private async Task<double> CalculatedTop()
        {
            if (_activatorRect == null) return 0;

            double top = 0;

            if (Top || Bottom)
            {
                top = (Attach ? _activatorRect.OffsetTop : _activatorRect.AbsoluteTop) +
                    (Bottom ? _activatorRect.OffsetHeight : -_contentRect.OffsetHeight) +
                    (Bottom ? 10 : -10);
            }
            else if (Left || Right)
            {
                top = (Attach ? _activatorRect.OffsetTop : _activatorRect.AbsoluteTop) +
                    (_activatorRect.OffsetHeight / 2) -
                    (_contentRect.OffsetHeight / 2);
            }

            if (NudgeTop != 0)
                top -= NudgeTop;

            if (NudgeBottom != 0)
                top += NudgeBottom;

            return await CalcYOverflow(top + _pageYOffset);
        }

        protected async Task<double> CalcYOverflow(double top)
        {
            //TODO：this will change
            var documentHeight = GetInnerHeight();
            var toTop = (await AbsoluteYOffset()) + documentHeight;

            var activator = _activatorRect;
            var contentHeight = _contentRect.ClientHeight;
            var totalHeight = top + contentHeight;
            var isOverflowing = toTop < totalHeight;

            return top < 12 ? 12 : top;
        }

        private async Task<double> AbsoluteYOffset()
        {
            double relativeYOffset = 0;
            var el = Document.QuerySelector(ContentRef);
            if (el != null)
            {
                var offsetRect = await el.OffsetParent.GetBoundingClientRectAsync();
                if (offsetRect != null)
                {
                    relativeYOffset = _pageYOffset + Math.Round(offsetRect.Top);
                }
            }

            return _pageYOffset - relativeYOffset;
        }

        private double GetInnerHeight()
        {
            if (!HasWindow) return 0;

            return _window.InnerHeight != default ? _window.InnerHeight : _bodyRect.ClientHeight;
        }

        protected async Task HandleOnMouseEnter(MouseEventArgs args)
        {
            await OptimizPosition();
            RunDelay("open");
        }

        protected async Task HandleOnMouseLeave(MouseEventArgs args)
        {
            await OptimizPosition();
            RunDelay("close");
        }

        private async void OnResize(Window window)
        {
            await OptimizPosition(window);
        }

        private async Task OptimizPosition(Window window = null)
        {
            _window = window ?? _window;

            _pageYOffset = GetOffsetTop();

            _bodyRect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, "document");

            var activator = Document.QuerySelector($"[_activator_{ActivatorId}]");

            _activatorRect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, activator.Selectors);

            _contentRect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, ContentRef);

            OffsetLeft = CalculatedLeft();
            OffsetTop = await CalculatedTop();

            if (ZIndex == default)
            {
                ZIndex = await ActiveZIndex();
            }

            await InvokeStateHasChangedAsync();
        }

        private void RunDelay(string type)
        {
            Task.Delay(10);

            switch (type)
            {
                case "open":
                    Value = true;
                    break;
                case "close":
                    Value = false;
                    break;
                default:
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DomEventJsInterop.RemoveEventListerner<Window>("window", "resize", OnResize);
        }
    }
}
